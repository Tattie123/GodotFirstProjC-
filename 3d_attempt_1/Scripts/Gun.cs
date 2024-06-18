using Godot;
using System;

public partial class Gun : MeshInstance3D
{
    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionJustPressed("Shoot"))
        {
            GD.Print("test_shoot");
            if (GetNode<RayCast3D>("GunRayCast").IsColliding())
            {
                GD.Print("test_hit");
            }

        }

    }


}
