
using UnityEngine;
using UnityEngine.UI;
public sealed class Respawn_View : View
{
    [SerializeField]
    private Button RespawnButton;

    public override void init()
    {
        RespawnButton.onClick.AddListener(() => Player_.LocalInstance.ServerSpawnPawn());



        base.init();
    }
}
