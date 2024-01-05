using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
    public Player Player { get; set; }
    public Start Start { get; set; }
    public Exit Exit { get; set; }
    public Area2D DeathZone { get; set; }
    public Timer TimerNode { get; set; }
    public UiLayer UiLayer { get; set; }
    private AudioPlayer audioPlayer;
    public int TimeLeft { get; set; }
    [Export]
    public int LevelTime { get; set; } = 40;
    public Hud Hud { get; set; }
    [Export]
    public PackedScene NextLevel { get; set; } = null;
    public bool Win { get; set; } = false;
    [Export]
    public bool IsFinalLevel { get; set; } = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = GetNode<Player>("Player");
        Start = GetNode<Start>("Start");
        Exit = GetNode<Exit>("Exit");
        DeathZone = GetNode<Area2D>("DeathZone");
        Hud = GetNode<Hud>("UILayer/HUD");
        UiLayer = GetNode<UiLayer>("UILayer");
        audioPlayer = GetNode<AudioPlayer>("/root/AudioPlayer");
        Player.GlobalPosition = Start.GetSpawnPosition();
        Exit.BodyEntered += _on_exit_body_entered;
        DeathZone.BodyEntered += _on_death_zone_body_entered;
        var traps = GetTree().GetNodesInGroup("traps");
        foreach (var trap in traps)
        {
            (trap as Trap).OnTouchedPlayer += _on_trap_on_touched_player;
        }

        TimerNode = new Timer();
        TimerNode.Name = "Level Timer";
        TimerNode.WaitTime = 1;
        TimerNode.Timeout += _on_level_timer_timeout;
        AddChild(TimerNode);
        TimerNode.Start();
        TimeLeft = LevelTime;
        Hud.SetTimeLabel(TimeLeft);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("quit"))
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
        if (body is Player)
        {
            audioPlayer.PlaySfx("hurt");
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        Player.Velocity = Vector2.Zero;
        Player.GlobalPosition = Start.GetSpawnPosition();
    }

    public void _on_trap_on_touched_player()
    {
        audioPlayer.PlaySfx("hurt");
        ResetPlayer();
    }

    public async void _on_exit_body_entered(Node2D body)
    {
        if (body is Player)
        {
            if (IsFinalLevel || NextLevel != null)
            {
                Exit.Animate();
                Player.Active = false;
                Win = true;
                await ToSignal(GetTree().CreateTimer(1.5), "timeout");
                if(IsFinalLevel)
                {
                    UiLayer.ShowWinScreen(true);
                }
                else
                {
                    GetTree().ChangeSceneToPacked(NextLevel);
                }
            }
        }
    }

    public void _on_level_timer_timeout()
    {
        if (!Win)
        {
            TimeLeft -= 1;
            Hud.SetTimeLabel(TimeLeft);

            if (TimeLeft < 0)
            {
                ResetPlayer();
                TimeLeft = LevelTime;
                Hud.SetTimeLabel(TimeLeft);

            }
        }
    }
}
