using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Smog : MonoBehaviour
{
    public Collider2D smogCollision;
    PlayerMovement playerScript;

    public void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.Health = -1f;
        }
    }
}
