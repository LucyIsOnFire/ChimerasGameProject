using Godot;
using System;

[GlobalClass, Icon("uid://c87tr5a13dcah")]

public partial class NetworkUI : CanvasLayer
{
    private Button hostButton, joinButton, quitButton;

    public override void _Ready()
    {
        hostButton = GetNode<Button>("%HostButton");
        joinButton = GetNode<Button>("%JoinButton");
        quitButton = GetNode<Button>("%QuitButton");

        hostButton.Pressed += hostButtonPressed;
        joinButton.Pressed += joinButtonPressed;
        quitButton.Pressed += quitButtonPressed;
    }

    private void hostButtonPressed()
    {
        Networking.Instance.CreateServer();
        Hide();
    }

    private void joinButtonPressed()
    {
        Networking.Instance.JoinServer();
        Hide();
    }

    private void quitButtonPressed()
    {
        GetTree().Quit();
    }
}
