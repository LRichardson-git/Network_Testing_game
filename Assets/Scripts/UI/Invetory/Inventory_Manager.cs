
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{

    public static Inventory_Manager instance;
    
    public ItemScript[] startItems;




    public InventorySlot[] inventorySlots;
    public GameObject InvItemPrefab;

    public int stacklimit = 8;

    int SelectedSlot = -1; //nothing slected as default

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        changeselectedslot(0);
        foreach (var item in startItems)
        {
            addItem(item);
        }
    }

    public bool addItem(ItemScript item)
    {

        //check if slot has same item that is stackable
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>(); //check to see if child has item
            //no item in slot
            if (iteminslot != null && iteminslot.item == item && iteminslot.count < stacklimit && iteminslot.item.stackable == true)
            {
                Debug.Log("test");
                iteminslot.count++;
                iteminslot.refreshCount();
                return true;
            }
        }






        //find empty inv slot
            for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>(); //check to see if child has item
            //no item in slot
            if(iteminslot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }

     
        }
        return false; //if no inventory slots
    }

    private void Update()
    {
        if (Input.inputString != null){
            bool isnumber = int.TryParse(Input.inputString, out int number);
            if (isnumber && number > 0 && number < 8)
            {
                changeselectedslot(number - 1);
            }
        }
    }

    void changeselectedslot(int newValue)
    {
        if (SelectedSlot >=0)
            inventorySlots[SelectedSlot].Deselect();

        inventorySlots[newValue].Select();
        SelectedSlot = newValue;

       
        
           // inventorySlots[SelectedSlot].GetComponent<Gun_Data_>().updateAmmo();
            
        
    }

    void SpawnNewItem(ItemScript item, InventorySlot slot)
    {
        GameObject newitemGo = Instantiate(InvItemPrefab, slot.transform);
        Inventory_Item invenItem = newitemGo.GetComponent<Inventory_Item>();
        invenItem.initialiseItem(item); //create in inventory based on itemscript data
    }


    public ItemScript GetselectedItem(bool use)
    {
        Debug.Log("valled"+ use);
        InventorySlot slot = inventorySlots[SelectedSlot];
        Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>();
        

        if (iteminslot != null) {
            ItemScript item = iteminslot.item;
            if (use == true) {
                iteminslot.count--;
                if (iteminslot.count <= 0) {
                    Destroy(iteminslot.gameObject);
                } 
                else {
                    iteminslot.refreshCount();
                }

            }
            return item;
        }

        return null;
        
            
    }

    public void DeleteItems(int amount)
    {


        int total = amount + 1;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>(); //check to see if child has item
            //no item in slot
            if (iteminslot != null && iteminslot.item.type == itemtype.ammo)
            {
                int counter = iteminslot.count;
                for (int j = 0; j < counter; j++)
                {
                    total--;
                    iteminslot.count--;
                    if (iteminslot.count <= 0)
                    {

                        Destroy(iteminslot.gameObject);
                    }

                }
                iteminslot.refreshCount();

                if (total <= 0)
                    return;
            }
        }

    }


    public Gun_Data_ GetweaponData()
    {
        InventorySlot slot = inventorySlots[SelectedSlot];
        Gun_Data_ iteminslot = slot.GetComponentInChildren<Gun_Data_>();

        if (iteminslot != null)
        {
            return iteminslot;
        }
        else
            return null;
    }

    public int UpdateAmmo()
    {
        int totalammo =0 ;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>(); //check to see if child has item
            //no item in slot
            if (iteminslot != null && iteminslot.item.type == itemtype.ammo)
            {
                totalammo += iteminslot.count;
                
            }
        }
        return totalammo;

    }
}


