using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class ItemScript : ScriptableObject
{


    [Header("gameplay")]
    public itemtype type;
    public GunData gundata;

    [Header("UI")]
    public bool stackable = true;

    public Sprite icon;
    public Gun_Data_ gun;

    
  
}
   public enum itemtype
    {
    Weapon,
    ammo,
    block,
    material
    }

