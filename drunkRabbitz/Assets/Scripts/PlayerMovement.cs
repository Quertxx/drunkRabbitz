using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 mousePos;
    public GameObject player;
    private Rigidbody2D playerRB;
    private float jumpTimer;
    public float jumpMultiplyer;
    private Vector2 dir;
    public GameObject axe;
    public Transform axeRestLoc;
    private bool isRotating = false;
    // Start is called before the first frame update
    void Start()
    {

        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            playerRB.AddForce(dir * jumpTimer);
            jumpTimer = 0;
            
        }

        if (Input.GetKey(KeyCode.Mouse1) && !isRotating)
        {
            //axe.transform.RotateAround(gameObject.transform.position, Vector3.back, 360 * Time.deltaTime);
            //axe.transform.Rotate(0, 0, 90);
            StartCoroutine(rotateBack(0.5f));
        }


    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            dir = (mousePos - gameObject.transform.position).normalized;
            print(dir);
            jumpTimer = (Mathf.Clamp(jumpTimer, 0, 1470)+ jumpMultiplyer);
            print(jumpTimer);
        }
    }

    IEnumerator rotateBack(float duration)
    {
        isRotating = true;
        float t = 0f;
        Quaternion target = axe.transform.rotation * Quaternion.AngleAxis(180, Vector3.forward);
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            axe.transform.rotation = Quaternion.Lerp(axe.transform.rotation, target, t);
            yield return null;
        }
        isRotating = false;
     
    }
}
