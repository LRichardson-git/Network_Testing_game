using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet;
public sealed class LobbyView_ : View
{
    [SerializeField]
    private Button toggleReadyButton;

    [SerializeField]
    private TextMeshProUGUI ToggleReadyButtonText;

    [SerializeField]
    private Button ToggleBodyColor;

    [SerializeField]
    private Button ToggleGunColor;

    [SerializeField]
    private Button StartGameButton;


    public override void init() //override Init from view which we inherit from
    {

        

        
            StartGameButton.onClick.AddListener(() => GameManager.Instance.StartGame()); //no check since button is disabled for not hosts

            StartGameButton.gameObject.SetActive(true);

        
      


        

        toggleReadyButton.onClick.AddListener(() => Player_.LocalInstance.ServerSetIsReady(!Player_.LocalInstance.isReady));
        ToggleBodyColor.onClick.AddListener(() => Player_.LocalInstance.ControlledPawn.GetComponent<Lobby_Pawn>().RandomBodyColor());
        ToggleGunColor.onClick.AddListener(() => Player_.LocalInstance.ControlledPawn.GetComponent<Lobby_Pawn>().RandomGunColor());



        base.init(); //will call Init from View which is class we inherit from
    }

    private void Update()
    {
       if (!initialized) return;



        //if player ready make green else red
        ToggleReadyButtonText.color = Player_.LocalInstance.isReady ? Color.green : Color.red;

        //disableds interaction with button unless can start
        StartGameButton.interactable = GameManager.Instance.canstart;
    }

}
