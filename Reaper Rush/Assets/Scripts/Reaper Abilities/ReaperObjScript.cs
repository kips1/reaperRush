﻿using System.Collections;
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

        if (render.transform.position.z < runner.transform.position.z - 100)
        {
            if (photonView.IsMine) {
            PhotonNetwork.Destroy(gameObject);
            }
        }

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if (runner.GetComponent<Runner>().takeDamage == true)
            {
                runner.GetComponent<Runner>().TakeDamage(1);
            }
            runner.GetComponent<Runner>().anim.SetTrigger("Collide");
        }
    }
}