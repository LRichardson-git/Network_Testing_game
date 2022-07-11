using FishNet;
using UnityEngine;
using UnityEngine.UI;

public class Menu_ : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button HostButton;

    [SerializeField]
    private Button ConnectButton;


    private void Start()
    {
        HostButton.onClick.AddListener(() => //using server manager and client manager to host game
        {
            InstanceFinder.ServerManager.StartConnection(); // start server
            InstanceFinder.ClientManager.StartConnection(); // connect self

        });

        ConnectButton.onClick.AddListener(() => InstanceFinder.ClientManager.StartConnection());
    }



}
