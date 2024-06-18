using Godot;
using System;
public partial class Gun : MeshInstance3D
{
    public Enemy enemy;

    public override void _Process(double delta)
	{
        if (enemy == null) {
            enemy = (Enemy)GetNode("/root/TestLevel/Enemy");
        }

        if (Input.IsActionJustPressed("Shoot"))
        {
            GD.Print("test_shoot");
            if (GetNode<RayCast3D>("GunRayCast").IsColliding())
            {
                GD.Print("test_hit");
                enemy.Shot();
            }

        }
        
    }
}
