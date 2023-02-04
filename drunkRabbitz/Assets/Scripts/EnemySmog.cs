using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmog : MonoBehaviour
{
    PlayerMovement playerScript;
    public Collider2D selfCollision;


    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.Health = playerScript.Health - 1.0f;
        }
    }
}
