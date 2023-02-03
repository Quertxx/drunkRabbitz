using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //simple patrol enemy - point A to point B

    public Transform[] points;
    public float speed;
    public int startPoint;

    private int p;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
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
    }


    //also some code for damaging player on collision (might be handled in the player script instead who knows

    //code for player damaging this unit with axe


}
