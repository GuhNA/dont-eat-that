using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileKeep : MonoBehaviour
{
    public InputField input;


    public void KeepName()
    {
        PlayerPrefs.SetString("Profile", input.text);
    }
}
