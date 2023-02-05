using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoints : MonoBehaviour
{
    gameController controllerScript;
    public Collider2D trigger;

    private void Awake()
    {
        controllerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        controllerScript.lastCheckpoint = GameObject.Find("StartPoint").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trigger.enabled = false;
            controllerScript.lastCheckpoint = this.gameObject.transform;
        }
    }
}
