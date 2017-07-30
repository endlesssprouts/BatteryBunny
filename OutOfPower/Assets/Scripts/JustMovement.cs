using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMovement : MonoBehaviour
{
    public bool isForward = true;
    private Vector3 forceToApply;
    public bool isBattery = false;
    public bool isTutorial = false;

    public TitleManager lmLevelInfo;

    // Use this for initialization
    void Start () {
        if (isForward)
            forceToApply = Vector3.forward * 5.0f;
        else
            forceToApply = Vector3.back * 5.0f;
        
    }

    void SetLevelManager(TitleManager mainLevelManager)
    {
        lmLevelInfo = mainLevelManager;
    }

    // Update is called once per frame
    void Update()
    {

        if (isBattery == false)
        {
            if (transform.position.z >= 20f)
                Destroy(gameObject);
        }
        else
        {
            if (isTutorial == false)
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
