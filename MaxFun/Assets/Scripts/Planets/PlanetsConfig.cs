using UnityEngine;

[CreateAssetMenu(fileName = "Planets", menuName = "Configs/Planets")]
public class PlanetsConfig : ScriptableObject
{
    [SerializeField] private DifficultyGame _difficulty;
    [Space]
    [SerializeField] private int _countPlanets;
    [SerializeField] private int _numberAirplanes;
    [SerializeField] private Vector2 _rangeRadius;

    public DifficultyGame Difficulty => _difficulty;
    public int CountPlanets => _countPlanets;
    public int NumberAirplanes => _numberAirplanes;
    public Vector2 RangeRadius => _rangeRadius;
}

public enum DifficultyGame
{
    Easy,
    Normal,
    Hard
}