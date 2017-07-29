using System;
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

    private float maxBatteryCapacity = 10;
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


            float moveSpeed = 2f;
            float step = moveSpeed * Time.deltaTime;

            if (Input.GetKeyDown("up"))
            {
                GoUp();
            }

            if (Input.GetKeyDown("down"))
            {
                GoDown();
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

    public void MorePower()
    {

        float percentToGenerate = (Mathf.PingPong(Time.time, timeToReachMax) / timeToReachMax);
        txPowerToGenerate.text = percentToGenerate * 100 + "%";

        float maxPowerThatCanBeAdded = 3f;
        if (powerInBattery + maxPowerThatCanBeAdded > maxBatteryCapacity)
            powerInBattery = maxBatteryCapacity;
        else
            powerInBattery += maxPowerThatCanBeAdded * percentToGenerate;

        if (percentToGenerate >= 0.8f)
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

}

enum State { InPlay, Finished };
