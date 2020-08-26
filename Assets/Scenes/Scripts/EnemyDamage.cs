﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            killEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints -= 1;
    }

    private void killEnemy()
    {
        Destroy(gameObject);
    }
}
