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



    
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _Pawn = GetComponent<Pawn>();

        _Input = GetComponent<PawnInput>();

        _Camera = GetComponent<PawnCamera>().myCamera.GetComponent<Camera>();

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
                ServerFire(FirePoint.position, FirePoint.right);
                
                _TimeUntilNextShot = RateOfFire;
            }
        }
        else
            _TimeUntilNextShot -= Time.deltaTime;
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
    }


    [ServerRpc]
    private void ServerFire(Vector2 FirPointPos, Vector2 firepointdirection) //fine to use server transform but better to use parameter to make sure
    {

        ShootProjectile();
       

        RaycastHit2D hit = Physics2D.Raycast(FirPointPos, firepointdirection);
        if (hit.collider != null && hit.transform.TryGetComponent(out Pawn pawn))  //check if raycast hit anything
        {
            pawn.RecieveDamage(damage);
        }


        Debug.Log("line");
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(10,0,0), Color.green, 10f);

    }
}
