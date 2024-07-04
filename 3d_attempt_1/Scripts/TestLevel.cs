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
        var scene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");
        var Enemy = scene.Instantiate();
        enemycount++;
        AddChild(Enemy);
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
