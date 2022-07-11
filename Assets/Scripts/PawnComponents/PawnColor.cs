using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
public sealed class PawnColor :NetworkBehaviour
{


    [SyncVar(OnChange = nameof(OnChangeBody))]
    public Color MatColor;

    [SyncVar(OnChange = nameof(OnChangeGun))]
    public Color GunColor;

    [SerializeField]
    private GameObject Body;

    [SerializeField]
    private GameObject Gun;


    //these are called by the server to everything when the sync value changes
    public void OnChangeBody(Color oldValue, Color newValue, bool isServer)
    {
        Body.GetComponent<SpriteRenderer>().color = newValue;
    }

    private void OnChangeGun(Color oldValue, Color newValue, bool isServer)
    {
        Gun.GetComponent<SpriteRenderer>().color = newValue;
    }


    public void SetColors(Color BodyCol, Color GunCol)
    {
        MatColor = BodyCol;
        GunColor = GunCol;
    }


}