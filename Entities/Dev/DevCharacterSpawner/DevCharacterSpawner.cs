using Godot;
using System.Collections.Generic;

public partial class DevCharacterSpawner : Marker3D
{
    [Export]
    PackedScene devCharacter;

    Dictionary<string, Node> spawnedDevCharacters = [];

    public override void _Ready()
    {
        Multiplayer.PeerConnected += addDevCharacter;
        Multiplayer.PeerDisconnected += removeDevCharacter;
    }

    void addDevCharacter(long peerID)
    {
        //GlobalMultiplayerSpawner.Instance.SpawnFunction = new(this, MethodName.spawnDevCharacter);

        if (!Multiplayer.IsServer()) return;
        //GlobalMultiplayerSpawner.Instance.Spawn(peerID);
    }

    void removeDevCharacter(long peerID)
    {
        if (!Multiplayer.IsServer()) return;
        Node _playerToRemove = spawnedDevCharacters[peerID.ToString()];
        spawnedDevCharacters.Remove(peerID.ToString());
        _playerToRemove.QueueFree();
    }

    Node spawnDevCharacter(int peerID)
    {
        Node3D _instance = (Node3D)devCharacter.Instantiate();
        _instance.Name = peerID.ToString();
        spawnedDevCharacters[_instance.Name] = _instance;
        _instance.Position = GlobalPosition;
        return _instance;
    }
}
