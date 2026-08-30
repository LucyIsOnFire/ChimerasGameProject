using Godot;
using System;

public partial class GlobalMultiplayerSpawner : MultiplayerSpawner
{
    public static GlobalMultiplayerSpawner Instance;
    public static Node ParentScene;

    public override void _Ready()
    {
        Instance = this;
        ParentScene = GetNode(SpawnPath);
    }
}
