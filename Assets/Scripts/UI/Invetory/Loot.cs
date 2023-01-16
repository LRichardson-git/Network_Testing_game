using FishNet.Object;
using UnityEngine;

public class Loot : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private SpriteRenderer iconRenderer;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float speed;

    [SerializeField] private ItemScript _item;
    public override void OnStartNetwork() //set pawn as reference when spawned
    {
        base.OnStartNetwork();

        _collider = GetComponent<BoxCollider2D>();
        iconRenderer = GetComponent<SpriteRenderer>();
        //for debugin
        init(_item);
    }

    public void init(ItemScript item)

    {
        this._item = item;
        iconRenderer.sprite = item.icon;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory_Manager.instance.addItem(_item);
            //add a move thing here maybe
            Despawn();
        }
    }



}
