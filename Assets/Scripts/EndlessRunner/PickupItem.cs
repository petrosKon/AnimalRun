using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Points")]
    public int scoreToGive;

    [Header("PickUp Effect")]
    public ParticleSystem pickUpEffect;

    private AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(scoreToGive);

            ParticleSystem clone =  Instantiate(pickUpEffect, gameObject.transform.position,Quaternion.identity);

            Destroy(clone, 0.2f);

            gameObject.SetActive(false);

            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }

            coinSound.Play();

        }
    }
}
