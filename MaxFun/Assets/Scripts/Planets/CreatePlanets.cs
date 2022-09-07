using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CreatePlanets : MonoBehaviour
{
    private DataCreator _data;

    private List<Planet> _planets;
    
    private PlanetsConfig _config;
    private Camera _camera;
    private Vector3 _min, _max;
    private float _zPosition;
    
    private readonly Collider2D[] _nearestPlanets = new Collider2D[5];

    public void Init(DataCreator creator)
    {
        _data = creator;
    }
    
    private void Awake()
    {
        _camera = Camera.main;
        var facade = new FacadePlanets();
        _config = facade.GetConfig(_data.DifficultyGame);
        _planets = new List<Planet>(_config.CountPlanets);
    }

    private void Start()
    {
        _min = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        _max = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        SpawnPlanets();
        SpawnPlayer();
    }

    private void SpawnPlanets()
    {
        for (var i = 0; i < _config.CountPlanets; i++)
        {
            var radius = Random.Range(_config.RangeRadius.x, _config.RangeRadius.y);

            var position = GetPosition(radius);

            if (!CheckForPlacing(position, radius))
            {
                var bl = false;
                for (var j = 0; j < _data.NumberChecks; j++)
                {
                    position = GetPosition(radius);
                    bl = CheckForPlacing(position, radius);
                    if(bl) break;
                }
                
                if(!bl) continue;
            }

            var planet = Instantiate(_data.PlanetPrefab, position, quaternion.identity, transform);
            
            planet.radius = radius;
            planet.GetComponent<NavMeshAgent>().radius = radius;
            var planetComp = planet.GetComponent<Planet>();
            planetComp.Init(_config.NumberAirplanes * radius, TypePlanet.Neutral);
            var child = planet.GetComponentInChildren<SpriteRenderer>();
            child.color = _data.NeutralColor;
            child.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
            
            _planets.Add(planetComp);
        }
    }

    private void SpawnPlayer()
    {
        var planet = _planets[Random.Range(0, _planets.Count)];
        planet.Init(50, TypePlanet.Player);
        planet.GetComponentInChildren<SpriteRenderer>().color = _data.PlayerColor;
        var player = planet.AddComponent<Player>();
        player.gameObject.layer = 6;
        player.name = "Player";
        player.Init(_data.AirplaneAttack);
    }

    private bool CheckForPlacing(Vector3 position, float radius)
    {
        var countNearest = Physics2D.OverlapCircleNonAlloc(position, radius * 2, _nearestPlanets, 1 << LayerMask.NameToLayer("Planet"));

        if(countNearest < 1) return true;
        float sumRadius = 0;
        
        for (var i = 0; i < countNearest; i++)
        {
            sumRadius += _nearestPlanets[i].GetComponent<CircleCollider2D>().radius;
        }
        var countCheckForPlacing = Physics2D.OverlapCircleNonAlloc(position, sumRadius + radius, _nearestPlanets, 1 << LayerMask.NameToLayer("Planet"));

        return countCheckForPlacing < 1;
    }

    private Vector3 GetPosition(float radius)
    {
        var x = Coordinate(_min.x, _max.x, radius);
        var y = Coordinate(_min.y, _max.y, radius);
        return new Vector3(x, y, 0);
    }
    
    private float Coordinate(float min, float max, float radius)
    {
        return Random.Range(min + radius, max - radius);
    }
}
