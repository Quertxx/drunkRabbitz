using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerSpawn : MonoBehaviour
{

    public GameObject mainmenu;
    public GameObject creditsmenu;
    public GameObject gc;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameController") == null)
        {
            Instantiate(gc, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playgame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitgame()
    {
        Application.Quit();
    }
    public void credits()
    {
        mainmenu.SetActive(false);
        creditsmenu.SetActive(true);
    }

    public void backbutton()
    {
        creditsmenu.SetActive(false);
        mainmenu.SetActive(true);
    }
}
