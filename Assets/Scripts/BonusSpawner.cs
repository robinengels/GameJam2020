using UnityEngine;
using Random = UnityEngine.Random;
using Pooling;

public class BonusSpawner : MonoBehaviour
{

    [SerializeField] private int dropRating;
    [SerializeField] private Transform Bonus;
    [SerializeField] private Transform LevelPart;

    private Transform spawned;

    private void Awake()
    {
        int rand = Random.Range(0, dropRating);
        if (rand == 1)
        {
            spawnRandomBonus();
        }
    }
    
    private void spawnRandomBonus()
    {
        Vector3 pos_to_spawn = LevelPart.Find("BonusSpawn").position;
        Instantiate(Bonus, pos_to_spawn, Quaternion.identity);
        // if (Bonus.gameObject.TryAcquire(out var bonus))
        // {
        //     var bonusTransform = bonus.transform;
        //     bonusTransform.position = pos_to_spawn;
        // }
    }

}
