using UnityEngine;

public class FacadePlanets
{
    private readonly PlanetsConfig[] _configs;

    public FacadePlanets()
    {
        _configs = Resources.LoadAll<PlanetsConfig>("PlanetConfigs");
    }

    public PlanetsConfig GetConfig(DifficultyGame difficulty)
    {
        for (var i = 0; i < _configs.Length; i++)
        {
            if (_configs[i].Difficulty == difficulty)
                return _configs[i];
        }

        return null;
    }
}