using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveAudio : BaseAirplaneAudio
{
    public float minDivePitch,
    maxDivePitch,
    minDiveVolume,
    maxDiveVolume;

    private float lastDiveFactor = 0f;

    public int movingAverageLenght = 20;
    private int count = 0;
    private float movingAverage = 0f;

    void Start()
    {
        audioSource.volume = 0f;
    }

    public void HandleDiveAudio(float diveFactor)
    {
        if (diveFactor > lastDiveFactor)
        {
            audioSource.pitch = Mathf.Lerp(maxDivePitch, minDivePitch, diveFactor);
            lastDiveFactor = diveFactor;
        }
        else if (diveFactor < 0.05f)
        {
            audioSource.pitch = Mathf.Lerp(maxDivePitch, minDivePitch, diveFactor);;
            lastDiveFactor = diveFactor;
        }

        audioSource.volume = Mathf.Lerp(minDiveVolume, maxDiveVolume, diveFactor);
    }

    void CalculateAngularMovingAverage(float newValue)
    {
        count++;

        //This will calculate the MovingAverage AFTER the very first value of the MovingAverage
        if (count > movingAverageLenght)
        {
            movingAverage = movingAverage + (newValue - movingAverage) / (movingAverageLenght + 1);

            //Debug.Log("Moving Average: " + movingAverage); //for testing purposes

        }
        else
        {
            //NOTE: The MovingAverage will not have a value until at least "MovingAverageLength" values are known (10 values per your requirement)
            movingAverage += newValue;

            //This will calculate ONLY the very first value of the MovingAverage,
            if (count == movingAverageLenght)
            {
                movingAverage = movingAverage / count;
                //Debug.Log("Moving Average: " + movingAverage); //for testing purposes
            }
        }
    }
}
