using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    public Question[] questions; // Array of questions for the level
    public int xpReward; // XP reward for a correct answer
}
