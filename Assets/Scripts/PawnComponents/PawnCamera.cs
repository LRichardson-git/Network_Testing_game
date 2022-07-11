using FishNet.Object;
using UnityEngine;

public sealed class PawnCamera : NetworkBehaviour 
{
    [SerializeField]
    public Transform myCamera;



    public override void OnStartClient() //called on client start and object is fully initilazed
    {
        base.OnStartClient();


        //makes it so in client unless you own that object the camera and audio sources from that object are disabled
        myCamera.GetComponent<Camera>().enabled = IsOwner;

        myCamera.GetComponent<AudioListener>().enabled = IsOwner;

    }

}
