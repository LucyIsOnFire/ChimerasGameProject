using Godot;
using System;

[GlobalClass, Icon("uid://dt3nucrb8lx5r")]

public partial class Main : Node
{
    public override void _Ready()
    {
        if (OS.HasFeature("server")) NetworkHandler.Instance.CreateServer();
    }
}
