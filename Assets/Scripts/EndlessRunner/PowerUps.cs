using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Header("Variables")]
    public bool doublePoints;
    public bool safeMode;
    public float powerUpLength;

    [Header("PickUp Effect")]
    public ParticleSystem pickUpEffect;

    public Sprite[] powerUpSprites;

    private void Awake()
    {
        int powerUpSelector = Random.Range(0, 2);

        switch (powerUpSelector)
        {
            case 0: safeMode = true; break;
            case 1: doublePoints = true; break;
        }

        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelector];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PowerUpManager.instance.ActivatePowerUp(doublePoints, safeMode, powerUpLength);

            ParticleSystem clone = Instantiate(pickUpEffect, gameObject.transform.position, Quaternion.identity);

            Destroy(clone, 0.2f);

            gameObject.SetActive(false);

        } 
    }
}
