using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Collider2D selfCollision;
    public Collider2D smogTrigger;


    public void Awake()
    {
       smogTrigger = GetComponentInChildren<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       //if collided with player = player takes dmg
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //if player = take dmg to player

    }


}
