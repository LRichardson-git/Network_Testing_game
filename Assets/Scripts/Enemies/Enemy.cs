
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
public class Enemy : NetworkBehaviour
{
    [SyncVar]
    public float health;

    public Enemy_Input _input;

    internal void RecieveDamage(float damage) //called in a server RPC in weapon
    {
        if (!IsSpawned) return;

        health -= damage;

        //person controlling pawn has died, tell the controlling player he has died
        if ((health) <= 0.0f)
        {
            Enemey_Manager.Instance.desself(gameObject);

            Despawn(); //Pawn is deleted from the Server(therefore is gone from all clients)

        }
    }
}
