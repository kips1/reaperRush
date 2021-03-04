using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Sharp Coder, Josh, Kips
 * 
 * The script that handles all network activity for syncing players
 *
 * 
 */

public class PUN2_PlayerSync : MonoBehaviourPun, IPunObservable
{
    // List of the scripts that should only be active for the local player (ex. PlayerController, MouseLook etc.)
    public MonoBehaviour[] localScripts;
    // List of the GameObjects that should only be active for the local player (ex. Camera, AudioListener etc.)
    public GameObject[] localObjects;
    // Values that will be synced over network
    private Vector3 latestPos;
    private Vector3 positionAtLastPacket = Vector3.zero;

    // Values that will be used to account for lag over network
    private float currentTime = 0;
    private double currentPacketTime = 0;
    private double lastPacketTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            //Player is Remote, deactivate the scripts and object that should only be enabled for the local player
            for (int i = 0; i < localScripts.Length; i++)
            {
                localScripts[i].enabled = false;
            }
            for (int i = 0; i < localObjects.Length; i++)
            {
                localObjects[i].SetActive(false);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
        }
        else
        {
            //Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();

            // Account for lag when receving other player's position
            currentTime = 0.0f;
            positionAtLastPacket = transform.position;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            // Calculate lag duration
            double timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;

            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, (float)(currentTime / timeToReachGoal));
        }
    }
}