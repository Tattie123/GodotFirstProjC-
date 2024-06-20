using Godot;
using System;
using System.Security.Cryptography;

public partial class Enemy : CharacterBody3D
{

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private float _movementSpeed = 2.0f;
    private NavigationAgent3D _navigationAgent;
    public Vector3 _movementTargetPosition;
    float randomFloat1 = GD.RandRange(-10, 10);
    float randomFloat2 = GD.RandRange(-10, 10);
    int count = 0;
    public async void Randomnav()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        GD.Print(randomFloat1 + " random floats");
        Vector3 _movementTargetPosition = new Vector3(randomFloat1, 0.0f, randomFloat2);
        MovementTarget = _movementTargetPosition;
    }
    
    public void Shot()
    {
        GD.Print("enemy_hit POW!");
        QueueFree();
        count++;
        GD.Print(count);
        if (count == 2)
        {
            GetTree().Quit();
        }
    }

    public Vector3 MovementTarget
    {
        get { return _navigationAgent.TargetPosition; }
        set { _navigationAgent.TargetPosition = value; }
    }

    public override void _Ready()
    {

        GD.Randomize();

        base._Ready();

        _navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");

        // These values need to be adjusted for the actor's speed
        // and the navigation layout.
        _navigationAgent.PathDesiredDistance = 0.5f;
        _navigationAgent.TargetDesiredDistance = 0.5f;

        // Make sure to not await during _Ready.
        Randomnav();
    }

    public override void _PhysicsProcess(double delta)
    {
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