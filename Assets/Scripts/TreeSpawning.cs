using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawning : MonoBehaviour {
    [SerializeField] private TreeData treeData;
    
    private bool isTreeRespawning = false;

    private void Update() {
        if (transform.childCount <= 0 && !isTreeRespawning) {
            StartCoroutine(TreeRespawnRoutine());
        }
    }

    private IEnumerator TreeRespawnRoutine() {
        isTreeRespawning = true;
        float respawnTime = Random.Range(treeData.maxSpawnTime, treeData.minSpawnTime);
        yield return new WaitForSeconds(respawnTime);
        GameObject newTree = Instantiate(treeData.treePrefab, transform.position, Quaternion.identity);
        newTree.transform.SetParent(transform);
        isTreeRespawning = false;
    }
}
