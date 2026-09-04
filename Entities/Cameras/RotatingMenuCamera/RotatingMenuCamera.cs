using Godot;
using System;

[Icon("uid://jt37krmpsgnt")]

public partial class RotatingMenuCamera : Node3D
{
    [Export] private float rotationSpeed = 7.5f;
    public override void _Process(double delta)
    {
        RotateY(Mathf.DegToRad(rotationSpeed) * (float)delta);
    }
}
