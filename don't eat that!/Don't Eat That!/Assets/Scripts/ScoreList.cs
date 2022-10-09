using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class ScoreList : MonoBehaviour
{
    public Text score;


    private void Start()
    {
        score.text = PlayerPrefs.GetString("Profile") + ": " + PlayerPrefs.GetInt("score");
    }


    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

}
