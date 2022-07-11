using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;
public sealed class PawnMovement : NetworkBehaviour
{
    private PawnInput _input;

    private Rigidbody2D _RigidBody2d;

    private Animator _animtor;

    private bool jumping;

   // [SyncVar(OnChange = nameof(OnJump))]
    public Vector2 Velocity;

    [SerializeField]
    private float Speed;

    [SerializeField]
    private float JumpSpeed;




    [SerializeField]
    private float GravityScale;

    //  private Rigidbody2D _RigidBody;

    private Vector2 Velocity_;



    public override void OnStartNetwork() // on spawn
    {
        base.OnStartNetwork();
        _input = GetComponent<PawnInput>();
        _RigidBody2d = GetComponent<Rigidbody2D>();
        _RigidBody2d.freezeRotation = true;
        _animtor = GetComponentInChildren<Animator>();
    }


    private void Update() //move on client side
    {



        if (!IsOwner) return;

        transform.position += new Vector3(_input.horizontal, 0, 0) * Time.deltaTime * Speed;



        if (_input.jump == true && Mathf.Abs(_RigidBody2d.velocity.y) < 0.001f && jumping == false)
        {
            _RigidBody2d.AddForce(new Vector2(0, JumpSpeed), ForceMode2D.Impulse);
            Debug.Log("loop");
            jumping = true;
        }

        if (Mathf.Abs(_RigidBody2d.velocity.y) < 0.001f)
            jumping = false;

        if (jumping == true)
        {
            var Vel = _RigidBody2d.velocity;
            Jump(Vel);
        }

        

        if (_input.mouseX > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else
            transform.localScale = new Vector3(-1f, 1f, 1f);



        if (_input.horizontal != 0)
            _animtor.SetFloat("speed", 1);
        else
            _animtor.SetFloat("speed", 0);



    }

    [ServerRpc]
    private void Jump(Vector2 Vel)
    {
        OnJump(Vel);
        //Velocity = Vel;
        //Debug.Log(Velocity);
    }


    //cserver to everything else
    [ObserversRpc]
    private void OnJump(Vector2 Vel)
    {

        if ( IsOwner == false)
        {
            _RigidBody2d.velocity = Vel;
        }
    }



}
