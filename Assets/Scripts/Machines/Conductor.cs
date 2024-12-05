using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float beatInterval = 1f;

    private float nextBeatTime;
    public List<Machine> machines;

    public int currentBeat = 0;

    public delegate void OnBeat(int beat);
    public static event OnBeat BeatEvent;

    void Start()
    {
        nextBeatTime = Time.time + beatInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            nextBeatTime += beatInterval;
            currentBeat++;

            // Trigger global beat event
            BeatEvent?.Invoke(currentBeat);
            Debug.Log($"Beat {currentBeat} triggered");

            // Example: Use specific machines to create music
            TriggerMachinesOnSequence(currentBeat);
        }
    }

    private void TriggerMachinesOnSequence(int beat)
    {
        // Example: Trigger machines based on the beat sequence
        if (machines.Count >= 4)
        {
            if (beat % 4 == 1)
            {
                machines[0].ActivateMachine(); // First machine on beat 1
                machines[1].ActivateMachine(); // First machine on beat 1
            }
            else if (beat % 4 == 2)
            {
                machines[2].ActivateMachine(); // Third machine on beat 3
            }
            else if (beat % 4 == 0)
            {
                machines[3].ActivateMachine(); // Fourth machine on beat 4
            }
        }
    }
}
