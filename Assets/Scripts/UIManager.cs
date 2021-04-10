using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauesmenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    void Resume()
    {
        pauesmenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Paused()
    {
        pauesmenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
