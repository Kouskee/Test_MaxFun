using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Planet : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberAirplanesTxt;

    private TypePlanet _type;
    private int _numberAirplanes;

    public void Init(float numberAirplanes, TypePlanet type)
    {
        _numberAirplanes = (int) numberAirplanes;
        _type = type;
    }

    private void Awake()
    {
        TryGetComponent(out NavMeshAgent agent);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        if (_type == TypePlanet.Player)
            StartCoroutine(AddAirplanes());
        StartCoroutine(CheckNumber());
    }

    #region IEnumerator

    private IEnumerator AddAirplanes()
    {
        while (true)
        {
            _numberAirplanes += 1;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator CheckNumber()
    {
        while (true)
        {
            _numberAirplanesTxt.text = _numberAirplanes.ToString();
            if(_type == TypePlanet.Neutral && _numberAirplanes <= 0)
                GameManager.CaptureByPlayer.Invoke(this);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    #endregion

    public int SendAirplanes()
    {
        return _numberAirplanes /= 2;
    }

    public void TakeDamage()
    {
        if(_type == TypePlanet.Neutral || _type == TypePlanet.Enemy)
            _numberAirplanes--;
        else
            _numberAirplanes++;
    }
}

public enum TypePlanet
{
    Neutral,
    Player,
    Enemy
}