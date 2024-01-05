using Godot;
using System;

public partial class StartMenu : Control
{
    [Export]
    public PackedScene FirstLevel { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	public void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToPacked(FirstLevel);
	}

    public void _on_quit_button_pressed()
    {
        GetTree().Quit();
    }
}
