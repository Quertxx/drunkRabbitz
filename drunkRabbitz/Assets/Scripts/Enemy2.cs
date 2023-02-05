using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    //patrol-specific variables
    public Transform[] points;
    [SerializeField] int startPoint;
    private int p;
    public float knockback;
    public GameObject drop;
    private HealthPickup itemScript;



    //follow-specific variables
    Vector2 moveDirection;
    public int detectionDistance;
    GameObject player;
    public float health = 2;



    // state-neutral variables
    [SerializeField] float speed;   //Speed of the enemy;
    Rigidbody2D rb;

    PlayerMovement playerScript;
    public Collider2D selfCollision;


    enum EnemyBehaviours
    {
        Follow,
        Patrol,
        Idle,
    }

    private EnemyBehaviours enemyState;


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        itemScript = drop.GetComponent<HealthPickup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // switch to EnemyBehaviours.Idle if the enemy's base state is meant to be idle
        enemyState = EnemyBehaviours.Patrol;
        transform.position = points[startPoint].position;

    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
            {
            case EnemyBehaviours.Patrol:

               

                if (Vector2.Distance(transform.position, points[p].position) < 0.02f)
                {
                    p++;
                    if (p == points.Length)
                    {
                        p = 0;
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, points[p].position, speed * Time.deltaTime);



                if ((Vector2.Distance(this.gameObject.transform.position, player.transform.position) < detectionDistance))
                {
                    enemyState = EnemyBehaviours.Follow;
                }

                break;


            case EnemyBehaviours.Follow:

                Vector2 direction = (player.transform.position - transform.position).normalized;
                moveDirection = direction;
                rb.velocity = new Vector2(moveDirection.x, 0) * speed;

                if ((Vector2.Distance(this.gameObject.transform.position, player.transform.position) > detectionDistance))
                {
                    enemyState = EnemyBehaviours.Patrol;
                }

                break;




            case EnemyBehaviours.Idle:
                //add animator.idleAnim thing here
                break;
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir = player.transform.position - transform.position;
            playerScript.Health = playerScript.Health - 25.0f;
            playerScript.playerRB.AddForce(dir.normalized * knockback, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("Axe"))
        {
            print(health);
            health--;
            if (health <= 0)
            {
                int randomnumber;
                randomnumber = Random.Range(0, 10);
                if (randomnumber > 7)
                {
                    itemScript.pickTypesRef = HealthPickup.pickupType.health;
                    GameObject itemdrop = Instantiate(drop, transform.position, Quaternion.identity);
                }
                else
                {
                    itemScript.pickTypesRef = HealthPickup.pickupType.carrot;
                    GameObject itemdrop = Instantiate(drop, transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject);
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            print(health);
            health--;
            if (health <= 0)
            {
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
                Destroy(this.gameObject);
            }

        }
    }




}
