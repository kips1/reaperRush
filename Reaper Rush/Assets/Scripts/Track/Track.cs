using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Josh
 * 
 * Handles the reference and recycling of an instance of a track 
 * 
 * Version:
 * 
 */

public class Track : MonoBehaviour
{
    private TrackGenerator trackGenerator;

    // Links the object when it is available 
    private void OnEnable()
    {
        trackGenerator = GameObject.FindObjectOfType<TrackGenerator>();
    }

    // Recycles instances of a track when it becomes invisible to the player
    private void OnBecameInvisible()
    {
        trackGenerator.RecycleTrack(this.gameObject);
    }
}