using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager SharedInstance;

    public GameObject pauseMenu;

    public int giftCount;
    public bool blinkStatus;
    public int playerLifes = 3;
    public bool isInvulnerable = false;
    public bool gameIsPaused = false;
    private void Awake()
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
            // Keep this object even when we switch scenes
            DontDestroyOnLoad(gameObject);
        }

    }
    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
    private void Update()
    {
        GameObject pauseScreen = FindObject(GameObject.Find("Canvas"), "PauseScreen");
        if (pauseMenu == null && pauseScreen != null)
        {
            pauseMenu = pauseScreen;
        }
    }

    public void ResetHUD()
    {
        playerLifes = 3;
        giftCount = 0;
        blinkStatus = true;

        HUDManager.SharedInstance.UpdateGiftCount();
        HUDManager.SharedInstance.UpdateBlinkStatus();
    }

    public void TakeDamage()
    {
        playerLifes--;
        HUDManager.SharedInstance.DeactivateLife(playerLifes);
    }

    void Start()
    {
        UnPauseGame();
        blinkStatus = true;
        //HUDManager.SharedInstance.UpdateBlinkStatus();
    }

    public void GameOver()
    {
        ResetHUD();
        SceneManager.LoadScene("GameOver");
    }

    public void Pause()
    {
        Debug.Log("pausinggg...");
        Debug.Log(gameIsPaused);
        if (gameIsPaused)
        {
            Resume();
            return;
        }

        pauseMenu.SetActive(true);
        PauseGame();
    }

    public void Resume()
    {
        UnPauseGame();
        pauseMenu.SetActive(false);
        
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
