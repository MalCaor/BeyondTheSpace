using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMana : MonoBehaviour
{
    // vars
    // time since start
    float timeF;
    // speed
    float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        this.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.timeF = Time.time;
    }

    public void SlowDownTime()
    {
        this.timeScale = this.timeScale/2;
        Time.timeScale = this.timeScale;
    }

    public void SpeedUpTime()
    {
        this.timeScale = this.timeScale*2;
        Time.timeScale = this.timeScale;
    }
}
