using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
    
    public void StartLevel()
    {

        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene("Scenes/Main");
    }
    


}

