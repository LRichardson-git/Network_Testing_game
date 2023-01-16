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

    public AudioSource audio;

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

        transform.position += new Vector3(_input.horizontal, _input.vertical, 0) * Time.deltaTime * Speed;


        if (_input.horizontal != 0 || _input.vertical != 0)
            audio.enabled = true;
        else
            audio.enabled = false;

        
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
