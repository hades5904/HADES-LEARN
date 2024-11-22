using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Game/DifficultyConfig", order = 1)]
public class DifficultyConfig : ScriptableObject
{
    public int easyXPReward;
    public int mediumXPReward;
    public int hardXPReward;
}
