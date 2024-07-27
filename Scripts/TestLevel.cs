using Godot;
using System;

public partial class TestLevel : Node3D
{
    [Export]
        public PackedScene MobScene { get; set; }


    public Enemy enemy;

    public int enemycount;
    public void Shot()
    {
        // Load the PackedScene of the Enemy object.
        var scene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");

        // Instantiate a new Enemy object from the PackedScene.
        var Enemy = scene.Instantiate();

        // Increment the enemy count.
        enemycount++;

        // Add the Enemy object as a child to the current TestLevel node.
        AddChild(Enemy);

        // Print the current enemy count to the console.
        GD.Print(enemycount);
    }

    public override void _Ready()
    {
        AddToGroup("level");
    }


    public override void _Process(double delta)
	{
        if (enemy == null)
        {
            enemy = (Enemy)GetNode("Enemy");
        }
    }    
}
