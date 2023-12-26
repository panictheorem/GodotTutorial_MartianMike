using Godot;
using System;

public partial class JumpPad : Area2D
{
    [Export]
    public float JumpForce { get; set; } = 300;
    public AnimatedSprite2D AnimatedSprite2D { get; set; }

    public override void _Ready()
    {
        AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _on_body_entered(Node2D body)
    {
        var player = body as Player;
        if (player != null)
        {
            AnimatedSprite2D.Play("jump");
            player.Jump(JumpForce);
        }
    }
}
