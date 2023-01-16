
using UnityEngine;
using TMPro;
public sealed class MainView : View
{
    [SerializeField]
    private TextMeshProUGUI HealthText;

    [SerializeField]
    private TextMeshProUGUI AmmoText;

    private void Update()
    {
        if (!initialized) return;

        //safety stuff
        Player_ player = Player_.LocalInstance;
        if (player == null || player.ControlledPawn == null) return;


        //display current pawns health
        HealthText.text = $"Health: {Player_.LocalInstance.ControlledPawn.health}";

        if (Player_.LocalInstance.ControlledPawn.GetComponent<PawnWeapon>().CurrentGun != null)
        {

            AmmoText.text = $"Ammo: {Player_.LocalInstance.ControlledPawn.GetComponent<PawnWeapon>().CurrentGun.ammo} ::: {Player_.LocalInstance.ControlledPawn.GetComponent<PawnWeapon>().CurrentGun.totalammo}  ";

        }

    }



}
