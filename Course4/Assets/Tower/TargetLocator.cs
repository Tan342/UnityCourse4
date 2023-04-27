using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectTileParticle;
    [SerializeField] float range = 15f;
    Transform target;


    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closetTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closetTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closetTarget;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position,target.position);
        weapon.transform.LookAt(target);
        if(targetDistance <= range)
        {
            Attack(true);
            return;
        }
        Attack(false);
    }

    void Attack(bool isActive)
    {
        var emission = projectTileParticle.emission;
        emission.enabled = isActive;
    }
}
