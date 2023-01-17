
using UnityEngine;
using FishNet.Object;
public class MeleeWeapon : NetworkBehaviour
{

    public Gun_Data_ Weapondata;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOwner)
            return;

        
        if (collision.gameObject.CompareTag("Enemy"))
               
        {
            Debug.Log(Weapondata.damage);
            collision.GetComponent<Enemy>().RecieveDamage(Weapondata.damage);
            
        }
    }
}
