using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] ParticleSystem goalParticle;

    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Block> path)
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        selfDestruct();
    }

    private void selfDestruct()
    {
        var goalPrefab = Instantiate(goalParticle, transform.position, Quaternion.identity);
        goalPrefab.Play();
        Destroy(goalPrefab.gameObject, goalPrefab.main.duration);

        Destroy(gameObject);
    }
}
