using UnityEngine;
using FishNet.Object;
using UnityEngine.AddressableAssets;
public sealed class PawnWeapon : NetworkBehaviour
{
    [SerializeField]
    public Gun_Data_ CurrentGun;
    

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

    private Inventory_Manager _Inventory;


    
    
    //initilize component
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _Pawn = GetComponent<Pawn>();

        _Input = GetComponent<PawnInput>();

        _Camera = GetComponent<PawnCamera>().myCamera.GetComponent<Camera>();


        _Inventory = Inventory_Manager.instance;
        Projectile = Addressables.LoadAssetAsync<GameObject>("Projectile").WaitForCompletion();

    
        

    }



    private void Update()
    {

        if (!IsOwner) return;

        if (_Inventory.GetselectedItem(false) != null && _Inventory.GetselectedItem(false).type != itemtype.Weapon)
        {
            Gun.SetActive(false);
            return;
        }
        else
            Gun.SetActive(true);

        CurrentGun = _Inventory.GetweaponData();
        

        UpdateRotation();


      

        
        if (_TimeUntilNextShot <= 0.0f)
        {
            if (_Input.fire && CurrentGun.ammo > 0)
            {
                ServerFire();
                CurrentGun.ammo--;
                _TimeUntilNextShot = CurrentGun.firerate;

                if (CurrentGun.ammo <= 0 )
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

        CurrentGun.updateAmmo();
        CurrentGun.reload();
        CurrentGun.updateAmmo();

    }


    public int updateammo()
    {

        return 1;
        
    }



    private void UpdateRotation()
    {
        //keep gun rotation looking at mouse
        Vector2 dir = _Input.MousePos - _Camera.WorldToScreenPoint(Gun.transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Gun.transform.rotation = Rotation;



        if (_Input.mouseX > 1000)
            Gun.transform.localScale = new Vector3(1.6f, 2.17248f, 1f);
        else if (_Input.mouseX < 1000)
            Gun.transform.localScale = new Vector3(1.6f, -2.17248f, 1f);
    }



    [ObserversRpc]
    private void ShootProjectile()
    {
        GameObject ProjectileInstance = Instantiate(Projectile, FirePoint.position, Gun.transform.rotation);
        ProjectileInstance.GetComponent<Projectile>().damage = CurrentGun.damage;
        ProjectileInstance.GetComponent<Projectile>().speed = CurrentGun.speed;
        Spawn(ProjectileInstance);
       // AudioSource.PlayClipAtPoint(CurrentGun.fire, FirePoint.position);
    }


    [ServerRpc]
    private void ServerFire() //fine to use server transform but better to use parameter to make sure
    {

        ShootProjectile(); //this is run on server and then all slients becasue overserverRPC

    }

}

