using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    private int currentScene;

    public Transform lastCheckpoint;
    private GameObject startPosition;
    public PlayerMovement playerScript;
    public Transform playerPos;
    private bool exists = false;

    private void Awake()
    {

        SceneManager.sceneLoaded += onSceneLoaded;
         DontDestroyOnLoad(this.gameObject);

    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        startPosition = GameObject.FindGameObjectWithTag("startPos");
        lastCheckpoint = startPosition.transform;
        if(currentScene == 0)
        {
            playerScript = null;
        }
        if (playerScript == null && currentScene >= 1)
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (playerScript != null)
        {
            playerPos.position = new Vector3(lastCheckpoint.position.x, lastCheckpoint.position.y, 0);
        }

        print(currentScene);

    }

    void Update()
    {

        if (playerScript !=null )
        {
            if (currentScene >= 1 && playerScript.Health <= 0)
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
