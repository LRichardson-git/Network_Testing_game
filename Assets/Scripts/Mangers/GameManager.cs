using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using UnityEngine;
//since network behaviour will be synconized across the network
public sealed class GameManager : NetworkBehaviour 
{
    public static GameManager Instance { get; private set; }


    [SyncObject]
    public readonly SyncList<Player_> players = new SyncList<Player_>(); //need to mark any custom sync type as syncobject
                                                                         //since read only unity will not serialize it
    [SerializeField]
    private Vector3[] LobbySpawns;

    [SerializeField]
    private Vector3[] Playerspawns;

    [SyncVar]
    public bool canstart; //can only update syncvars in server

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (!IsServer) return; //if not the server exit immeditly


        canstart = players.All(player => player.isReady); //itterate over a collection and iff all items match condition it returns true, so if all players ready this is true

        

        //Debug.Log($"Can Start = {canstart}");
    }



    [Server] //only executed in server  since game manager is run on server
    public void StartGame() //tell players game has started and spawn pawn // IS RUN BY THE SERVER
    {
        if (!canstart) return;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].StartGame(Playerspawns[i]);
        }
        Enemey_Manager.Instance.start = true;
    }

    [Server] //only executed in server  since game manager is run on server
    public void StartLobby() //tell players game has started and spawn pawn // IS RUN BY THE SERVER
    {
        
        for (int i = 0; i < players.Count; i++ )
        {
            if (players[i].ControlledPawn == false)
                players[i].StartLobby(LobbySpawns[i]);
        }
    }


    [Server] //only executed in server
    public void StopGame() //destroy pawns game is done
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].StopGame();
        }

    }
}
