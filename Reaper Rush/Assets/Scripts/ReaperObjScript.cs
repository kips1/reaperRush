using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReaperObjScript : MonoBehaviourPun
{
    private ReaperObj generate;
    private Renderer render;
    public GameObject runner;
    // Start is called before the first frame update
    void Start()
    {
        generate = GetComponentInParent<ReaperObj>();
        render = GetComponent<Renderer>();
        runner = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (render.transform.position.z < runner.transform.position.z - 40)
        {
            if (photonView.IsMine) {
            PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
