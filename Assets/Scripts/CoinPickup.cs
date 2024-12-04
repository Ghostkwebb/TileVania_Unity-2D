using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForCoinPickup = 100;
    [SerializeField] AudioClip shardPickupSFX;
    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        if (other.gameObject.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(shardPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
    }
}
