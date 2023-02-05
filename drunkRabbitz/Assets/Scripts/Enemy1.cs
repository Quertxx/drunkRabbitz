using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    PlayerMovement playerScript;
    public Collider2D selfCollision;
    public float health = 1;
    public GameObject drop;
    private HealthPickup itemScript;
    public ParticleSystem death;
    public AudioSource deathSound;

    public void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        itemScript = drop.GetComponent<HealthPickup>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.Health = playerScript.Health - 10.0f;
        }
        if (collision.gameObject.CompareTag("Axe"))
        {
            health--;
            int randomnumber;
            randomnumber = Random.Range(0, 10);
            if(randomnumber > 7)
            {
                itemScript.pickTypesRef = HealthPickup.pickupType.health;
                GameObject itemdrop = Instantiate(drop, transform.position, Quaternion.identity);
            }
            else
            {
                itemScript.pickTypesRef = HealthPickup.pickupType.carrot;
                GameObject itemdrop = Instantiate(drop, transform.position, Quaternion.identity);
            }
            if (health <= 0)
            {
                Instantiate(death, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }





}
