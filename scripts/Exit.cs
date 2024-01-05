using Godot;
using System;

public partial class Exit : Area2D
{
    public AnimatedSprite2D AnimatedSprite2D { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void Animate()
    {
        AnimatedSprite2D.Play("default");
    }
}
