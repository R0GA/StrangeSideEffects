using UnityEngine;

[CreateAssetMenu(fileName = "New Drug Effect", menuName = "Clinical Trial/Drug Effect")]
public class DrugEffect : ScriptableObject
{
    [Header("Basic Info")]
    public string effectName;
    public int effectDamage;
    public int effectMinValue;
    public int effectMaxValue;
    public int effectClearValue;
    public int effectChance;
}