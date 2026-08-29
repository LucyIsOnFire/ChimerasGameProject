using Godot;
using NetworkMultiplayer;

public partial class NetworkUI : CanvasLayer
{
    private Button clientButton, serverButton;

    public override void _Ready()
    {
        clientButton = GetNode<Button>("%ClientButton");
        serverButton = GetNode<Button>("%ServerButton");

        clientButton.Pressed += onClientPressed;
        serverButton.Pressed += onServerPressed;
    }

    void onServerPressed()
    {
        NetworkHandler.Instance.StartServer();
        Hide();
    }

    void onClientPressed()
    {
        NetworkHandler.Instance.StartClient();
        Hide();
    }
}
