using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class RhythmGameLogic : MonoBehaviour
{
    public GameObject target;
    public AudioSource songSource;
    [SerializeField] private int BPM = 122;

    private int previousBeat = -1;
    private double audioStartTime;

    void Start()
    {
        double currentAudioTime = AudioSettings.dspTime;

        Debug.Log(currentAudioTime);

        audioStartTime = Mathf.FloorToInt((float)(currentAudioTime) + 3);


        songSource.PlayScheduled(audioStartTime);
    }

    void Update()
    {
        // use the unity time for now, even though it WOULD desync from the song
        //float time = Time.time;   


        double time = AudioSettings.dspTime;
        float toMinutes = (float) (time / 60);

        int beat = Mathf.FloorToInt(toMinutes * BPM);

        if (previousBeat != beat)
        {
            target.GetComponent<Renderer>().material.color = beat % 2 != 0 ? Color.red : Color.blue;

            previousBeat = beat;

        }

        Debug.Log($"Time: {time} Beat: {beat}");
    }
}
