using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitThings : MonoBehaviour
{

    private PlayerMovement playerscript;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerscript.isRotating)
        {
            if (collision.gameObject.tag == ("Enemy"))
            {

            }
        }
    }
}
