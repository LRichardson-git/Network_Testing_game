using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName ="Weapon")]
public class GunData : ScriptableObject {

    [SerializeField]
    public string Name;


    [Header("shooting")]
    public int damage;
    public float maxDistance;
    public float speed;

    [Header("Reload")]
    public int currentammo;
    public int magsize;
    public int magtype;
    public float firerate;
    public float timetoreload;


    [HideInInspector]
    public int reloadTime;


    [Header("sounds")]
    public AudioClip fire;


    [SerializeField]
    public Gun_Data_ track;

    private void Awake()
    {
       // track.totalammo = currentammo;
       // track.ammo = currentammo;
    }


}
