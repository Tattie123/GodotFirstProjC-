using Godot;
using System;
using System.Security.Cryptography;

public partial class Enemy : CharacterBody3D
{
    public Gun gun;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private float _movementSpeed = 4.0f;
    private NavigationAgent3D _navigationAgent;
    public Vector3 _movementTargetPosition;
    float randomFloat1 = GD.RandRange(-10, 10);
    float randomFloat2 = GD.RandRange(-10, 10);

    public async void Randomnav()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        Vector3 _movementTargetPosition = new Vector3(randomFloat1, 0.0f, randomFloat2);
        MovementTarget = _movementTargetPosition;
    }
    async void Respawn()
    {
        await ToSignal(GetTree().CreateTimer(0.01f), SceneTreeTimer.SignalName.Timeout);
        this.Name = ("Enemy");
    }

    public void Shot()
    {
        Free();
    }

    public Vector3 MovementTarget
    {
        get { return _navigationAgent.TargetPosition; }
        set { _navigationAgent.TargetPosition = value; }
    }

    public override void _Ready()
    {
        Respawn(); 
        GD.Randomize();

        base._Ready();

        _navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        _navigationAgent.PathDesiredDistance = 0.5f;
        _navigationAgent.TargetDesiredDistance = 0.5f;

        Randomnav();
    }

    public override void _PhysicsProcess(double delta)
    {
        if(gun == null)
        {
            gun = (Gun)GetNode("/root/TestLevel/Player/Camera3D/Gun");
        }

        Vector3 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y -= gravity * (float)delta;
        }
        Velocity = velocity;

        base._PhysicsProcess(delta);

        if (_navigationAgent.IsNavigationFinished())
        {
            return;
        }

        Vector3 currentAgentPosition = GlobalTransform.Origin;
        Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();

        Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * _movementSpeed;

        MoveAndSlide();
    }
}