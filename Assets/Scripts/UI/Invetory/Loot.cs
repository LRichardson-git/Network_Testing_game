using FishNet.Object;
using UnityEngine;

public class Loot : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private SpriteRenderer iconRenderer;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float speed;

    [SerializeField] public ItemScript _item;

    public int count = 1;
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


    [ObserversRpc]
    public void DespawnSElf()
    {
        Despawn();
    }



}
