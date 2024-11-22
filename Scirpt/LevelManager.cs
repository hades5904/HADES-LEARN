using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public XPSystem xpSystem;
    public string[] difficultyLevels = { "Easy", "Medium", "Hard" };

    void Start()
    {
        xpSystem = GetComponent<XPSystem>();
    }

    public void IncreaseDifficulty()
    {
        int currentLevel = xpSystem.currentLevel;

        // Adjust difficulty based on the player's level
        if (currentLevel <= 5)
        {
            SetDifficulty("Easy");
        }
        else if (currentLevel <= 10)
        {
            SetDifficulty("Medium");
        }
        else
        {
            SetDifficulty("Hard");
        }
    }

    private void SetDifficulty(string difficulty)
    {
        Debug.Log("Setting difficulty to: " + difficulty);
        // Here you can add difficulty-related logic like more complex challenges, hints, etc.
    }
}
