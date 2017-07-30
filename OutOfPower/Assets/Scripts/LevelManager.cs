﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    
    public Text txPowerToGenerate;
    public Text txPowerInBattery;
    
    public Slider slPowerIndicator;
    public Slider slBatteryIndicator;

    public GameObject goInPlay;
    public GameObject goFinished;
    
    public Text txScore;

    public AudioSource asMainAudioSource;
    public AudioClip acHit;
    public AudioClip acBoostedPowerInput;
    public AudioClip acPowerInput;
    public AudioClip acRun;

    public GameObject goPlayer;
    private Vector3 MovingTo;

    private float movementAmount = 1;

    private float timeToReachMax = 1f;

    private float maxBatteryCapacity = 5;
    private float powerInBattery;

    private float score = 0;

    private State currentSate;

    private float groundSpawnTime = 2f;

    public GameObject goGroundPrefab;

    private float difficultylevel = 0.5f;

    public Image imPowerIndicator;
    private Color powerIndicatorDefaultColour;

    private bool powerButtonLocked = false;

    public Button buMorePowerButton;

    // Use this for initialization
    void Start () {
        powerInBattery = maxBatteryCapacity;
        setInPlay();
        MovingTo = goPlayer.transform.position;

        powerIndicatorDefaultColour = imPowerIndicator.color;
        buMorePowerButton.enabled = true;

        StartCoroutine("MakeGround");
    }
	
	// Update is called once per frame
	void Update () {
        if(currentSate == State.InPlay) {
            float percentToGenerate = (Mathf.PingPong(Time.time, timeToReachMax) / timeToReachMax);
            txPowerToGenerate.text = percentToGenerate * 100 + "%";

            slPowerIndicator.value = percentToGenerate;
            slBatteryIndicator.value = powerInBattery / maxBatteryCapacity;

            powerInBattery -= Time.deltaTime ;
            txPowerInBattery.text = powerInBattery + "";
            
            score += 0 + Time.deltaTime;
            txScore.text = score.ToString("F2") + "m";

            if (Input.GetKeyDown("space"))
            {
                MorePower();
            }


            float moveSpeed = 2.5f;
            float step = moveSpeed * Time.deltaTime;

            if (Input.GetKeyDown("up"))
            {
                GoUp();
            }

            if (Input.GetKeyDown("down"))
            {
                GoDown();
            }
            
            if (percentToGenerate > difficultylevel)
            {
                imPowerIndicator.color = powerIndicatorDefaultColour;
            }
            else
            {
                imPowerIndicator.color = Color.black;
            }
                

            goPlayer.transform.position = Vector3.MoveTowards(goPlayer.transform.position, MovingTo, step);

            if (powerInBattery <= 0)
                setGameOver();
        }



        
    }

    private void setGameOver()
    {
        goPlayer.GetComponent<Player>().PlayDead();
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

    public void MorePower()
    {
        if(powerButtonLocked == false)
        {
            lockPowerButton();
            float percentToGenerate = (Mathf.PingPong(Time.time, timeToReachMax) / timeToReachMax);
            txPowerToGenerate.text = percentToGenerate * 100 + "%";

            if (percentToGenerate > difficultylevel)
            {
                float maxPowerThatCanBeAdded = 2.1f;
                if (powerInBattery + maxPowerThatCanBeAdded > maxBatteryCapacity)
                    powerInBattery = maxBatteryCapacity;
                else
                    powerInBattery += maxPowerThatCanBeAdded * percentToGenerate;

                if (percentToGenerate >= 0.9f)
                {
                    asMainAudioSource.clip = acBoostedPowerInput;
                    asMainAudioSource.Play();
                }
                else
                {
                    asMainAudioSource.clip = acPowerInput;
                    asMainAudioSource.Play();
                }
            }

        }
        
    }

    public void GoUp()
    {
        Vector3 xPositive = goPlayer.transform.position;
        xPositive.x += movementAmount;
        MovingTo = xPositive;

        asMainAudioSource.clip = acRun;
        asMainAudioSource.Play();
    }

    public void GoDown()
    {
        Vector3 xNegitive = goPlayer.transform.position;
        xNegitive.x -= movementAmount;
        MovingTo = xNegitive;

        asMainAudioSource.clip = acRun;
        asMainAudioSource.Play();
    }

    internal void drainBattery()
    {
        powerInBattery -= 1f;
    }
    
    internal void PlayHit()
    {
        asMainAudioSource.clip = acHit;
        asMainAudioSource.Play();
    }

    public void RestartLevel()
    {

        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }

    IEnumerator MakeGround()
    {
        while (true)
        {
            Instantiate(goGroundPrefab, new Vector3(0f, 0f, -20f), Quaternion.identity);
            yield return new WaitForSeconds(groundSpawnTime);
        }
    }

    IEnumerator ReleasePowerLock()
    {
        yield return new WaitForSeconds(0.5f);
        powerButtonLocked = false;
        buMorePowerButton.interactable = true;
    }

    void lockPowerButton()
    {
        powerButtonLocked = true;
        buMorePowerButton.interactable = false;
        StartCoroutine("ReleasePowerLock");
    }

}

enum State { InPlay, Finished };
