using Godot;

[Icon("uid://c87tr5a13dcah")]

public partial class NetworkUI : Control
{
    Button ENetHostButton, ENetJoinButton;

    public override void _Ready()
    {
        ENetHostButton = GetNode<Button>("%ENetHostButton");
        ENetJoinButton = GetNode<Button>("%ENetJoinButton");

        ENetHostButton.Pressed += eNetHostButtonPressed;
        ENetJoinButton.Pressed += eNetJoinButtonPressed;
    }

    void eNetHostButtonPressed()
    {
        if (NetworkHandler.Instance.ENetCreateServer()) Hide();
    }

    void eNetJoinButtonPressed()
    {
        if (NetworkHandler.Instance.ENetJoinServer()) Hide();
    }
}
