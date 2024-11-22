using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI[] optionTexts;
    public LevelConfig[] levels; // Assign LevelConfig ScriptableObjects in the Inspector
    public XPSystem xpSystem;

    private int currentQuestionIndex = 0;
    private int currentLevelIndex = 0;
    private Question[] currentQuestions;

    void Start()
    {
        LoadLevel(0); // Start with the first level
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < levels.Length)
        {
            currentLevelIndex = levelIndex;
            currentQuestions = levels[levelIndex].questions;

            if (currentQuestions.Length > 0)
            {
                LoadQuestion(0); // Load the first question of the level
            }
            else
            {
                Debug.LogError("No questions available in the selected level.");
            }
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    public void LoadQuestion(int questionIndex)
    {
        if (questionIndex < currentQuestions.Length)
        {
            currentQuestionIndex = questionIndex;
            Question currentQuestion = currentQuestions[questionIndex];

            questionText.text = currentQuestion.questionText;

            // Activate buttons for the options and set the texts
            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < currentQuestion.options.Length)
                {
                    optionButtons[i].gameObject.SetActive(true);
                    optionTexts[i].text = currentQuestion.options[i];

                    // Clear previous listeners and add a new listener
                    int index = i;
                    optionButtons[i].onClick.RemoveAllListeners();
                    optionButtons[i].onClick.AddListener(() => OnOptionSelected(currentQuestion.options[index]));
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false); // Hide unused buttons
                }
            }
        }
        else
        {
            Debug.Log("All questions in this level completed!");
            // Reward XP after completing all questions in the level
            xpSystem.AddXP(levels[currentLevelIndex].xpReward);

            // Save the XP and level, then go back to the main menu
            PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex + 2);  // Unlock next level
            SceneManager.LoadScene("FRONT");  // Change "FRONT" to your level container scene
        }
    }

    public void OnOptionSelected(string selectedAnswer)
    {
        Debug.Log("Selected Answer: " + selectedAnswer);

        // Check if the selected answer is correct
        if (selectedAnswer == currentQuestions[currentQuestionIndex].correctAnswer)
        {
            Debug.Log("Correct Answer!");
            xpSystem.AddXP(levels[currentLevelIndex].xpReward); // Add XP for a correct answer
            LoadQuestion(currentQuestionIndex + 1); // Load the next question
        }
        else
        {
            Debug.Log("Incorrect Answer");
        }
    }
}
