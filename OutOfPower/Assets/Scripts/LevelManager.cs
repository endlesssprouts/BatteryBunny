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

    public GameObject goPlayer;
    private Vector3 MovingTo;

    private float maxBatteryCapacity = 5;
    private float powerInBattery;

    private float score = 0;

    private State currentSate;

    // Use this for initialization
    void Start () {
        powerInBattery = maxBatteryCapacity;
        setInPlay();
        MovingTo = goPlayer.transform.position;

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


            float moveSpeed = 2f;
            float movementAmount = 1;
            float step = moveSpeed * Time.deltaTime;

            if (Input.GetKeyDown("up"))
            {
                Vector3 xPositive = goPlayer.transform.position;
                xPositive.x += movementAmount;
                MovingTo = xPositive;
            }

            if (Input.GetKeyDown("down"))
            {
                Vector3 xNegitive = goPlayer.transform.position;
                xNegitive.x -= movementAmount;
                MovingTo = xNegitive;
            }


            goPlayer.transform.position = Vector3.MoveTowards(goPlayer.transform.position, MovingTo, step);

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
    
    internal void drainBattery()
    {
        powerInBattery -= 1f;
    }

}

enum State { InPlay, Finished };
