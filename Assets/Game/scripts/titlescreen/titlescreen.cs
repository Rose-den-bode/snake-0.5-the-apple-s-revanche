using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlescreen : MonoBehaviour
{
    public GameObject mainMenu; // Referentie naar het MainMenu paneel
    

    public void playGame(){
        
        
            
        
        SceneManager.LoadScene("level");
    }   

    public void quitGame(){
        Application.Quit();
    }

    

    public void MainMenu()
    {
        SceneManager.LoadScene("homescreen");
    }
}
