using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    public void Exit()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
