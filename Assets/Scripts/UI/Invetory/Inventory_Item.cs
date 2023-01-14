
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Inventory_Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public ItemScript item;



    //drag and drop
    [Header("ui")]
    public Image icon;
    public TextMeshProUGUI CountText;

    [HideInInspector]
    public int count = 1;
    [HideInInspector]
    public Transform parentAfterDrag;

    
    private void Start()
    {
        initialiseItem(item);
    }

    

    public void refreshCount()
    {
        CountText.text = count.ToString();
        bool textActive = count > 1;
        CountText.gameObject.SetActive(textActive);
    }

    public void initialiseItem(ItemScript NewItem)     
    {
        item = NewItem;
        icon.sprite = NewItem.icon;
        refreshCount();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        icon.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag( PointerEventData eventData)
    {
        icon.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        throw new System.NotImplementedException();
    }

}
