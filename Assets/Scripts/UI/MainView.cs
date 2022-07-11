
using UnityEngine;
using TMPro;
public sealed class MainView : View
{
    [SerializeField]
    private TextMeshProUGUI HealthText;

    private void Update()
    {
        if (!initialized) return;

        //safety stuff
        Player_ player = Player_.LocalInstance;
        if (player == null || player.ControlledPawn == null) return;


        //display current pawns health
        HealthText.text = $"Health: {Player_.LocalInstance.ControlledPawn.health}";

    }



}
