using UnityEngine;
using Pooling;

public class bonusActivator : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        gameObject.TryRelease();
        PlayerController player = FindObjectOfType<PlayerController>();
        player.randBonus();
    }
}
