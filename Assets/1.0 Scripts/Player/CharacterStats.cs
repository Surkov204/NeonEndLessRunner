using UnityEngine;

[CreateAssetMenu(menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    public int maxHealth = 10;
    // sau này có thể thêm:
    // public float speed;
    // public float damage;
}
