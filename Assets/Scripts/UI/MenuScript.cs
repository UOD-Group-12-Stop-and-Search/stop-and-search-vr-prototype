using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject aboutMenu;
    public GameObject howToMenu;

    void Start()
    {
        aboutMenu.SetActive(false);
        howToMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void AboutButton()
    {
        aboutMenu.SetActive(true);
        howToMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void HowToButton()
    {
        aboutMenu.SetActive(false);
        howToMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        aboutMenu.SetActive(false);
        howToMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}


