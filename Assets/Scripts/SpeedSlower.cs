using UnityEngine;
using UnityEngine.UI;

public class SpeedSlower : MonoBehaviour
{
    private float originalPlayerSpeed;
    private Image img;
    private PlayerController player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = FindObjectOfType<PlayerController>();
        //player.NoJumpingIcon.gameObject.SetActive(true);
        originalPlayerSpeed = player.speed;
        player.Blocked = true;
        Image img = player.NoJumpingIcon;
        img.gameObject.SetActive(true);
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
        Image img = player.NoJumpingIcon;
        img.gameObject.SetActive(false);
    }
}
