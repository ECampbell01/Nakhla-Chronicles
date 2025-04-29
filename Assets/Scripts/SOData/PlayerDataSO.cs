using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO")]

public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int _HP;

    [SerializeField]
    private int _agility;

    [SerializeField]
    private int _defense;

    [SerializeField]
    private int _luck;

    [SerializeField]
    private int _meleeDamage;

    [SerializeField]
    private int _rangedDamage;

    [SerializeField]
    private int _level;

    [SerializeField]
    private int _experience;

    [SerializeField]
    private int _availablePoints;

    [SerializeField]
    private GameObject _companionPrefab;

    public int HP
    {
        get { return _HP; }
        set { _HP = value; }
    }

    public int Agility
    {
        get { return _agility; }
        set { _agility = value; }
    }

    public int Defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public int Luck
    {
        get { return _luck; }
        set { _luck = value; }
    }

    public int MeleeDamage
    {
        get { return _meleeDamage; }
        set { _meleeDamage = value; }
    }

    public int RangedDamage
    {
        get { return _rangedDamage; }
        set { _rangedDamage = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int Experience
    {
        get { return _experience; }
        set { _experience = value; }
    }

    public int AvailablePoints
    {
        get { return _availablePoints; }
        set { _availablePoints = value; }
    }

    public GameObject CompanionPrefab
    {
        get { return _companionPrefab; }
        set { _companionPrefab = value; }
    }
}

