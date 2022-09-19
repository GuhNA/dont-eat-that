using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

     private string cena="SampleScene";
    public GameObject optionsPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

}
