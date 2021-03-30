using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    private static Options _instance;
    public float masterVol;
    public float sfxVol;
    public float backgroundmusicVol;

    public string runnerName;
    public string currentRunner;

    public static Options Instance { get { return _instance; } }
    private void Awake()
    {
        // Ensures only one instance of this class is created
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        runnerName = "Runner1";
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = masterVol;
    }
}
