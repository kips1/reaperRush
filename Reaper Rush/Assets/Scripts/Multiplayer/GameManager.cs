using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int round;
    private int distanceScore;
    private int coinsCollected;
    public bool gameEnded;

    private GameObject runner;
    private GameObject rmController;

    // Start is called before the first frame update
    void Start()
    {
        rmController = GameObject.FindGameObjectWithTag("RoomController");
        gameEnded = false;
        round = 1;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
        {
            runner = GameObject.FindGameObjectWithTag("Player");

            if (runner.GetComponent<Player>().hasLost && round == 1)
            {
                round++;
                gameEnded = true;
                runner.GetComponent<Player>().Reset();
                Destroy(runner);
            }

        }


    }

    public void Test()
    {
        Debug.Log("Yes");
    }
}
