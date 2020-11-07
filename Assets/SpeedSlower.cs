using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSlower : MonoBehaviour
{
    private float originalPlayerSpeed;
    private PlayerController player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        player = FindObjectOfType<PlayerController>();
        originalPlayerSpeed = player.speed;
        player.Blocked = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        player.speed = 0.3f;
        Debug.Log("Slowing player down ...");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player.speed = originalPlayerSpeed;
        player.Blocked = false;
        Debug.Log("Player speed is back !");
    }
}
