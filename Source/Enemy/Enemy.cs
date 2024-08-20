using Godot;
using System;


public partial class Enemy : Actor
{
    public AnimationPlayer AnimPlayer;

    [Export]
    public float MaxDetectDistance;

    private RayCast2D _LineOfSight;

    private Player _player;

    private Area2D _death;
    public bool PlayerDetected;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimPlayer = (AnimationPlayer)GetNode("AnimationPlayer");

        GetStateMachine();

        AnimPlayer.Play("Idle");
        Sprite = (Sprite2D)GetNode("Enemy");

        Gravity = DefaultGravity;

        _LineOfSight = GetNode<RayCast2D>("LineOfSight");

        ResetEnemy();

    }

    public bool DetectPlayer()
    {
        if (_LineOfSight.IsColliding())
        {
            return _LineOfSight.GetCollider() is Player;
        }
        return false;
    }


    public void DrawDebug()
    {
        DrawLine(_LineOfSight.Position, ToLocal(_LineOfSight.GetCollisionPoint()), new Color(1, 0, 0));
    }

    public void EnemyDie() => Destroyed.Invoke();


    public void ResetEnemy()
    {
        ResetActor();
        // Reset their position, and state
        StateMachine.PrintStates();
        StateMachine.InitState("PATROL");
    }

    public override void _PhysicsProcess(double delta)
    {
        //GD.Print(DetectPlayer());
        //GD.Print("Gravity: " + Gravity);
        ApplyGravity((float)delta);
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
