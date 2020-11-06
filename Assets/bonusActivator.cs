using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusActivator : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        gameObject.SetActive(false);
        MovementPlayer player = FindObjectOfType<MovementPlayer>();
        player.randBonus();
    }
}
