using Godot;
using System;

public enum EnemyDirection { LEFT = 0, RIGHT = 1 };

public partial class Enemy : Actor
{
    public AnimationPlayer AnimPlayer;

    [Export]
    public float MaxDetectDistance;

    private RayCast2D _lineOfSight;

    private Player _player;

    private Area2D _death;
    private Area2D _detectPlayer;

    public bool PlayerDetected;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimPlayer = (AnimationPlayer)GetNode("AnimationPlayer");

        AnimPlayer.Play("Idle");
        Sprite = (Sprite2D)GetNode("Enemy");

        _detectPlayer = GetNode<Area2D>("DetectPlayer");

        _detectPlayer.BodyEntered += DetectPlayer;

        StateMachine.InitState("PATROL");

        _death = GetNode<Area2D>("Area2D");
        _death.BodyEntered += KillPlayer;

        Gravity = DefaultGravity;
    }

    public void Destroy()
    {
        Sprite = null;
    }

    public void DetectPlayer(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("RAY");
            var spaceState = GetWorld2D().DirectSpaceState;
            var query = PhysicsRayQueryParameters2D.Create(Vector2.Zero, new Vector2(50, 100));
            var result = spaceState.IntersectRay(query);
            PlayerDetected = result["collider"] is Player;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            var _Velocity = Velocity;
            _Velocity.Y += (float)delta * Gravity;
            Velocity = _Velocity;
        }
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
