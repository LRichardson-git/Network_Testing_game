using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public sealed class Enemy_Movement : NetworkBehaviour
{
    [SerializeField]
    Enemy_Input _input;

    Rigidbody2D _rb;
    [SerializeField]
    float speed = 5f;
    // Start is called before the first frame update
    Vector3 dir;
    float angle;

    //temp audio
    //public AudioSource Audio;
    

    public override void OnStartNetwork() // on spawn
    {
        base.OnStartNetwork();
        _input.GetComponent<Enemy_Input>();
        _rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        //go towards the player

        if (_input.ClosestPlayer != null)
        {
            dir = _input.ClosestPlayer.transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
            dir.Normalize();
            _rb.MovePosition((Vector2)transform.position + ((Vector2)dir * speed * Time.deltaTime));
        }
    }


    

}





