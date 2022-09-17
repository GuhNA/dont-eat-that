using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Fluxo : MonoBehaviour
{
    public void CarregarJogo()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Clicou");
    }
}
