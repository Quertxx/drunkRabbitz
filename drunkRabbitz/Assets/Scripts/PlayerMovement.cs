using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 mousePos;
    public GameObject player;
    public Rigidbody2D playerRB;
    private float jumpTimer;
    public float maxjumpTimer = 1000;
    public float jumpMultiplyer;
    private Vector2 dir;
    public GameObject axe;
    public bool isRotating = false;
    private bool isJumping = false;
    public float Health = 100;
    public float maxHealth = 100;
    private Slider healthBar;
    private Slider jumpBar;
    private SpriteRenderer playerspriteRender;
    private Sprite playerspriterender;
    public int carrots = 0;
    private bool facingRight;
    private TMP_Text carrotCounter;
    private bool isCharging = false;

    public GameObject groundCheck;
    public float checkRadius;
    public LayerMask groundLayers;

    public Animator myAnim;
    public AudioSource boingSound;
    // Start is called before the first frame update
    void Start()
    {

        playerRB = gameObject.GetComponent<Rigidbody2D>();
        healthBar = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Slider>();
        jumpBar = GameObject.Find("JumpSlider").GetComponent<Slider>();
        carrotCounter = GameObject.Find("Carrot, Counter").GetComponent<TextMeshProUGUI>();
        playerspriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        var mousePosFloat = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        isJumping = Physics2D.OverlapCircle(groundCheck.transform.position, checkRadius, groundLayers);
        if (isJumping)
        {
            myAnim.SetBool("isJumping", false);
        }
        else
        {
            myAnim.SetBool("isJumping", true);
        }
        if(mousePosFloat.x >= 0 && !facingRight)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        else if(mousePosFloat.x < 0 && facingRight)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isJumping)
            {
                boingSound.Play();
            }
            isCharging = false;
            isJumping = true;
            myAnim.SetBool("isCharging", false);
            myAnim.SetBool("isJumping", true);
            //myAnim.SetBool("isJumping", true);
            playerRB.AddForce(dir.normalized * jumpTimer);
            jumpTimer = 0;
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isRotating)
        {
            //axe.transform.RotateAround(gameObject.transform.position, Vector3.back, 360 * Time.deltaTime);
            //axe.transform.Rotate(0, 0, 90);
            StartCoroutine(rotateBack(0.2f));
        }


        healthBar.value = Health/maxHealth;
        carrotCounter.text = (""+carrots);

        /*if(Health <= 0)
        {
            SceneManager.LoadScene(1);
        }*/
    }

    private void FixedUpdate()
    {

        jumpBar.value = jumpTimer / maxjumpTimer;
        if (Input.GetKey(KeyCode.Mouse0) && isJumping)
        {
            isCharging = true;
            myAnim.SetBool("isCharging", true);
            dir = (mousePos - gameObject.transform.position).normalized;
            jumpTimer = (Mathf.Clamp(jumpTimer, 0, maxjumpTimer) + jumpMultiplyer);
        }
        else
        {
            myAnim.SetBool("isCharging", false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            //isJumping = false;
            

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if(isJumping == true)
            {
                //isJumping = false;
                //myAnim.SetBool("isJumping", false);
            }
            
        }
    }

    IEnumerator rotateBack(float duration)
    {
        isRotating = true;
        float t = 0f;
        int angle;
        int spinCount = 0;
        spinCount++;
        if(player.transform.localScale.x >= 0)
        {
            angle = -360;
        }
        else
        {
            angle = 360;
        }
        //float rot = Mathf.Lerp(axe.transform.rotation.z, angle, t);
        Quaternion target = axe.transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward);

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float rot = Mathf.Lerp(axe.transform.rotation.z, angle, t);
            Quaternion axeRot = Quaternion.Euler(0, 0, rot);//Quaternion.Lerp(axe.transform.rotation, target, t);
            axe.transform.rotation = axeRot;
            yield return null;
        }

        isRotating = false;
     
    }
}
