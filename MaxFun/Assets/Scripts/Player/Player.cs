using UnityEngine;

public class Player : MonoBehaviour
{
    private AirplaneAttack _airplane;
    private Planet _planet;
    
    public void Init(AirplaneAttack airplane)
    {
        _airplane = airplane;
    }
    
    private void Awake()
    {
        TryGetComponent(out _planet);
    }

    public void AttackPlanet(Planet planet)
    {
        var airplanes = _planet.SendAirplanes();
        
        for (var i = 0; i < airplanes; i++)
        {
            var airplane = Instantiate(_airplane, transform.position, Quaternion.identity, transform);
            airplane.SetTarget(planet);
            airplane.GetComponent<AirplaneMove>().SetTarget(planet.transform);
        }
    }
}