using Godot;
using System;

[GlobalClass]
public partial class Saw : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Test()
	{
		GD.Print("Test 123");
	}
}
