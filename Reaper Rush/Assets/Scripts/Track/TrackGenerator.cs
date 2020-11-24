using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] trackPrefabs;
    [SerializeField] private double zOffset;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < trackPrefabs.Length; i++)
        {
            Instantiate(trackPrefabs[i], new Vector3(0, 0, i * 43.4f), Quaternion.Euler(0, 0, 0));
            zOffset += 43.4;
        }
    }

    public void RecycleTrack(GameObject track)
    {
        track.transform.position = new Vector3(0, 0, (float)zOffset);
        zOffset += 43.4;
    }
}
