using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.AddressableAssets;
using FishNet.Connection;
//Identifier for player object
public class Player_ : NetworkBehaviour // monobehabour that is networked
{

    public static Player_ LocalInstance { get; private set; } //it is a local instance

    [SyncVar] //when value is change it is automatically synco to all clients, NEEDS TO BE CHANGED ON THE SERVER
    public string UserName;

    [SyncVar]//sync var only update by self if the client is also the host
    public bool isReady;

    [SyncVar]
    public Pawn ControlledPawn;

    [SyncVar]
    public GameObject controlledObject;

    //dont need to be sync vars atm maybe in the future, since only local player needs to know
    Color MatColorP;
    Color GunColorP;


    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        LocalInstance = this; //set local instance to the player_ you are controlling

        Ui_Manager.Instance.init(); //Ui_Manger is initialized depending on who owns the player object

        

        
    }

    public override void OnStartServer()
    {
        base.OnStartServer(); //when override network call backs make sure to allways call base method first

        GameManager.Instance.players.Add(this);

        GameManager.Instance.StartLobby(); //need to do in this order because the object was spawned after we went to lobby view, meaing that the wasnt anything for buttons to ref

        Ui_Manager.Instance.show<LobbyView_>(); //set to lobby view on spawn
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        GameManager.Instance.players.Remove(this);
    }

    private void Update()
    {
        if (!IsOwner) return; //is true if this object is owned by the connection, if try to execture without check will execture on client and then server which wont work

        if (Input.GetKeyDown(KeyCode.R)){

            ServerSetIsReady(!isReady); //switch Isready when Press R
        }


    }

    //dont know if serverrpc
    public void StartGame(Vector3 spawn) //only called by gamemanger(server)
    {

        

        GameObject PawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion(); //makes everyone have it synchonisly

        GameObject PawnInstance = Instantiate(PawnPrefab, spawn, Quaternion.identity);

        //setup setttings here


        if (ControlledPawn != null && ControlledPawn.IsSpawned)
        {
            MatColorP = ControlledPawn.GetComponent<Lobby_Pawn>().MatColor;
            GunColorP = ControlledPawn.GetComponent<Lobby_Pawn>().GunColor;
            ControlledPawn.Despawn();
        }
      
        

        Spawn(PawnInstance, Owner);

        controlledObject = PawnInstance;

        ControlledPawn = PawnInstance.GetComponent<Pawn>(); //can get pawn instance anytime

        ControlledPawn.ControllingPlayer = this; //set refence

        ControlledPawn.GetComponent<PawnColor>().SetColors(MatColorP, GunColorP);

        TargetPawnSpawn(Owner); //server tells player to switch to main view
    }


    public void StartLobby(Vector3 spawn)
    {
        GameObject LobbyPawnPrefab = Addressables.LoadAssetAsync<GameObject>("LobbyPawn").WaitForCompletion(); //makes everyone have it synchonisly

        
        GameObject PawnInstance = Instantiate(LobbyPawnPrefab,spawn, Quaternion.identity) ;

        //setup setttings here


        Spawn(PawnInstance, Owner);

        ControlledPawn = PawnInstance.GetComponent<Pawn>(); //can get pawn instance anytime

        ControlledPawn.ControllingPlayer = this; //set refence

         
    }




    [ServerRpc(RequireOwnership = false)] //run on server, which means only this client will respawn and not every client
    public void ServerSpawnPawn() //ownership set to false since UI does not own the object and this is where it is called from
    {
        StartGame(new Vector3(0,0,0)); //this is for respawn func, probably need to change so people dont spawn in each other
    }

    public void StopGame()
    {
        //isspawned check makes sure that network object is spawned on server and is ready to recieve RPC(safety check is important and useful)
        if (ControlledPawn != null && ControlledPawn.IsSpawned) ControlledPawn.Despawn();
        
    }


    //by default requireonwership = true and is usally good to keep on
    //you should never network you UI unless absoultly neccasry
    //since you don't own the UI make it requireownership = false
    [ServerRpc(RequireOwnership = false)] //RPC client call code on the server 
    public void ServerSetIsReady(bool value)  //Call server to set syncvar isready to the bool
    {
        isReady = value;
    }


    //Fishnet has nice feature where if network connection is last paramater it will automatically set it to the owner or the object that sent the RPC
    [TargetRpc] //like a normal Rpc but only sends for a specific connection
    private void TargetPawnSpawn(NetworkConnection networkConnection)//connection we would like to send to
    {
        Ui_Manager.Instance.show<MainView>();

    }
    //server called Receive damage on pawn, pawn is dead and now the server needs to tell the player that is has died
    [TargetRpc] //it just seems confusing because the code for this is in player itself but this code is called by the server in the player script
    public void TargetPawnKilled(NetworkConnection networkConnection)
    {
        Ui_Manager.Instance.show<Respawn_View>(); //Tells its instance of UI manager to go to dead screen
    }//has to be targetRPC because THE SERVER SERVER IS SENDING THE THE MESSAGE TO RUN THIS CODE TO THE TARGETED PLAYER
}
