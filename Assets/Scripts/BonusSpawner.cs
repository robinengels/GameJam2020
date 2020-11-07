using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

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
        spawned = Instantiate(Bonus, pos_to_spawn, Quaternion.identity);
    }

}
