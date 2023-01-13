using UnityEngine;
using FishNet.Object;
using UnityEngine.AddressableAssets;
public sealed class PawnWeapon : NetworkBehaviour
{
    private Pawn _Pawn;

    private PawnInput _Input;

    private Camera _Camera;

    private Quaternion Rotation;


    private GameObject Projectile;


    [SerializeField]
    private float damage;

    [SerializeField]
    private float RateOfFire;

    private float _TimeUntilNextShot;

    [SerializeField]
    private GameObject test;

    [SerializeField]
    private Transform FirePoint;

    [SerializeField]
    private Transform Gun;

    Quaternion CamRotation;

    
    private GameObject enemi;

    public AudioClip fire;

    
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _Pawn = GetComponent<Pawn>();

        _Input = GetComponent<PawnInput>();

        _Camera = GetComponent<PawnCamera>().myCamera.GetComponent<Camera>();
        CamRotation = _Camera.transform.rotation;

        Projectile = Addressables.LoadAssetAsync<GameObject>("Projectile").WaitForCompletion();
        

    }



    private void Update()
    {

        if (!IsOwner) return;

        UpdateRotation();

        if (_TimeUntilNextShot <= 0.0f)
        {
            if (_Input.fire)
            {
                //ServerFire(FirePoint.position, FirePoint.right);
                //request server RPC func so that non server clients request the server to run this code
                ServerFire();
                //ServerSpawnEnemy();
                _TimeUntilNextShot = RateOfFire;
            }
        }
        else
            _TimeUntilNextShot -= Time.deltaTime;

        //if (_Input.fire2)
            

    }




    //Keep the cameras rotation as constant
    void UpdateCameraRotation()
    {

        _Camera.transform.rotation = CamRotation;

    }

    private void UpdateRotation()
    {

        Vector2 dir = _Input.MousePos - _Camera.WorldToScreenPoint(Gun.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Gun.transform.rotation = Rotation;
    }



    [ObserversRpc]
    private void ShootProjectile()
    {
        GameObject ProjectileInstance = Instantiate(Projectile, FirePoint.position, Gun.rotation);
        Spawn(ProjectileInstance);
        //ProjectileInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Rotation.x * 0.01f, Rotation.y * 0.01f));
        AudioSource.PlayClipAtPoint(fire, FirePoint.position);
    }

    
    [ServerRpc]
    private void ServerFire() //fine to use server transform but better to use parameter to make sure
    {

        ShootProjectile(); //this is run on server and then all slients becasue overserverRPC


       /*
       old
        RaycastHit2D hit = Physics2D.Raycast(FirPointPos, firepointdirection);
        if (hit.collider != null && hit.transform.TryGetComponent(out Pawn pawn))  //check if raycast hit anything
        {
            pawn.RecieveDamage(damage);
        }


        Debug.Log("line");
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(10,0,0), Color.green, 10f);
       */
    }


    [ServerRpc]
    private void ServerSpawnEnemy()
    {
        SpawnEnemey();

    }
    [ObserversRpc]
    private void SpawnEnemey()
    {
        GameObject Enemey = Instantiate(enemi, new Vector3(-3f, -2f, 0f), Gun.rotation);
        Spawn(Enemey);
    }
    
}

