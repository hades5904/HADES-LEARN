using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelContainer : MonoBehaviour
{
    public Button[] levelButtons; // Assign all the level buttons in the Inspector
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public string[] levelSceneNames; // Array to hold scene names for each level (assign in Inspector)

    private int unlockedLevel;

    void Start()
    {
        // Load the last unlocked level (defaults to 1 if not set)
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        UpdateLevelButtons();
    }

    void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = i < unlockedLevel;
            levelButtons[i].interactable = isUnlocked;

            Image buttonImage = levelButtons[i].GetComponent<Image>();
            Transform lockIcon = levelButtons[i].transform.Find("LockIcon");

            if (isUnlocked)
            {
                buttonImage.sprite = unlockedSprite;
                if (lockIcon != null) lockIcon.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.sprite = lockedSprite;
                if (lockIcon != null) lockIcon.gameObject.SetActive(true);
            }

            int index = i;
            levelButtons[i].onClick.RemoveAllListeners();
            levelButtons[i].onClick.AddListener(() => OnLevelSelected(index));
        }
    }

    public void OnLevelSelected(int level)
    {
        if (level <= unlockedLevel)
        {
            // Load the corresponding level scene
            SceneManager.LoadScene(levelSceneNames[level]);
        }
        else
        {
            Debug.LogWarning($"Level {level} is locked!");
        }
    }

    public void UnlockNextLevel()
    {
        if (unlockedLevel < levelButtons.Length)
        {
            unlockedLevel++;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            UpdateLevelButtons();
        }
    }
}
