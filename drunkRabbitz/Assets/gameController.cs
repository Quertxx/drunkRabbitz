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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Update()
    {
      playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
      playerPos = GameObject.FindGameObjectWithTag("Player").transform;
      currentScene = SceneManager.GetActiveScene().buildIndex;

      if (currentScene == 1 && playerScript.Health <= 0)
        {
            StartCoroutine(checkpointDelay(0.25f));
        }
    }

    public void playgame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitgame()
    {
        Application.Quit();
    }

    IEnumerator checkpointDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerPos.position = new Vector3(lastCheckpoint.position.x, lastCheckpoint.position.y, 0);
        playerScript.Health = 100;
    }

}
