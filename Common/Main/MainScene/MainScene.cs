using Godot;

[GlobalClass, Icon("uid://dt3nucrb8lx5r")]

public partial class MainScene : Node
{
    public Node3D LevelRoot, EntityRoot, EffectRoot;
    public Control HUDRoot, PauseRoot, TransitionRoot, DebugRoot;

    public override void _Ready()
    {
        LevelRoot = GetNode<Node3D>("%LevelRoot");
        EntityRoot = GetNode<Node3D>("%EntityRoot");
        EffectRoot = GetNode<Node3D>("%EffectRoot");

        HUDRoot = GetNode<Control>("%HUDRoot");
        PauseRoot = GetNode<Control>("%PauseRoot");
        TransitionRoot = GetNode<Control>("%TransitionRoot");
        DebugRoot = GetNode<Control>("%DebugRoot");
    }
}
