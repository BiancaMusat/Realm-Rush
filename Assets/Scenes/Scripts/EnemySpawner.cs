using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)][SerializeField] float seconsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] int score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnedEnemySFX;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        StartCoroutine(EnemiesSpawning());
    }

    IEnumerator EnemiesSpawning()
    {
        while (true)
        {
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            score++;
            scoreText.text = score.ToString();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(seconsBetweenSpawns);
        }
    }
}
