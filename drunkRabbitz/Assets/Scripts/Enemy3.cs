using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    //patrol-specific variables
    public Transform[] points;
    [SerializeField] int startPoint;
    private int p;
    [SerializeField] float speed;


    //shoot variables
    public float detectionDistance;
    GameObject player;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootCooldown;
    GameObject clone;
    private bool isShooting = false;
    [SerializeField] Transform shootLocation;
    [SerializeField] GameObject pivotPoint;

    PlayerMovement playerScript;
    public Collider2D selfCollision;


    void Start()
    {
        transform.position = points[startPoint].position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateCrosshair(pivotPoint.transform.position);



        if (Vector2.Distance(transform.position, points[p].position) < 0.02f)
        {
            p++;
            if (p == points.Length)
            {
                p = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[p].position, speed * Time.deltaTime);


        if ((Vector2.Distance(this.gameObject.transform.position, player.transform.position) < detectionDistance) && !isShooting)
        {

            StartCoroutine(EnemyShoot(2f));
           

            /*if(Time.time > shootCooldown)
            {
                clone = Instantiate(bullet, this.gameObject.transform);
                Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
                cloneRb.velocity = transform.TransformDirection(player.transform.position * bulletSpeed);
            }*/
        }
    }


    private void rotateCrosshair(Vector3 shootPosition)
    {
        Vector3 distance = player.transform.position - pivotPoint.transform.position;
        float angle = Mathf.Atan2(-distance.y, -distance.x) * Mathf.Rad2Deg;
        pivotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator EnemyShoot (float seconds)
    {
        isShooting = true;
        if (isShooting)
        {
            clone = Instantiate(bullet, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
            Vector3 dir = transform.TransformDirection(shootLocation.transform.position).normalized;
            cloneRb.velocity = -dir * bulletSpeed;  //the problem has to lie here --> this is the only line of code that makes bullets move
            yield return new WaitForSeconds(seconds);
        }

        isShooting = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.Health = -10f;
    }

    //set the invisible - shooting location to be just in front of the enemy = make the enemy turn towards the player 
    //then just set a pure force related to bullet speed and time to the bullet?

}
