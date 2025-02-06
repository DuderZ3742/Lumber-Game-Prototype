using UnityEngine;

[CreateAssetMenu(fileName = "TreeData", menuName = "ScriptableObjects/TreeDataScriptableObject", order = 1)]
public class TreeData : ScriptableObject {
    public GameObject treePrefab;

    public int minTreeHealth;
    public int maxTreeHealth;

    public int minWoodReward;
    public int maxWoodReward;

    public float minSpawnTime;
    public float maxSpawnTime;
}
