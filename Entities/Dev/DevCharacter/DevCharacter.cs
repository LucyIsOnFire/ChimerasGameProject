using Godot;
using System;

namespace Development
{
    [GlobalClass, Icon("uid://bq7xbel5bwuid")]

    /// <summary>
    /// <para><A test CharacterBody3D for multiplayer and some general functionality<para>
    /// </summary>
    public partial class DevCharacter : CharacterBody3D
    {
        private Action<Vector2, float> movementDelegateAction;
        
        private Camera3D camera;
        private CollisionShape3D collisionShape;
        private Node3D cameraPivot;

        private bool noClipEnabled = false;
        private float jumpForce = 5f;
        private float maxCameraAngle = 90f;
        private float minCameraAngle = 90f;
        private float mouseSensitivity = 0.01f;
        private float moveSpeed = 10f;
        private bool hasMPAuthority = false;
        
        public override void _EnterTree()
        {
            
            SetMultiplayerAuthority(((string)Name).ToInt());
            hasMPAuthority = IsMultiplayerAuthority();
        }

        public override void _Ready()
        {
            if (!hasMPAuthority) return;

            Input.MouseMode = Input.MouseModeEnum.Captured;
            camera = GetNode<Camera3D>("%Camera3D");
            collisionShape = GetNode<CollisionShape3D>("%CollisionShape3D");
            cameraPivot = GetNode<Node3D>("%CameraPivot");

            camera.Current = true;

            movementDelegateAction = NormalMovement;
        }

        public override void _PhysicsProcess(double delta)
        {
            if (!hasMPAuthority) return;
            
            Vector2 _input = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

            movementDelegateAction = noClipEnabled ? NoClipMovement : NormalMovement;
            movementDelegateAction(_input, (float)delta);

            MoveAndSlide();
        }

        public override void _Input(InputEvent @event)
        {
            if (!hasMPAuthority) return;

            if (@event is InputEventMouseButton && Input.MouseMode == Input.MouseModeEnum.Visible)
            {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }

            if (@event is InputEventKey)
            {
                if (Input.IsActionJustPressed("noClipToggle")) noClipEnabled = !noClipEnabled;
                if (Input.IsActionJustPressed("escape") && Input.MouseMode == Input.MouseModeEnum.Captured) Input.MouseMode = Input.MouseModeEnum.Visible;
            }

            if (@event is InputEventMouseMotion _motion)
            {
                cameraPivot.RotateY(-_motion.Relative.X * mouseSensitivity);
                camera.RotateX(-_motion.Relative.Y * mouseSensitivity);
                camera.Rotation = new Vector3(Mathf.Clamp(camera.Rotation.X, -Mathf.DegToRad(minCameraAngle), Mathf.DegToRad(maxCameraAngle)), camera.Rotation.Y, camera.Rotation.Z);
            }
        }

        private void NormalMovement(Vector2 input, float delta)
        {
            if (collisionShape.Disabled) collisionShape.Disabled = false;

            float _velocityY = Velocity.Y;
            Velocity = (cameraPivot.GlobalBasis.X * input.X + cameraPivot.GlobalBasis.Z * input.Y) * moveSpeed;
            Velocity = new Vector3(Velocity.X, _velocityY, Velocity.Z);
            Velocity -= new Vector3(0, 9.8f, 0) * delta;
            
            if (Input.IsActionJustPressed("moveJump") && IsOnFloor()) Velocity += Vector3.Up * jumpForce;
        }

        private void NoClipMovement(Vector2 input, float delta)
        {
            if (!collisionShape.Disabled) collisionShape.Disabled = true;
            
            Velocity = (cameraPivot.GlobalBasis.X * input.X + cameraPivot.GlobalBasis.Z * input.Y) * moveSpeed;

            if (Input.IsActionPressed("noClipUp")) Velocity += Vector3.Up * moveSpeed;
            if (Input.IsActionPressed("noClipDown")) Velocity += Vector3.Down * moveSpeed;
        }
    }
}
