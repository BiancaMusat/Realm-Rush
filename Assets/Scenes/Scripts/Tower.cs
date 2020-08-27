using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectileParticle;

    public Block baseBlock;

    Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToMove.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0)
        {
            return;
        }
        Transform closestEnemy = sceneEnemies[0].transform;
        foreach (var testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        if (Vector3.Distance(transformA.position, transform.position) < Vector3.Distance(transformB.position, transform.position))
        {
            return transformA;
        }
        return transformB;
    }

    private void FireAtEnemy()
    {
        float distToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distToEnemy <= attackRange)
        {
            Shoot(true);
        } else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isShooting)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isShooting;

    }
}
