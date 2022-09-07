using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static readonly UnityEvent<Planet> CaptureByPlayer = new UnityEvent<Planet>();

    private DataCreator _data;
    private Camera _camera;
    private Player _currentPlayer;

    public void Init(DataCreator data)
    {
        _data = data;
    }

    private void Start()
    {
        CaptureByPlayer.AddListener(ChangeOnPlayer);
        _camera = Camera.main;
    }

    private void ChangeOnPlayer(Planet planet)
    {
        planet.Init(50, TypePlanet.Player);
        planet.GetComponentInChildren<SpriteRenderer>().color = _data.PlayerColor;
        var player = planet.AddComponent<Player>();
        player.gameObject.layer = 6;
        player.name = "Player";
        player.Init(_data.AirplaneAttack);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var hit = GetHit(1 << LayerMask.NameToLayer("Player"));

            if (_currentPlayer != null)
                _currentPlayer.GetComponentInChildren<TMP_Text>().color = Color.white;
            
            if (hit.collider == null) return;
            if (!hit.collider.TryGetComponent(out _currentPlayer))
            {
                _currentPlayer = null;
            }

            _currentPlayer.GetComponentInChildren<TMP_Text>().color = Color.green;
        }

        if (Input.GetMouseButtonDown(0) && _currentPlayer != null)
        {
            var hit = GetHit(1 << LayerMask.NameToLayer("Planet"));

            if (hit.collider == null) return;
            if (!hit.collider.TryGetComponent(out Planet planet)) return;

            _currentPlayer.AttackPlanet(planet);
        }

        RaycastHit2D GetHit(LayerMask mask)
        {
            return Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, Mathf.Infinity,
                mask);
        }
    }
}