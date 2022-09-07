using System.Collections;
using UnityEngine;

public class AirplaneAttack : MonoBehaviour
{
    private Planet _target;
    private float _radiusTarget;

    public void SetTarget(Planet target)
    {
        _target = target;
        _radiusTarget = target.GetComponent<CircleCollider2D>().radius;
    }

    private void Start()
    {
        StartCoroutine(AddAirplanes());
    }

    private IEnumerator AddAirplanes()
    {
        while (true)
        {
            if ((transform.position - _target.transform.position).magnitude < .35f + _radiusTarget)
            {
                _target.TakeDamage();
                Destroy(gameObject);
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }
}
