using Godot;
using System;


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

    public Action EnemyDied;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimPlayer = (AnimationPlayer)GetNode("AnimationPlayer");

        GetStateMachine();

        AnimPlayer.Play("Idle");
        Sprite = (Sprite2D)GetNode("Enemy");

        _detectPlayer = GetNode<Area2D>("DetectPlayer");

        _detectPlayer.BodyEntered += DetectPlayer;

        _death = GetNode<Area2D>("Area2D");
        _death.BodyEntered += KillPlayer;

        Gravity = DefaultGravity;

        ResetEnemy();

        Destroyed += EnemyDie;
    }

    public void DetectPlayer(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("RAY");
            var spaceState = GetWorld2D().DirectSpaceState;
            var query = PhysicsRayQueryParameters2D.Create(Vector2.Zero, new Vector2(50, 100));
            var result = spaceState.IntersectRay(query);
            //PlayerDetected = result["collider"] is Player;
        }
    }

    public void EnemyDie() => EnemyDied.Invoke();


    public void ResetEnemy()
    {
        ResetActor();
        // Reset their position, and state
        StateMachine.PrintStates();
        StateMachine.InitState("PATROL");
    }

    public override void _PhysicsProcess(double delta)
    {
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
