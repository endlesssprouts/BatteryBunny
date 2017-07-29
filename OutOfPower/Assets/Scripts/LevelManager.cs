using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    
    public Text txPowerToGenerate;
    public Text txPowerInBattery;

    public GameObject goInPlay;
    public GameObject goFinished;
    
    public Text txScore;

    private float maxBatteryCapacity = 5;
    private float powerInBattery;

    private float score = 0;

    private State currentSate;

    // Use this for initialization
    void Start () {
        powerInBattery = maxBatteryCapacity;
        setInPlay();

    }
	
	// Update is called once per frame
	void Update () {
        if(currentSate == State.InPlay) {
            float timeToReachMax = 1f;
            float percentToGenerate = (Mathf.PingPong(Time.time, timeToReachMax) / timeToReachMax);
            txPowerToGenerate.text = percentToGenerate * 100 + "%";
        
            powerInBattery -= Time.deltaTime ;
            txPowerInBattery.text = powerInBattery + "";
            
            score += 0 + Time.deltaTime;
            txScore.text = score + "";

            float maxPowerThatCanBeAdded = 1f;
            if (Input.GetKeyDown("space"))
            {
                if (powerInBattery + maxPowerThatCanBeAdded > maxBatteryCapacity)
                    powerInBattery = maxBatteryCapacity;
                else
                    powerInBattery += maxPowerThatCanBeAdded * percentToGenerate;

            }


            if (powerInBattery <= 0)
                setGameOver();
        }



        
    }

    private void setGameOver()
    {
        currentSate = State.Finished;
        goInPlay.SetActive(false);
        goFinished.SetActive(true);
    }

    private void setInPlay()
    {
        currentSate = State.InPlay;
        goInPlay.SetActive(true);
        goFinished.SetActive(false);
    }
}

enum State { InPlay, Finished };
