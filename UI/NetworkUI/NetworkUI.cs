using Godot;

[Icon("uid://c87tr5a13dcah")]

public partial class NetworkUI : Control
{
    Button ENetHostButton, ENetJoinButton, ENetSwitchButton, SteamHostButton, SteamSwitchButton;
    
    PanelContainer ENetMenu, SteamMenu;

    bool showSteam = false;

    public override void _Ready()
    {

        ENetMenu = GetNode<PanelContainer>("%ENetMenu");
        SteamMenu = GetNode<PanelContainer>("%SteamMenu");

        ENetHostButton = GetNode<Button>("%ENetHostButton");
        ENetJoinButton = GetNode<Button>("%ENetJoinButton");
        ENetSwitchButton = GetNode<Button>("%ENetSwitchButton");
        SteamHostButton = GetNode<Button>("%SteamHostButton");
        SteamSwitchButton = GetNode<Button>("%SteamSwitchButton");

        ENetHostButton.Pressed += eNetHostButtonPressed;
        ENetJoinButton.Pressed += eNetJoinButtonPressed;
        ENetSwitchButton.Pressed += eNetSwitchButtonPressed;
        SteamHostButton.Pressed += steamHostButtonPressed;
        SteamSwitchButton.Pressed += steamSwitchButtonPressed;
    }

    void eNetHostButtonPressed()
    {
        if (ENetworkManager.Instance.ENetCreateServer()) Hide();
    }

    void eNetJoinButtonPressed()
    {
        if (ENetworkManager.Instance.ENetJoinServer()) Hide();
    }

    void eNetSwitchButtonPressed()
    {
        ENetMenu.Hide();
        SteamMenu.Show();
    }

    void steamHostButtonPressed()
    {
        //SteamNew.Call("host_lobby");
        Hide();
    }

    void steamSwitchButtonPressed()
    {
        SteamMenu.Hide();
        ENetMenu.Show();
    }
}
