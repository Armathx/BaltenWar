using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave", order = 1)]
public class WavePreset : ScriptableObject
{
    public WaveManager.ENNEMY_TYPE[] enemyTypes;
    public int[] count;
}