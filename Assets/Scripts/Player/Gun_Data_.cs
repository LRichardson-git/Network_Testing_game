using UnityEngine;

public class Gun_Data_ :MonoBehaviour
{
    public float firerate = 0.1f;
    public float reloadTime = 1;
    public int ammo = 10;
    public int totalammo = 10;
    public float speed = 100;
    public int damage = 50;
    public int magsize = 8;


    public void Start()
    {
        updateAmmo();
    }


    public void updateAmmo()
    {
        totalammo = Inventory_Manager.instance.UpdateAmmo();
    }

    public void reload()
    {
        if (totalammo > 0)
        {

            for (int i = 0; i < totalammo; i++)
            {
                //Inventory_Manager.instance.GetselectedItem(false);
            }

           
            Debug.Log("testdd");
            totalammo = totalammo - magsize;
            ammo = magsize;

            if (totalammo < 0)
            {
                Debug.Log("testdfcsdf");
                ammo = ammo + totalammo;
                totalammo = 0;
            }

            Inventory_Manager.instance.DeleteItems(ammo);

            Debug.Log(totalammo);



        }
     
        

    }


}
