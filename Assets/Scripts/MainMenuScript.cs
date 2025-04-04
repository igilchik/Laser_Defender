using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour {
    
    void Start()
    {

    }

    void Update()
    {

    }

    public void startGame() {
        SceneManager.LoadScene("GameScene");
    }
    public void GameOver() {
        // funguje len vo vybuildovanej aplikácii
        Application.Quit (); 
        Debug.Log("Hra skončila");
    }
}
