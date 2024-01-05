using Godot;
using System;

public partial class Start : StaticBody2D
{
    public Marker2D SpawnPosition { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SpawnPosition = GetNode<Marker2D>("SpawnPosition");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public Vector2 GetSpawnPosition()
    {
        return SpawnPosition.GlobalPosition;
    }
}
