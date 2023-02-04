using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 mousePos;
    public GameObject player;
    private Rigidbody2D playerRB;
    private float jumpTimer;
    private float maxjumpTimer = 1500;
    public float jumpMultiplyer;
    private Vector2 dir;
    public GameObject axe;
    private bool isRotating = false;
    private bool isJumping = false;
    public float Health = 100;
    public float maxHealth = 100;
    private Slider healthBar;
    private Slider jumpBar;
    private SpriteRenderer playerspriteRender;
    // Start is called before the first frame update
    void Start()
    {

        playerRB = gameObject.GetComponent<Rigidbody2D>();
        healthBar = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Slider>();
        jumpBar = GameObject.Find("JumpSlider").GetComponent<Slider>();
        playerspriteRender = gameObject.GetComponent<SpriteRenderer>();
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
            isJumping = true;
            
        }

        if (Input.GetKey(KeyCode.Mouse1) && !isRotating)
        {
            //axe.transform.RotateAround(gameObject.transform.position, Vector3.back, 360 * Time.deltaTime);
            //axe.transform.Rotate(0, 0, 90);
            StartCoroutine(rotateBack(0.5f));
        }

        healthBar.value = Health/maxHealth;
        jumpBar.value = jumpTimer / maxjumpTimer;

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isJumping == false)
        {
            dir = (mousePos - gameObject.transform.position).normalized;
            jumpTimer = (Mathf.Clamp(jumpTimer, 0, 1470)+ jumpMultiplyer);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
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
