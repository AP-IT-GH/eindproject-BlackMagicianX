using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExitEvent : MonoBehaviour
{
    public void button_exit()
    {

        EditorApplication.isPlaying = false;
        Application.Quit();

    }
}
