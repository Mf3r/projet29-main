using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;     //D'ou viens le SceneManagement
using UnityEngine;

public class Load_Game_Play : MonoBehaviour
{
    public void ClickonPlay()    //a approfondir sur le clickonplay(commande de base ?) (pourquoi public void?)
    {
        SceneManager.LoadScene("SCENE");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
