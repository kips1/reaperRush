using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Sharp Coder
 * 
 * The script that handles all network activity for syncing players
 *
 * 
 */

public class PUN2_PlayerSync : MonoBehaviourPun, IPunObservable
{
    //List of the scripts that should only be active for the local player (ex. PlayerController, MouseLook etc.)
    public MonoBehaviour[] localScripts;
    //List of the GameObjects that should only be active for the local player (ex. Camera, AudioListener etc.)
    public GameObject[] localObjects;
    //Values that will be synced over network
    Vector3 latestPos;
    Quaternion latestRot;
    float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
    Quaternion rotationAtLastPacket = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //Player is local
            //gameObject.tag = "Player";
            //Add Rigidbody to make the player interact with rigidbody
            //Rigidbody r = gameObject.AddComponent<Rigidbody>();
            //r.isKinematic = true;
        }
        else
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
            //stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();
            //latestRot = (Quaternion)stream.ReceiveNext();

            currentTime = 0.0f;
            positionAtLastPacket = transform.position;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
            //rotationAtLastPacket = transform.rotation;
        }
        //lag = Mathf.Abs(((float)PhotonNetwork.Time - info.SentServerTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            double timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;

            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, (float)(currentTime / timeToReachGoal));
            //transform.rotation = Quaternion.Lerp(rotationAtLastPacket, latestRot, (float)(currentTime / timeToReachGoal));
        }
    }
}