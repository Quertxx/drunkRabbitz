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
    


    void Start()
    {
        transform.position = points[startPoint].position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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



    IEnumerator EnemyShoot (float seconds)
    {
        isShooting = true;
        if (isShooting)
        {
            clone = Instantiate(bullet, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
            Vector3 vel = transform.TransformDirection(player.transform.position).normalized;
            cloneRb.velocity = vel * bulletSpeed;
            yield return new WaitForSeconds(seconds);
        }

        isShooting = false;
    }
}
