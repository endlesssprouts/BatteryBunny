using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
   
    public LevelManager lmLevelManager;
    
    public Animator anim;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            anim.Play("Bunny|Hit");
            lmLevelManager.drainBattery();

            lmLevelManager.PlayHit();
        }
    }
}
