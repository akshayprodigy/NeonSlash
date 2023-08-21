using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatObjectSpawner : MonoBehaviour
{
    public GameObject beatObjectPrefab;
    public float beatThreshold = 0.1f;

    private List<float> beatTimes = new List<float>();
    private int beatIndex = 0;
    private float sampleRate;
    [SerializeField]
    private GameObject[] SpawnPoints;

    public float minInterval = 0.5f;  // Minimum time interval in seconds
    public float maxInterval = 0.9f;
    public bool isMusicPlaying = false;

    void Start()
    {
        // Retrieve the sample rate on the main thread
        sampleRate = AudioSettings.outputSampleRate;
        PlayMusic();
    }

    public void PlayMusic()
    {
        isMusicPlaying = true;
        StartCoroutine(RandomCallCoroutine());
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            float sample = data[i];

            // Check for beat detection
           /* Debug.Log("sample: " + sample + " i: " + i );
            if(i > 0)
            {
                Debug.Log(" data[i - channels]: " + data[i - channels]);
            }*/

            if (sample > beatThreshold && (i == 0 || data[i - channels] <= beatThreshold))
            {
                float time = (float)i / channels / sampleRate;
                beatTimes.Add(time);
            }
        }
    }

    //void Update()
    //{
    //    if (beatIndex < beatTimes.Count && Time.time >= beatTimes[beatIndex])
    //    {
    //        SpawnBeatObject();
    //        beatIndex++;
    //    }
    //}


    private IEnumerator RandomCallCoroutine()
    {
        while (isMusicPlaying)
        {
            // Calculate a random interval
            float randomInterval = Random.Range(minInterval, maxInterval);

            // Wait for the random interval
            yield return new WaitForSeconds(randomInterval);

            // Call the function
            SpawnBeatObject();
        }
    }

    void SpawnBeatObject()
    {
        // Debug.Log("SpawnBeatObject");
        SpawnPoints[Random.Range(0, SpawnPoints.Length)].GetComponent<SpawnObjects>().SpawnObject();

    }
}
