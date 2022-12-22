using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
