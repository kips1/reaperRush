using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    private TrackGenerator trackGenerator;

    private void OnEnable()
    {
        trackGenerator = GameObject.FindObjectOfType<TrackGenerator>();
    }

    private void OnBecameInvisible()
    {
        if (Camera.main)
        {
            trackGenerator.RecycleTrack(this.gameObject);
        } 
    }
}
