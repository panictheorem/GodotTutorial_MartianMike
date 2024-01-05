using Godot;
using System;

public partial class AudioPlayer : Node
{
    public AudioStream HurtSound { get; set; }
    public AudioStream JumpSound { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        HurtSound = GD.Load<AudioStream>("res://assets/audio/hurt.wav");
        JumpSound = GD.Load<AudioStream>("res://assets/audio/jump.wav");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public async void PlaySfx(string sfxName)
	{
        var asp = new AudioStreamPlayer();
        AudioStream stream = null;
        if (sfxName == "hurt")
        {
            stream = HurtSound;
        }
        else if (sfxName =="jump")
        {
            stream = JumpSound;
        }
        else
        {
            GD.Print("Invalid sfx name");
            return;
        }
        asp.Stream = stream;
        asp.Name = "SFX";
        AddChild(asp);
        asp.Play();

        await ToSignal(asp, "finished");
        asp.QueueFree();
    }
}
