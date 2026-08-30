using Godot;
using System;

[GlobalClass, Icon("uid://c87tr5a13dcah")]

public partial class NetworkUI : CanvasLayer
{
    private Button joinButton, quitButton;


    public override void _Ready()
    {
        if (OS.HasFeature("server"))
        {
            Hide();
            return;
        }

        joinButton = GetNode<Button>("%JoinButton");
        quitButton = GetNode<Button>("%QuitButton");

        joinButton.Pressed += joinButtonPressed;
        quitButton.Pressed += quitButtonPressed;
    }

    private void joinButtonPressed()
    {
        NetworkHandler.Instance.JoinServer();
        Hide();
    }

    private void quitButtonPressed()
    {
        GD.Print("Bye!");
        GetTree().Quit();
    }
}
