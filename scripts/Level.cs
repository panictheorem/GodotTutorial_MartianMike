using Godot;
using System;

public partial class Level : Node2D
{
    public Player Player { get; set; }
    public Marker2D StartPosition { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Player = GetNode<Player>("Player");
        StartPosition = GetNode<Marker2D>("StartPosition");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("quit"))
		{
			GetTree().Quit();
		}

        if (Input.IsActionJustPressed("reset"))
        {
			GetTree().ReloadCurrentScene();
        }
    }

	public void _on_death_zone_body_entered(Node2D body)
	{
		var player = body as Player;
		if(player != null)
		{
			player.Velocity = Vector2.Zero;
			player.GlobalPosition = StartPosition.GlobalPosition;

        }
	}
}
