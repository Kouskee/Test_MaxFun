using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private CreatePlanets _planets;
    [SerializeField] private GameManager _manager;
    [Header("Creator")]
    [SerializeField] private CircleCollider2D _planetPrefab;
    [SerializeField] private DifficultyGame _difficultyGame;
    [SerializeField] private int _numberChecks;
    [Space]
    [SerializeField] private Color _playerColor;
    [SerializeField] private Color _neutralColor;
    [Space]
    [Header("Player")]
    [SerializeField] private AirplaneAttack _airplaneAttack;

    private void Awake()
    {
        var data = new DataCreator
        {
            DifficultyGame = _difficultyGame,
            NumberChecks = _numberChecks,
            PlayerColor = _playerColor,
            NeutralColor = _neutralColor,
            AirplaneAttack = _airplaneAttack,
            PlanetPrefab = _planetPrefab
        };

        _planets.Init(data);
        _manager.Init(data);
        
        Destroy(gameObject);
    }
}

public struct DataCreator
{
    public DifficultyGame DifficultyGame;
    public int NumberChecks;
    public Color PlayerColor;
    public Color NeutralColor;
    public AirplaneAttack AirplaneAttack;
    public CircleCollider2D PlanetPrefab;
}
