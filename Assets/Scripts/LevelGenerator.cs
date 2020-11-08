using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform levelPartStart;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Transform player;
    private Vector3  _lastEndPosition;
    
    private void Awake()
    {
        _lastEndPosition = levelPartStart.Find("EndPoint").position;
        for (int i = 0; i < 5; i++)
        {
            SpawnLevelPart();
        }
        
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        _lastEndPosition = SpawnLevelPart(chosenLevelPart,_lastEndPosition).Find("EndPoint").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 position)
    {
        Transform levelPartTransform = Instantiate(levelPart, position, Quaternion.identity);
        return levelPartTransform;
        // if (levelPart.gameObject.TryAcquire(out var part))
        // {
        //     var partTransform = part.transform;
        //     partTransform.position = position;
        //     return partTransform;
        // }
        // return null;
    }
}
