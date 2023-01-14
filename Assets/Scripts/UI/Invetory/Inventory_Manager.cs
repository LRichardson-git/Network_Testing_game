using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{

    public InventorySlot[] inventorySlots;
    public GameObject InvItemPrefab;

    public int stacklimit = 8;

    int SelectedSlot = -1; //nothing slected as default
    public bool addItem(ItemScript item)
    {

        //check if slot has same item that is stackable
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>(); //check to see if child has item
            //no item in slot
            if (iteminslot != null && iteminslot == item && iteminslot.count < stacklimit &&iteminslot.item.stackable == true)
            {
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
    }

    void SpawnNewItem(ItemScript item, InventorySlot slot)
    {
        GameObject newitemGo = Instantiate(InvItemPrefab, slot.transform);
        Inventory_Item invenItem = newitemGo.GetComponent<Inventory_Item>();
        invenItem.initialiseItem(item); //create in inventory based on itemscript data
    }


    public ItemScript GetselectedItem()
    {
        InventorySlot slot = inventorySlots[SelectedSlot];
        Inventory_Item iteminslot = slot.GetComponentInChildren<Inventory_Item>();
        if (iteminslot != null)
        {
            return iteminslot.item;
        }
        else
            return null;
    }

}
