using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
public sealed class PawnInput : NetworkBehaviour //sake of simplicity
{
    private Pawn pawn_;

    public float horizontal;
    public float vertical;

    public float mouseX;
    public float mouseY;

    public Vector3 MousePos;

    public bool jump;
    public bool fire;
    public bool fire2;


    // do not need to synco input (obviously) so no sync vars
    public override void OnStartNetwork() //set pawn as reference when spawned
    {
        base.OnStartNetwork();

        pawn_ = GetComponent<Pawn>();
    }


    //get inputs
    private void Update()
    {
        if (!IsOwner) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        
        mouseY = Input.GetAxis("Mouse Y");

        jump = Input.GetButton("Jump");

        fire = Input.GetButton("Fire1");
        fire2 = Input.GetButton("Fire2");

        MousePos = Input.mousePosition;

        mouseX = Camera.main.ScreenToWorldPoint(MousePos).x;
    }

}
