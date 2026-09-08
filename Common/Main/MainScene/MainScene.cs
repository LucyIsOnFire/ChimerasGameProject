using Godot;

[GlobalClass, Icon("uid://dt3nucrb8lx5r")]

public partial class MainScene : Node
{
    public Node SystemsHead, UIHead;
    public Node3D WorldHead, LevelRoot, EntityRoot, EffectRoot;
    public Control HUDRoot, PauseRoot, TransitionRoot, DebugRoot;

    public override void _EnterTree()
    {
        Global.SetMainScene(this);

        SystemsHead = GetNode<Node>("%SystemsHead");
        WorldHead = GetNode<Node3D>("%WorldHead");
        UIHead = GetNode<Node>("%UIHead");

        LevelRoot = GetNode<Node3D>("%LevelRoot");
        EntityRoot = GetNode<Node3D>("%EntityRoot");
        EffectRoot = GetNode<Node3D>("%EffectRoot");

        HUDRoot = GetNode<Control>("%HUDRoot");
        PauseRoot = GetNode<Control>("%PauseRoot");
        TransitionRoot = GetNode<Control>("%TransitionRoot");
        DebugRoot = GetNode<Control>("%DebugRoot");
    }
}
