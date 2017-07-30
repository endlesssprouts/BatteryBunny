using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public bool isForward = true;
    private Vector3 forceToApply;
    public bool isBattery = false;
    public bool isTutorial = false;

    // Use this for initialization
    void Start () {
        if (isForward)
            forceToApply = Vector3.forward * 5.0f;
        else
            forceToApply = Vector3.back * 5.0f;
        
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(forceToApply * Time.deltaTime );

        if(isBattery == false)
        {
            if (transform.position.z >= 20f)
                Destroy(gameObject);
        }
        else
        {
            if(isTutorial == false)
            {
                if (transform.position.z >= 20f)
                    transform.position = new Vector3(transform.position.x, transform.position.y, -20f);
            }
            else
            {
                if (transform.position.z >= 20f)
                    Destroy(gameObject);
            }
        }


    }
    
}
