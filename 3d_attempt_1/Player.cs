using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody3D
{


	//Variable declerations (of war)
	public const float Speed = 5.0f;
	public const float CrouchedSpeed = 2.0f;
	public const float JumpVelocity = 4.5f;
	public const float Sensitivity = 50.0f;
	public bool ads = false;
	public bool iscrouched;
	public bool canstand;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	private Camera3D camera;



	//Functions

	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;

		camera = GetNode<Camera3D>("Camera3D");
	}

	public void OnEnter(Node Other)
	{
		canstand = false;
	}
	public void OnExit(Node Other)
	{
		canstand = true;
	}
	public void gunshoot()
	{
		
	}



	//main Proccess
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Vector2 inputDir = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		float speed = Speed;
		if(Input.IsActionJustPressed("Crouch") & !iscrouched)
		{
			canstand = true;
			GetNode<AnimationPlayer>("AnimationPlayercrouch").Play("Crouch");
			iscrouched = true;
			speed = CrouchedSpeed;
		}else if(Input.IsActionJustPressed("Crouch") & canstand == true)
		{
			GetNode<AnimationPlayer>("AnimationPlayercrouch").Play("UnCrouch");
			iscrouched = false;
			speed = Speed;
		}
		if (Input.IsActionJustPressed("click"))
		{
			if (ads == false)
			{
				GetNode<AnimationPlayer>("AnimationPlayergun").SpeedScale = 1;
				GetNode<AnimationPlayer>("AnimationPlayergun").Play("unadsshoot");
				//call function to shoot here
			}else if (ads == true)
			{
				GetNode<AnimationPlayer>("AnimationPlayergun").Play("adsshoot");
			}
		} 
		if (Input.IsActionJustPressed("ads"))
		{
			if (ads == false)
			{
				GetNode<AnimationPlayer>("AnimationPlayergun").SpeedScale = 1;
				GetNode<AnimationPlayer>("AnimationPlayergun").Play("ads");
				ads = true;
			}else if (ads == true)
			{
				GetNode<AnimationPlayer>("AnimationPlayergun").Play("unads");
				ads = false;
			}
		} 
		
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * speed;
			velocity.Z = direction.Z * speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}



	//Mouse look Function

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseMotion motion)
		{
			RotationDegrees = new Vector3(RotationDegrees.X, RotationDegrees.Y - motion.Relative.X / 1000 * Sensitivity, RotationDegrees.Z);

			if (camera != null)
			{
				camera.RotationDegrees = new Vector3(camera.RotationDegrees.X - motion.Relative.Y / 1000 * Sensitivity, camera.RotationDegrees.Y, camera.RotationDegrees.Z);
			}
		}
	}
}
