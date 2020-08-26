using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectileParticle;

    // Update is called once per frame
    void Update()
    {
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
