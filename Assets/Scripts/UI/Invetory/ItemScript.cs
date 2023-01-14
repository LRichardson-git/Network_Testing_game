using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class ItemScript : ScriptableObject
{


    [Header("gameplay")]
    public GameObject Item;
    public itemtype type;

    [Header("UI")]
    public bool stackable = true;

    public Sprite icon;

}
   public enum itemtype
    {
    Weapon,
    ammo,
    block,
    material
    }

