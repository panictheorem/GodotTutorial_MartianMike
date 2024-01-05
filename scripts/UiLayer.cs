using Godot;
using System;

public partial class UiLayer : CanvasLayer
{
    public WinScreen WinScreen { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        WinScreen = GetNode<WinScreen>("WinScreen");

    }
    public void ShowWinScreen(bool flag)
    {
        WinScreen.Visible = flag;
    }
}
