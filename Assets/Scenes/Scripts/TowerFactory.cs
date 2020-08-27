using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Block baseBlock)
    {
        int noTowers = towerQueue.Count;

        if (noTowers < towerLimit)
        {
            InstantiateNewTower(baseBlock);
        }
        else
        {
            MoveExistingTowers(baseBlock);
        }
    }

    private void InstantiateNewTower(Block newbaseBlock)
    {
        var newTower = Instantiate(towerPrefab, newbaseBlock.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        newbaseBlock.isPlaceable = false;
        newTower.baseBlock = newbaseBlock;
        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTowers(Block newbaseBlock)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.baseBlock.isPlaceable = true;
        newbaseBlock.isPlaceable = false;
        oldTower.baseBlock = newbaseBlock;
        oldTower.transform.position = newbaseBlock.transform.position;
        towerQueue.Enqueue(oldTower);
    }
}
