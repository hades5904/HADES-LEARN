using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] options; // For Multiple Choice Questions (MCQ)
    public string correctAnswer;
}
