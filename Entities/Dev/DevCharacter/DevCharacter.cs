using Godot;
using System;

namespace Development
{
    [Icon("uid://bq7xbel5bwuid")]

    public partial class DevCharacter : CharacterBody3D
    {
        Camera3D camera;
        CollisionShape3D collisionShape;
        Node3D cameraPivot;

        float moveSpeed = 10f;
        float gravityForce = 9.8f;
        float jumpForce = 5f;

        float maxCameraAngle = 90f;
        float minCameraAngle = 90f;

        float mouseSensitivity = 0.01f;

        public override void _EnterTree()
        {
            SetMultiplayerAuthority(Name.ToString().ToInt());
        }

        public override void _Ready()
        {
            GetNode<Label3D>("%Label3D").Text = Name.ToString();

            if (!IsMultiplayerAuthority())
            {
                SetProcess(false);
                SetPhysicsProcess(false);
                SetProcessUnhandledInput(false);
                return;
            }

            Input.MouseMode = Input.MouseModeEnum.Captured;

            camera = GetNode<Camera3D>("%Camera3D");
            collisionShape = GetNode<CollisionShape3D>("%CollisionShape3D");
            cameraPivot = GetNode<Node3D>("%CameraPivot");

            camera.Current = true;
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 _input = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

            if (Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                normalMovement(_input);
            }
            else
            {
                Velocity *= Vector3.Up;
            }

            if (!IsOnFloor()) Velocity += Vector3.Down * gravityForce * (float)delta;

            MoveAndSlide();
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton && Input.MouseMode == Input.MouseModeEnum.Visible) Input.MouseMode = Input.MouseModeEnum.Captured;

            if (@event is InputEventKey)
            {
                if (Input.IsActionJustPressed("escape") && Input.MouseMode == Input.MouseModeEnum.Captured) 
                {
                    Input.MouseMode = Input.MouseModeEnum.Visible;
                }
            }

            if (@event is InputEventMouseMotion _motion) rotateCamera(_motion.Relative);
        }

        void normalMovement(Vector2 input)
        {
            if (collisionShape.Disabled) collisionShape.Disabled = false;
            Vector3 _moveVector = cameraPivot.GlobalBasis.X * input.X + cameraPivot.GlobalBasis.Z * input.Y;
            Velocity = (_moveVector * moveSpeed) + (Vector3.Up * Velocity.Y);
            
            if (IsOnFloor() && Input.IsActionJustPressed("moveJump")) Velocity += Vector3.Up * jumpForce;
            
        }

        void rotateCamera(Vector2 mouseMotion)
        {
            if (Input.MouseMode != Input.MouseModeEnum.Captured) return;

            cameraPivot.RotateY(-mouseMotion.X * mouseSensitivity);
            camera.RotateX(-mouseMotion.Y * mouseSensitivity);

            float _verticalCameraClamp = Mathf.Clamp(camera.Rotation.X, -Mathf.DegToRad(minCameraAngle), Mathf.DegToRad(maxCameraAngle));
            camera.Rotation = new Vector3(_verticalCameraClamp, camera.Rotation.Y, camera.Rotation.Z);
        }
    }
}
