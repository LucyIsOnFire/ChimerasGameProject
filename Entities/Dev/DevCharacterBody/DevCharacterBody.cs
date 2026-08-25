using Godot;
using System;

namespace Dev
{
    [GlobalClass, Icon("uid://bq7xbel5bwuid")]

    /// <summary>
    /// <para><A test CharacterBody3D for multiplayer and some general functionality<para>
    /// </summary>
    public partial class DevCharacterBody : CharacterBody3D
    {
    
        private Camera3D camera;
        private float moveSpeed = 10f;
        private float mouseSensitivity = 0.01f;
        private Vector2 input;
        
        public override void _Ready()
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            camera = GetNode<Camera3D>("%Camera3D");
        }

        public override void _Process(double delta)
        {
            input = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
        }

        public override void _PhysicsProcess(double delta)
        {
            Velocity = (camera.GlobalBasis.X * input.X + camera.GlobalBasis.Z * input.Y) * moveSpeed;
            MoveAndSlide();
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            
            
            if (@event is InputEventMouseMotion _motion)
            {
                camera.Rotation -= new Vector3(_motion.Relative.Y, _motion.Relative.X, 0) * mouseSensitivity;
                camera.Rotation = new Vector3(Mathf.Clamp(camera.Rotation.X, -Mathf.DegToRad(90), Mathf.DegToRad(90)), camera.Rotation.Y, camera.Rotation.Z);
            }
        }
    }
}
