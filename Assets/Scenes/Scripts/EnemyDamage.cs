using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 5;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem dieParticlePrefab;
    [SerializeField] AudioClip hitEnemySFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
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
        hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(hitEnemySFX);
    }

    private void killEnemy()
    {
        var diePrefab = Instantiate(dieParticlePrefab, transform.position, Quaternion.identity);
        diePrefab.Play();
        Destroy(diePrefab.gameObject, diePrefab.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
