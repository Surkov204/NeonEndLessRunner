using UnityEngine;

[CreateAssetMenu(menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Heal Resource (Red Bar)")]
    public int maxHealth = 10;
    [Header("Auto Heal Resource (Green Bar)")]
    public int maxHealResource = 100;
    [Header("Movement")]
    public float moveSpeed = 4f;
}
