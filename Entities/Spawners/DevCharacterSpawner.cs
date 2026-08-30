using Godot;
using System;
using System.Collections.Generic;

public partial class DevCharacterSpawner : Marker3D
{
    [Export]
    PackedScene player;

    Dictionary<string, Node> spawnedPlayers = [];

    public override void _Ready()
    {
        Multiplayer.PeerConnected += addPlayer;
        Multiplayer.PeerDisconnected += removePlayer;
    }


    private void addPlayer(long peerID)
    {
        if (!Multiplayer.IsServer()) return;

        Node3D _newPlayer = (Node3D)player.Instantiate();
        _newPlayer.Name = peerID.ToString();
        GlobalMultiplayerSpawner.ParentScene.AddChild(_newPlayer);

        spawnedPlayers[_newPlayer.Name] = _newPlayer;
    }


    private void removePlayer(long peerID)
    {
        if (!Multiplayer.IsServer()) return;

        Node _playerToRemove = spawnedPlayers[peerID.ToString()];

        spawnedPlayers.Remove(peerID.ToString());

        _playerToRemove.QueueFree();
    }
}
