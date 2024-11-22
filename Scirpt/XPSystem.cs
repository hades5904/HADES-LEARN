using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class LevelData
{
    public int level;
    public int xpRequired;
}

public class XPSystem : MonoBehaviour
{
    public int currentXP = 0;
    public int currentLevel = 1;
    public LevelData[] levelData; // Array for level thresholds

    public TextMeshProUGUI xpText;  // Change to TextMeshProUGUI
    public TextMeshProUGUI levelText; // Change to TextMeshProUGUI
    public UnityEngine.UI.Slider progressBar;

    void Start()
    {
        // Load saved XP and Level from PlayerPrefs
        currentXP = PlayerPrefs.GetInt("PlayerXP", 0);
        currentLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        UpdateUI();
    }

    public void AddXP(int xp)
    {
        currentXP += xp;
        CheckLevelUp();
        UpdateUI();
    }

    private void CheckLevelUp()
    {
        if (currentLevel - 1 < levelData.Length)
        {
            if (currentXP >= levelData[currentLevel - 1].xpRequired) // Fixed index here
            {
                currentLevel++;
                if (currentLevel > levelData.Length)
                {
                    currentLevel = levelData.Length; // Prevent currentLevel from going beyond the array size
                }
                currentXP = 0; // Reset XP for the next level
            }
        }
        else
        {
            currentLevel = levelData.Length; // Set to maximum level
        }
        // Save progress
        PlayerPrefs.SetInt("PlayerXP", currentXP);
        PlayerPrefs.SetInt("PlayerLevel", currentLevel);
    }

    private void UpdateUI()
    {
        xpText.text = "XP: " + currentXP.ToString();  // Update with TextMeshPro
        levelText.text = "Level: " + currentLevel.ToString();  // Update with TextMeshPro
        progressBar.value = (float)currentXP / levelData[currentLevel - 1].xpRequired;
    }
}
