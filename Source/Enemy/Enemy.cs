using Godot;
using System;

public enum EnemyDirection { LEFT = 0, RIGHT = 1 };

public partial class Enemy : Actor
{
    public AnimationPlayer Player;


    RayCast2D playerDetect;
    RayCast2D playerDetect2;

    Area2D _death;

    Vector2 inputVelocity;


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

        _death = GetNode<Area2D>("Area2D");
        _death.BodyEntered += KillPlayer;

        Gravity = DefaultGravity;
    }


    public void Destroy()
    {
        Sprite = null;
    }

    public void Attack()
    {
        Node2D rayArray = (Node2D)GetNode("raycasts");

        if (playerDetect.IsColliding() || playerDetect2.IsColliding())
        {
            // Play attacking Anim. Chase Player ... YARRR!
            // Yer going to be needin ya a hitbox as well laddy.
            GD.Print("Detected Player");
            Player.Play("Attack");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        inputVelocity.Y += (float)delta * Gravity;

        //Velocity = Move(delta);
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
