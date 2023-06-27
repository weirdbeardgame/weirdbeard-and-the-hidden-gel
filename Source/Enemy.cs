using Godot;
using System;

public enum EnemyDirection { LEFT, RIGHT };

public partial class Enemy : Actor
{
    public AnimationPlayer Player;

    RayCast2D Left;
    RayCast2D Right;
    RayCast2D playerDetect;
    RayCast2D playerDetect2;

    Sprite2D sprite;

    Vector2 dir;

    EnemyDirection dirToWalk;

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

        gravity = 500;

        dir = new Vector2();
    }

    public void ChangeDirection()
    {
        sprite = (Sprite2D)GetNode("Enemy");
        switch (dirToWalk)
        {
            case EnemyDirection.LEFT:
                sprite.FlipH = false;
                dir.X = speed;
                dirToWalk = EnemyDirection.RIGHT;
                break;

            case EnemyDirection.RIGHT:
                sprite.FlipH = true;
                dir.X = -speed;
                dirToWalk = EnemyDirection.LEFT;
                break;
        }
    }

    public void Move(double delta)
    {
        if (!Right.IsColliding() || !Left.IsColliding() || IsOnWall())
        {
            ChangeDirection();
        }

        Velocity = ApplyGravity(delta, gravity);
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
        Move(delta);
        MoveAndSlide();
    }

    public void OnArea2DAreaEntered(object area)
    {
        if (area is Player)
        {
            Player dead = area as Player;
            dead.SetState("DEATH");
        }
    }
}
