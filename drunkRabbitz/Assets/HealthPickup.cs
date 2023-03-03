using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public enum pickupType {health, carrot};

    public pickupType pickTypesRef;
    private SpriteRenderer spriteRendererRef;
    public Sprite carrot;
    public Sprite health;
    public float healthAmm = 25;
    GameObject player;
    PlayerMovement playerScript;
    private PolygonCollider2D itemCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRendererRef = gameObject.GetComponent<SpriteRenderer>();
        switch (pickTypesRef)
        {
            case pickupType.health:
                /*if (itemCollider != null){
                    Destroy(itemCollider);
                }
                itemCollider = gameObject.AddComponent<PolygonCollider2D>()*/;
                //itemCollider.isTrigger = true;
                spriteRendererRef.sprite = health;
                break;
            case pickupType.carrot:
                /*if (itemCollider != null)
                {
                    Destroy(itemCollider);
                }
                itemCollider = gameObject.AddComponent<PolygonCollider2D>();*/
                //itemCollider.isTrigger = true;
                spriteRendererRef.sprite = carrot;
                break;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (pickTypesRef)
            {
                case pickupType.health:
                    playerScript.Health = playerScript.Health + +healthAmm;
                    playerScript.Health = Mathf.Clamp(playerScript.Health, 0, playerScript.maxHealth);
                    Destroy(this.gameObject);
                    break;
                case pickupType.carrot:
                    playerScript.carrots++;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}


