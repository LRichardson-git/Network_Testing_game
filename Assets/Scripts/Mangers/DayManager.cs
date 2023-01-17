
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
public sealed class DayManager : NetworkBehaviour
{
    // Start is called before the first frame update

    [SyncVar(OnChange = nameof(OnChangeDay))]
    public bool day;

    [SyncVar]
    int time;

    float ticktimer;
    float tick;

    [SerializeField]
    GameObject Light;

    public static DayManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        day = true;
        time = 0;
        tick = 5;
    }

    void FixedUpdate()
    {

        if (!IsServer && Enemey_Manager.Instance.start == false)
            return;

        ticktimer += Time.deltaTime;

        if (ticktimer > tick)
        {
            
            Debug.Log("new day");
            day = !day;
            ticktimer -= tick;
        }
    }

        [ServerRpc]
        public void changeDay()
    {
        day = !day;
    } 


        public void OnChangeDay(bool oldValue, bool newValue, bool isServer)
        {
        Light.SetActive(newValue);
        Debug.Log("day = " + newValue);
        }

    
}
