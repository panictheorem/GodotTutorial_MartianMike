using Godot;
using System;
using System.Reflection;

public partial class Bg : ParallaxBackground
{
    [Export]
    public float ScrollSpeed { get; set; } = 15.0f;
    [Export]
    public CompressedTexture2D BgTexture { get; set; }
    public Sprite2D Sprite { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Sprite = GetNode<Sprite2D>("ParallaxLayer/Sprite2D");
        Sprite.Texture = BgTexture;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        var spritePos = Sprite.RegionRect.Position;
        var spriteSize = Sprite.RegionRect.Size;
        var scrollVector = new Vector2(ScrollSpeed, ScrollSpeed);

        Sprite.RegionRect = new Rect2(spritePos + (float)delta * scrollVector, spriteSize);

        if(spritePos >= new Vector2(64, 64))
        {
            Sprite.RegionRect = new Rect2(Vector2.Zero, spriteSize);
        }

    }
}
