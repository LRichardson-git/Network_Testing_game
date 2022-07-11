
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.AddressableAssets;
public sealed class Lobby_Pawn : NetworkBehaviour
{



    [SyncVar(OnChange = nameof(OnChangeBody))]
    public Color MatColor;

    [SyncVar(OnChange = nameof(OnChangeGun))]
    public Color GunColor;

    [SerializeField]
    private GameObject Body;

    [SerializeField]
    private GameObject Gun;
    //tell server to change thsese

    private void Awake()
    {
        MatColor = Body.GetComponent<SpriteRenderer>().color;
        GunColor = Gun.GetComponent<SpriteRenderer>().color;
    }

    public void OnChangeBody(Color oldValue, Color newValue, bool isServer)
    {
        Body.GetComponent<SpriteRenderer>().color = newValue;
    }

    private void OnChangeGun(Color oldValue, Color newValue, bool isServer)
    {
        Gun.GetComponent<SpriteRenderer>().color = newValue;
    }

    
    [ServerRpc] //server Rpc because we are changing the sync vars on the server, which means that all of the objects in the clients with will change colour 
    public void RandomBodyColor()
    {
        MatColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    [ServerRpc]
    public void RandomGunColor()
    {
        GunColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

}
