using Godot;
using System.Collections.Generic;

public partial class DevCharacterSpawner : Marker3D
{
    [Export]
    PackedScene player;

    Dictionary<string, Node3D> spawnedPlayers = [];

    public override void _Ready()
    {
        Multiplayer.PeerConnected += addPlayer;
        Multiplayer.PeerDisconnected += removePlayer;
    }


    private void addPlayer(long peerID)
    {
        GlobalMultiplayerSpawner.Instance.SpawnFunction = new(this, MethodName.createPlayerInstance);
        
        if (!Multiplayer.IsServer()) return;
        GlobalMultiplayerSpawner.Instance.Spawn(peerID);
    }


    private void removePlayer(long peerID)
    {
        if (!Multiplayer.IsServer()) return;

        Node _playerToRemove = spawnedPlayers[peerID.ToString()];

        spawnedPlayers.Remove(peerID.ToString());

        _playerToRemove.QueueFree();
    }

    private Node createPlayerInstance(long id)
    {
        Node3D _newPlayer = (Node3D)player.Instantiate();
        _newPlayer.Name = id.ToString();
        spawnedPlayers[_newPlayer.Name] = _newPlayer;
        _newPlayer.Position = GlobalPosition + (Vector3.Forward * GD.RandRange(-10, 10)) + (Vector3.Right * GD.RandRange(-10, 10));
        return _newPlayer;
    }
}
