using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        objectToMove.LookAt(targetEnemy);
    }
}
