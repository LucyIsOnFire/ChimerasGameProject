using Godot;
using System;
using System.Collections.Generic;

public partial class PhysicsPlayer : Node3D
{
    [Export]
    float linearSpringStiffness = 1200f, linearSpringDamping = 40f, angularSpringStiffness = 4000f, angularSpringDamping = 80f;
    
    PhysicalBoneSimulator3D boneSimulator;
    Skeleton3D targetSkeleton, physicsSkeleton;
    List<PhysicalBone3D> physicsBones = new();

    public override void _Ready()
    {
        boneSimulator = GetNode<PhysicalBoneSimulator3D>("%PhysicalBoneSimulator3D");
        targetSkeleton = GetNode<Skeleton3D>("%Skeleton3D");
        physicsSkeleton = GetNode<Skeleton3D>("%PhysicsSkeleton3D");

        foreach(Node _child in boneSimulator.GetChildren())
        {
            if (_child is PhysicalBone3D _bone) physicsBones.Add(_bone);
        }

        boneSimulator.PhysicalBonesStartSimulation();
    }

    public override void _PhysicsProcess(double delta)
    {
        foreach(PhysicalBone3D _bone in physicsBones)
        {
            Transform3D _targetTransform = targetSkeleton.GlobalTransform * targetSkeleton.GetBoneGlobalPose(_bone.GetBoneId());
            Transform3D _currentTransform = physicsSkeleton.GlobalTransform * physicsSkeleton.GetBoneGlobalPose(_bone.GetBoneId());

            Vector3 _positionDifference = _targetTransform.Origin - _currentTransform.Origin;
            Vector3 _force = hookesLaw(_positionDifference, _bone.LinearVelocity, linearSpringStiffness, linearSpringDamping);
            _bone.LinearVelocity += _force * (float)delta;

            Basis _rotationDifference = _targetTransform.Basis * _currentTransform.Basis.Inverse();
            Vector3 _torque = hookesLaw(_rotationDifference.GetEuler(), _bone.AngularVelocity, angularSpringStiffness, angularSpringDamping);
            _bone.AngularVelocity += _torque * (float)delta;
        }
    }

    Vector3 hookesLaw(Vector3 displacement, Vector3 currentVelocity, float stiffness, float damping)
    {
        return (stiffness * displacement) - (damping * currentVelocity);
    }
}
