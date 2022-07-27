using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMana : MonoBehaviour
{
    // vars
    // time since start
    float timeF;
    // speed
    // level of speed (0 = 0.5 / 1 = 1 / 2 = 1.5 / 3 = 2 / 4 = 3)
    int CurrentSpeedLevel;
    Dictionary<int, float> DicLevelTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        // set speed to "normal" TimeScale = 1
        this.CurrentSpeedLevel = 1;
        // init the dic
        DicLevelTimeScale = new Dictionary<int, float>();
        DicLevelTimeScale.Add(0, 0.5f);
        DicLevelTimeScale.Add(1, 1f);
        DicLevelTimeScale.Add(2, 1.5f);
        DicLevelTimeScale.Add(3, 2f);
        DicLevelTimeScale.Add(4, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : Change to date
        this.timeF = Time.time;
    }

    public void SlowDownTime()
    {
        if(CurrentSpeedLevel <= 0)
        {
            // can't slow time, do nothing
        } else {
            // Slow time by decreasing level and applying to TimeScale
            this.CurrentSpeedLevel -= 1;
            Time.timeScale = this.DicLevelTimeScale[this.CurrentSpeedLevel];
        }
    }

    public void SpeedUpTime()
    {
        if(CurrentSpeedLevel >= 4)
        {
            // can't Speed up time, do nothing
        } else {
            // Speed up time by increasing level and applying to TimeScale
            this.CurrentSpeedLevel += 1;
            Time.timeScale = this.DicLevelTimeScale[this.CurrentSpeedLevel];
        }
    }
}
