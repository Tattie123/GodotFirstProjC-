using Godot;
using System;

public partial class Area3D : Godot.Area3D
{
	public void __input()
	{
		if (Input.IsActionJustPressed("Crouch"))
		{
			GetNode<Area3D>("Area3DCollision").Monitoring = true;
            //StartTimer();
        }
	}
	public void __timer()
	{
        GetNode<Area3D>("Area3DCollision").Monitoring = false;
    }

}
