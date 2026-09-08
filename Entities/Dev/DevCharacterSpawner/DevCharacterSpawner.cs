using Godot;
using System.Collections.Generic;

public partial class DevCharacterSpawner : Marker3D
{
    [Export]
    PackedScene devCharacter;

    Node3D devCharacterContainer;

    Dictionary<string, Node> spawnedDevCharacters = [];

    public override void _Ready()
    {
        Multiplayer.PeerConnected += addDevCharacter;
        Multiplayer.PeerDisconnected += removeDevCharacter;
        ENetworkManager.Instance.ENetServerCreated += addDevCharacter;
    }
    
    void createDevCharacterContainer()
    {
        devCharacterContainer = new()
        {
            Name = "DevCharacters",
        };

        Global.MainScene.EntityRoot.AddChild(devCharacterContainer);

        MultiplayerSpawner _multiplayerSpawner = new()
        {
            Name = "DevCharacterSpawner",
            SpawnFunction = new(this, MethodName.spawnDevCharacter),
            SpawnPath = GetPathTo(devCharacterContainer),
        };

        _multiplayerSpawner.AddSpawnableScene(devCharacter.ResourcePath);

        devCharacterContainer.AddChild(_multiplayerSpawner);
    }

    public void addDevCharacter(long peerID)
    {
        if (!IsInstanceValid(devCharacterContainer)) createDevCharacterContainer();
        if (!Multiplayer.IsServer()) return;
        devCharacterContainer.GetNode<MultiplayerSpawner>("DevCharacterSpawner").Spawn(peerID);
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
