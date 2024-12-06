using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compositor : MonoBehaviour
{
    
    
    [System.Serializable]
    public class MachineConfig
    {
        public Machine machine;
        public float bpm;
        public float interval;
    }

    [SerializeField]
    private MachineConfig[] machineConfigs;

    public float timeSinceLastBeat = 0.0f;
    private float beatInterval = 0.0f;
    public float maxBPM = 0.0f;

    void Start()
    {
        foreach (var config in machineConfigs)
        {
            config.interval = 60f / config.bpm;  // Interval in seconds (60 / BPM)
        }

        maxBPM = Mathf.Max(GetMachineBPMs());

        beatInterval = 60f / maxBPM;  // Global interval for the "song"
        StartCoroutine(PlaySong());
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastBeat += Time.deltaTime;

        if (timeSinceLastBeat >= beatInterval)
        {
            TriggerMachines();
            timeSinceLastBeat = 0f;
        }
    }

    private float[] GetMachineBPMs()
    {
        float[] bpmArray = new float[machineConfigs.Length];
        for (int i = 0; i < machineConfigs.Length; i++)
        {
            bpmArray[i] = machineConfigs[i].bpm;
        }
        return bpmArray;
    }

    private void TriggerMachines()
    {
        foreach (var config in machineConfigs)
        {
            // Check if it's time to trigger this machine based on its BPM interval
            Debug.Log("Time since last beat: " + timeSinceLastBeat);
            Debug.Log("Interval: " + config.interval);
            if (timeSinceLastBeat >= config.interval)
            {
                config.machine.TriggerMachine();  // Call the machine's movement function
            }
        }
    }

    private IEnumerator PlaySong()
    {
        while (true)
        {
            yield return new WaitForSeconds(beatInterval);
            TriggerMachines();
        }
    }
}
