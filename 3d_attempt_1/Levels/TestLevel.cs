using Godot;
using System;

public partial class TestLevel : Node3D
{
    public Enemy enemy;

    float Spawnloc = GD.RandRange(1, 2);
    int counter = 0; 
    enemy.count = Counter;



    public override void _Ready()
    {
        AddToGroup("level");
    }

    public void Enemy_death()
    {

    }


    public override void _Process(double delta)
	{
        GD.Print(Spawnloc);
        if (enemy == null)
        {
            enemy = (Enemy)GetNode("/root/TestLevel/Enemy");
        }
    }
}
