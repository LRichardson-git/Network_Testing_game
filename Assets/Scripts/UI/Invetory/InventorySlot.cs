using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour, IDropHandler
{

    public Image image; //background to show selected image
    public Color selectedColour, notSelectedColour;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColour; 
    }

    public void Deselect()
    {
        image.color=notSelectedColour;
    }

    public void OnDrop(PointerEventData eventData)
       
    {
        if(transform.childCount == 0)
        {
            Inventory_Item inventory_Item = eventData.pointerDrag.GetComponent<Inventory_Item>();
            inventory_Item.parentAfterDrag = transform;
        }
    }
}
