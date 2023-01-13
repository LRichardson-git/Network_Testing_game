
using FishNet.Object;
using FishNet.Object.Synchronizing;


public sealed  class Pawn : NetworkBehaviour
{
    [SyncVar] 
    public Player_ ControllingPlayer; //reference to the player conntrolling this object

    [SyncVar]
    public float health;




    internal void RecieveDamage(float damage) //called in a server RPC in weapon
    {
        if (!IsSpawned) return;

        //person controlling pawn has died, tell the controlling player he has died
        if ((health -= damage) <= 0.0f)
        {
            ControllingPlayer.TargetPawnKilled(Owner);

            Despawn(); //Pawn is deleted from the Server(therefore is gone from all clients)

        }
    }
}
