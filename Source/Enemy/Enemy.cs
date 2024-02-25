using Godot;
using System;

public enum EnemyDirection { LEFT = 0, RIGHT = 1 };

public partial class Enemy : Actor
{
    public AnimationPlayer Player;

    [Export]
    public float MaxDetectDistance;

    RayCast2D playerDetect;
    RayCast2D playerDetect2;

    private Area2D _Death;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = (AnimationPlayer)GetNode("AnimationPlayer");
        _StateMachine = (StateMachine)GetNode("StateMachine");
        playerDetect = (RayCast2D)GetNode("raycasts/PlayerDetect");
        playerDetect2 = (RayCast2D)GetNode("raycasts/PlayerDetect2");

        Player.Play("Idle");
        Sprite = (Sprite2D)GetNode("Enemy");

        _StateMachine.InitState("PATROL");

        _Death = GetNode<Area2D>("Area2D");
        _Death.BodyEntered += KillPlayer;

        Gravity = DefaultGravity;
    }

    public void Destroy()
    {
        Sprite = null;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            var _Velocity = Velocity;
            _Velocity.Y += (float)delta * Gravity;
            Velocity = _Velocity;
        }

        GD.Print("Vel: ", Velocity);
        MoveAndSlide();
    }

    public void KillPlayer(object area)
    {
        if (area is Player)
        {
            Player dead = area as Player;
            dead.SetState("DEATH");
        }
    }
}
