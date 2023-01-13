using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Enemy_Input : NetworkBehaviour
{
    public GameObject ClosestPlayer;
    Enemy self;
    
    
    public override void OnStartNetwork() //set pawn as reference when spawned
    {
        base.OnStartNetwork();

        self = GetComponent<Enemy>();

        FindClosestPlayer();
    }

    public void FindClosestPlayer()
    {

      


        //calulate distant between players
        float dist = 1f;
        ClosestPlayer = GameManager.Instance.players[0].controlledObject;
        if (GameManager.Instance.players.Count > 0)
        {
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                if (GameManager.Instance.players[i].controlledObject != null && Vector2.Distance( GameManager.Instance.players[i].controlledObject.transform.position, transform.position) < dist )
                {
                    //choose the closest player character 
                    dist = Vector2.Distance(GameManager.Instance.players[i].controlledObject.transform.position, transform.position);
                    ClosestPlayer = GameManager.Instance.players[i].controlledObject;
                }
            }
        }


    }

}


        
        
        

    