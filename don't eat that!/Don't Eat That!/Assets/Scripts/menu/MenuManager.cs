using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

     private string cena="MainSceneGame";
     private string returnCena="Menu";
    public GameObject optionsPanel;



    public void StartGame()
    {
        SceneManager.LoadScene(cena);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Aplication.Quit();
    }

    public void ShowSobre()
    {
        optionsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        optionsPanel.SetActive(false);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(returnCena);
    }
}
