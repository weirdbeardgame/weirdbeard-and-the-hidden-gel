using Godot;
using System;

public enum EnemyDirection { LEFT = 0, RIGHT = 1 };

public partial class Enemy : Actor
{
    public AnimationPlayer Player;

    RayCast2D Left;
    RayCast2D Right;
    RayCast2D playerDetect;
    RayCast2D playerDetect2;

    Sprite2D sprite;

    Area2D _death;

    Vector2 dir;
    Vector2 inputVelocity;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = (AnimationPlayer)GetNode("AnimationPlayer");
        stateMachine = (StateMachine)GetNode("StateMachine");
        playerDetect = (RayCast2D)GetNode("raycasts/PlayerDetect");
        playerDetect2 = (RayCast2D)GetNode("raycasts/PlayerDetect2");

        Player.Play("Idle");
        Left = (RayCast2D)GetNode("Left");
        sprite = (Sprite2D)GetNode("Enemy");
        Right = (RayCast2D)GetNode("Right");

        dir = new Vector2();
        _death = GetNode<Area2D>("Area2D");
        _death.BodyEntered += KillPlayer;

        gravity = defaultGravity;
    }

    public void ChangeDirection(EnemyDirection dirToWalk)
    {
        sprite = (Sprite2D)GetNode("Enemy");
        switch (dirToWalk)
        {
            case EnemyDirection.LEFT:
                sprite.FlipH = false;
                dir.X = -1.0f;
                break;

            case EnemyDirection.RIGHT:
                sprite.FlipH = true;
                dir.X = 1.0f;
                break;
        }
    }

    public Vector2 Move(double delta)
    {
        if (!Right.IsColliding())
        {
            ChangeDirection(EnemyDirection.LEFT);
        }
        else if (!Left.IsColliding())
        {
            ChangeDirection(EnemyDirection.RIGHT);
        }

        inputVelocity.Y += (float)delta * gravity;
        inputVelocity.X = dir.X * speed;

        //GD.Print("Velocity: ", inputVelocity);

        return inputVelocity;
    }

    public void Destroy()
    {
        sprite = null;
    }

    public void FollowPlayer(Player body)
    {
        dir = (body.GlobalPosition - GlobalPosition);
        LookAt(dir);
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
            FollowPlayer((Player)playerDetect.GetCollider());
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Move(delta);
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
