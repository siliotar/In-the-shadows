using UnityEngine;
using System;

public class ClockController : MonoBehaviour
{
    public GameObject HourArrow;
    public GameObject MinuteArrow;

    private void FixedUpdate()
    {
        DateTime now = DateTime.Now;

        float minute = now.Minute + now.Second / 60.0f + now.Millisecond / 60000.0f;
        float minuteAngle = minute * 6.0f;
        MinuteArrow.transform.eulerAngles = new Vector3(MinuteArrow.transform.eulerAngles.x, MinuteArrow.transform.eulerAngles.y, minuteAngle);
        float hourAngle = (now.Hour + minute / 60.0f) * 30.0f;
        HourArrow.transform.eulerAngles = new Vector3(HourArrow.transform.eulerAngles.x, HourArrow.transform.eulerAngles.y, hourAngle);
    }
}
