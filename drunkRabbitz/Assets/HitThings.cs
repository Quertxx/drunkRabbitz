using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitThings : MonoBehaviour
{

    private PlayerMovement playerscript;
    private GameObject player;
    public PolygonCollider2D axeCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<PlayerMovement>();
        axeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerscript.isRotating)
        {
            axeCollider.enabled = true;
        }
        else
        {
            axeCollider.enabled = false;
        }
    }


}
