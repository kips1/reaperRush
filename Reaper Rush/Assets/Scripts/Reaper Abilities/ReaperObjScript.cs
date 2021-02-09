using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Kips
 * 
 * Deals with the behaviour of the reaper objects
 * 
 * Version:
 * 
 */

public class ReaperObjScript : MonoBehaviourPun
{
    public GameObject runner;

    private ReaperObj generate;

    private Renderer render;

    // Start is called before the first frame update
    // Initiate the fields
    void Start()
    {
        generate = GetComponentInParent<ReaperObj>();
        render = GetComponent<Renderer>();
        runner = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    // Removes the objects that are spawned
    void Update()
    {

        if (render.transform.position.z < runner.transform.position.z - 100)
        {
            if (photonView.IsMine) {
            PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    // Triggers damage function when runner instance collides reaper object
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
