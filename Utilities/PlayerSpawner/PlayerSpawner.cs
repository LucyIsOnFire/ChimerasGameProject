using Development;
using Godot;
using System;

public partial class PlayerSpawner : MultiplayerSpawner
{
    [Export]
    PackedScene networkPlayer;

    public override void _Ready()
    {
        Multiplayer.PeerConnected += spawnPlayer;
    }

    void spawnPlayer(long id)
    {
        if (!Multiplayer.IsServer()) return;

        Node _player = networkPlayer.Instantiate();
        _player.Name = id.ToString();

        GetNode<Node>(SpawnPath).CallDeferred(Node.MethodName.AddChild, _player);
    }
}
