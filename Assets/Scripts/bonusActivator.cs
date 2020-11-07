using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusActivator : MonoBehaviour
{

    [SerializeField] private AudioClip BonusSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _audioSource.PlayOneShot(BonusSound);
        Debug.Log("Trigger");
        gameObject.SetActive(false);
        PlayerController player = FindObjectOfType<PlayerController>();
        player.randBonus();
    }
}
