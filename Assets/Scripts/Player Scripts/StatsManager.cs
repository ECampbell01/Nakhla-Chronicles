// Contributions: Ethan Campbell
// Date Created: 3/4/2025

using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    public int HP;
    public int agility;
    public int defense;
    public int luck;
    public int meleeDamage;
    public int rangedDamage;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
