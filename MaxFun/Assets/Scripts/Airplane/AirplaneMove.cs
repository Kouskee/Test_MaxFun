using UnityEngine;
using UnityEngine.AI;

public class AirplaneMove : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        TryGetComponent(out _agent);
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        var direction = transform.position - _target.position;
        transform.up = direction;
        _agent.SetDestination(_target.position);
    }
}
