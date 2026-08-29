using Godot;
using System;

namespace Development
{
    [GlobalClass, Icon("uid://bq7xbel5bwuid")]

    public partial class DevCharacter : CharacterBody3D
    {
        private Action<Vector2, float> movementDelegateAction;
        
        private Camera3D camera;
        private CollisionShape3D collisionShape;
        private Node3D cameraPivot;

        private bool noClipEnabled = false;

        private float moveSpeed = 10f;
        private float gravityForce = 9.8f;
        private float jumpForce = 5f;

        private float maxCameraAngle = 90f;
        private float minCameraAngle = 90f;

        private float mouseSensitivity = 0.01f;


        public override void _Ready()
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;

            camera = GetNode<Camera3D>("%Camera3D");
            collisionShape = GetNode<CollisionShape3D>("%CollisionShape3D");
            cameraPivot = GetNode<Node3D>("%CameraPivot");

            camera.Current = true;

            movementDelegateAction = NormalMovement;
        }


        public override void _PhysicsProcess(double delta)
        {
            Vector2 _input = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");

            movementDelegateAction = noClipEnabled ? NoClipMovement : NormalMovement;
            movementDelegateAction(_input, (float)delta);

            MoveAndSlide();
        }


        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton && Input.MouseMode == Input.MouseModeEnum.Visible)
            {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }

            if (@event is InputEventKey)
            {
                if (Input.IsActionJustPressed("noClipToggle")) noClipEnabled = !noClipEnabled;
                if (Input.IsActionJustPressed("escape") && Input.MouseMode == Input.MouseModeEnum.Captured) Input.MouseMode = Input.MouseModeEnum.Visible;
            }

            if (@event is InputEventMouseMotion _motion) RotateCamera(_motion.Relative);
        }


        //Method that gives the player a more normal movement and jump
        private void NormalMovement(Vector2 input, float delta)
        {
            if (collisionShape.Disabled) collisionShape.Disabled = false;
            Vector3 _moveVector = cameraPivot.GlobalBasis.X * input.X + cameraPivot.GlobalBasis.Z * input.Y;
            Velocity = (_moveVector * moveSpeed) + (Vector3.Up * Velocity.Y);
            
            if (IsOnFloor()) 
            {
                if (Input.IsActionJustPressed("moveJump")) Velocity += Vector3.Up * jumpForce;
            }
            else
            {
                Velocity += Vector3.Down * gravityForce * delta;
            }
        }


        //Method that gives the player more noclip/developer flight. Useful for debugging
        private void NoClipMovement(Vector2 input, float delta)
        {
            if (!collisionShape.Disabled) collisionShape.Disabled = true;
            
            Velocity = (cameraPivot.GlobalBasis.X * input.X + cameraPivot.GlobalBasis.Z * input.Y) * moveSpeed;

            if (Input.IsActionPressed("noClipUp")) Velocity += Vector3.Up * moveSpeed;
            if (Input.IsActionPressed("noClipDown")) Velocity += Vector3.Down * moveSpeed;
        }

        private void RotateCamera(Vector2 mouseMotion)
        {
            if (Input.MouseMode != Input.MouseModeEnum.Captured) return;

            cameraPivot.RotateY(-mouseMotion.X * mouseSensitivity);
            camera.RotateX(-mouseMotion.Y * mouseSensitivity);

            float _verticalCameraClamp = Mathf.Clamp(camera.Rotation.X, -Mathf.DegToRad(minCameraAngle), Mathf.DegToRad(maxCameraAngle));
            camera.Rotation = new Vector3(_verticalCameraClamp, camera.Rotation.Y, camera.Rotation.Z);
        }
    }
}
