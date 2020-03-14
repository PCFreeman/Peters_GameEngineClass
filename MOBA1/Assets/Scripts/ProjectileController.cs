using System.Linq;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float moveSpeed;
    public float damageAmount;

    private Vector3 lastTargetPosition;
    private Health targetHealth;

    //private Vector3 GetBoudCenter(Health targetHealth)
    //{
    //    Vector3 center = new Vector3();
    //    var colliders = targetHealth.GetComponentInChildren<Collider>().Where(X =>!x.istrigger));
    //    foreach(var collider in colliders)
    //    {
    //        center += collider.bounds.center;
    //    }
    //    return center / colliders.Count();
    //}
    private void Update()
    {
        if (targetHealth.IsAlive())
        {
            var collider = targetHealth.GetComponentInChildren<Collider>();
            if (collider)
                lastTargetPosition = collider.bounds.center;
            else
            lastTargetPosition = targetHealth.transform.position;
        }

        Vector3 newPosition = Vector3.MoveTowards(transform.position, lastTargetPosition, moveSpeed * Time.deltaTime);
        transform.position = newPosition;
        if (transform.position == lastTargetPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Health>() == targetHealth)
        {
            targetHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Health target)
    {
        targetHealth = target;
    }
}
