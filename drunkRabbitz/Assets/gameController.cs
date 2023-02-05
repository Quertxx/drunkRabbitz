using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    private int currentScene;

    public Transform lastCheckpoint;

    PlayerMovement playerScript;
    Transform playerPos;
    private bool exists = false;

    private void Awake()
    {
        
        
         DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        if(playerScript ==null && currentScene == 1)
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            print(playerPos);
        }
      
      currentScene = SceneManager.GetActiveScene().buildIndex;
        if (playerScript !=null ){
            if (currentScene == 1 && playerScript.Health <= 0)
            {
                StartCoroutine(checkpointDelay(0.25f));
            }
        }  

    }



    IEnumerator checkpointDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerPos.position = new Vector3(lastCheckpoint.position.x, lastCheckpoint.position.y, 0);
        playerScript.Health = 100;
    }

}
