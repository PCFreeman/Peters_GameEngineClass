using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float damageAmount;
    public Transform projectileSpwnTransform;


    public void Use(Health targetHealth)
    {
        if(projectile)
        {
            var newProjectile = Instantiate(projectile, projectileSpwnTransform.position,Quaternion.identity);
            var controller = newProjectile.GetComponent<ProjectileController>();
            controller.SetTarget(targetHealth);
            controller.damageAmount = damageAmount;
        }
        else
        {
            targetHealth.TakeDamage(damageAmount);
        }
    }
}
