using UnityEngine;
using FishNet.Object;
using UnityEngine.AddressableAssets;
public sealed class PawnWeapon : NetworkBehaviour
{
    [SerializeField]
    public GunData CurrentGun;
    

    //data
    private Pawn _Pawn;
    private PawnInput _Input;
    private Camera _Camera;
    private Quaternion Rotation;
    private GameObject Projectile;

    private float _TimeUntilNextShot;

    [SerializeField]
    private Transform FirePoint;

    [SerializeField]
    private GameObject Gun;

    private PawnInventory _Inventory;


    
    
    //initilize component
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _Pawn = GetComponent<Pawn>();

        _Input = GetComponent<PawnInput>();

        _Camera = GetComponent<PawnCamera>().myCamera.GetComponent<Camera>();

        _Inventory = GetComponent<PawnInventory>();
    
        Projectile = Addressables.LoadAssetAsync<GameObject>("Projectile").WaitForCompletion();
        

    }



    private void Update()
    {

        if (!IsOwner) return;

        UpdateRotation();

        if (_TimeUntilNextShot <= 0.0f)
        {
            if (_Input.fire && CurrentGun.currentammo > 0)
            {
                ServerFire();
                CurrentGun.currentammo--;
                _TimeUntilNextShot = CurrentGun.firerate;

                if (CurrentGun.currentammo <= 0 )
                {
                    //start animation
                    Invoke("reload", CurrentGun.reloadTime);

                }
                
            }
        }
        else
            _TimeUntilNextShot -= Time.deltaTime; 

    }



    private void reload()
    {
        


    }





    private void UpdateRotation()
    {
        //keep gun rotation looking at mouse
        Vector2 dir = _Input.MousePos - _Camera.WorldToScreenPoint(Gun.transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Gun.transform.rotation = Rotation;
    }



    [ObserversRpc]
    private void ShootProjectile()
    {
        GameObject ProjectileInstance = Instantiate(Projectile, FirePoint.position, Gun.transform.rotation);
        ProjectileInstance.GetComponent<Projectile>().damage = CurrentGun.damage;
        ProjectileInstance.GetComponent<Projectile>().speed = CurrentGun.speed;
        Spawn(ProjectileInstance);
        AudioSource.PlayClipAtPoint(CurrentGun.fire, FirePoint.position);
    }


    [ServerRpc]
    private void ServerFire() //fine to use server transform but better to use parameter to make sure
    {

        ShootProjectile(); //this is run on server and then all slients becasue overserverRPC

    }

}

