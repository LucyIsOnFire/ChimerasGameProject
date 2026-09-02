using Godot;
using System;

public partial class Networking : Node
{
    const string IP_ADDRESS = "localhost";
    const int PORT = 9999;

    public static Networking Instance { get; private set; }

    ENetMultiplayerPeer peer;

    public override void _Ready()
    {
        Instance = this;
    }

    public void CreateServer()
    {
        peer = new();
        peer.CreateServer(PORT);
        Multiplayer.MultiplayerPeer = peer;
    }

    public void JoinServer()
    {
        peer = new();
        peer.CreateClient(IP_ADDRESS, PORT);
        Multiplayer.MultiplayerPeer = peer;
    }

    public void LeaveServer()
    {
        peer.Close();
        peer = null;
        GetTree().ReloadCurrentScene();
    }
}
