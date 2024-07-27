using Godot;
using System;
using static Godot.GD;
public partial class Gun : MeshInstance3D
{
    public Enemy enemy;
    public TestLevel testLevel;


    async void EnemyShot()
    {
        testLevel.Shot();
        enemy.Shot();
        await ToSignal(GetTree().CreateTimer(0.02f), SceneTreeTimer.SignalName.Timeout);
        enemy = (Enemy)GetNode("/root/TestLevel/Enemy");
        GD.Print("linked");
    }

    public override void _Process(double delta)
	{
        if (enemy == null) {
            enemy = (Enemy)GetNode("/root/TestLevel/Enemy");
        }
        if (testLevel == null) {
            testLevel = (TestLevel)GetNode("/root/TestLevel");
        }

        if (Input.IsActionJustPressed("Shoot"))
        {
            if (GetNode<RayCast3D>("GunRayCast").IsColliding())
            {
                EnemyShot();
            }

        }
        
    }
}
