using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 125.0f;
    public const float JumpVelocity = 200.0f;
    public const float Gravity = 400.0f;
    public bool Active { get; set; } = true;
    public AnimatedSprite2D AnimatedSprite2D { get; set; }
    private AudioPlayer audioPlayer;
    public override void _Ready()
    {
        AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        audioPlayer = GetNode<AudioPlayer>("/root/AudioPlayer");
    }


    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        //Velocity = Vector2.Zero;
        if (!IsOnFloor())
        {

            velocity.Y += Gravity * (float)delta;
            if (velocity.Y > 600)
            {
                velocity.Y = 600;
            }
        }
        var direction = 0.0f;

        if (Active)
        {
            direction = Input.GetAxis("move_left", "move_right");
        }
        if (direction != 0)
        {
            AnimatedSprite2D.FlipH = direction == -1;
        }

        velocity.X = Speed * direction;

        Velocity = velocity;

        if(Active)
        {
            if ((IsOnFloor() || IsOnWall()) && Input.IsActionJustPressed("jump"))
            {
                Jump(JumpVelocity);
            }
        }
        MoveAndSlide();
        UpdateAnimations(direction);
    }

    private void UpdateAnimations(float direction)
    {
        if (IsOnFloor())
        {
            if (direction == 0)
            {
                AnimatedSprite2D.Play("idle");
            }
            else
            {
                AnimatedSprite2D.Play("run");
            }
        }
        else
        {
            if (Velocity.Y < 0)
            {
                AnimatedSprite2D.Play("jump");
            }
            else
            {
                AnimatedSprite2D.Play("fall");
            }
        }
    }

    public void Jump(float force)
    {
        audioPlayer.PlaySfx("jump");
        Velocity = new Vector2(Velocity.X, -force);
    }
}
