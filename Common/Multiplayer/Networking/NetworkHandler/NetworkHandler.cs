using Godot;

public partial class NetworkHandler : Node
{
    static public NetworkHandler Instance;

    const string IP_ADDRESS = "127.0.0.1";
    const int PORT = 9999;

    public override void _Ready()
    {
        Instance= this;
        Multiplayer.ServerDisconnected += ServerClosed;
    }

    public bool ENetCreateServer()
    {
        ENetMultiplayerPeer peer = new();
        Error error = peer.CreateServer(PORT);

        if (error != Error.Ok)
        {
            GD.Print($"Cannot host: {error}");
            return false;
        }

        Multiplayer.MultiplayerPeer = peer;
        return true;
    }

    public bool ENetJoinServer()
    {
        ENetMultiplayerPeer peer = new();
        Error error = peer.CreateClient(IP_ADDRESS, PORT);

        if (error != Error.Ok)
        {
            GD.Print($"Cannot join: {error}");
            return false;
        }

        Multiplayer.MultiplayerPeer = peer;
        return true;
    }

    void ServerClosed()
    {
        GetTree().ReloadCurrentScene();
    }
}
