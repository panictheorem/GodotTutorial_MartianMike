using Godot;
using System;

public partial class Hud : Control
{
    public Label TimeLabel { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        TimeLabel = GetNode<Label>("TimeLabel");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void SetTimeLabel(int value)
    {
        TimeLabel.Text = $"TIME: {value}";
    }
}
