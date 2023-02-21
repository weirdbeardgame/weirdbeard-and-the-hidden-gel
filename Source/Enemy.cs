using Godot;
using System;

public enum EnemyDirection { LEFT, RIGHT };

public class Enemy : Actor
{
    public AnimationPlayer player;

    RayCast2D Right;
    RayCast2D Left;

    RayCast2D playerDetect;

    Sprite sprite;

    EnemyDirection dirToWalk;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        Right = (RayCast2D)GetNode("Right");
        Left = (RayCast2D)GetNode("Left");
        sprite = (Sprite)GetNode("Enemy");
        stateMachine = (StateMachine)GetNode("StateMachine");
        player.Play("Idle");
    }

    public void ChangeDirection()
    {
        sprite = (Sprite)GetNode("Enemy");
        switch (dirToWalk)
        {
            case EnemyDirection.LEFT:
                sprite.FlipH = false;
                velocity.x = speed;
                dirToWalk = EnemyDirection.RIGHT;
                break;

            case EnemyDirection.RIGHT:
                sprite.FlipH = true;
                velocity.x = -speed;
                dirToWalk = EnemyDirection.LEFT;
                break;
        }
    }

    public void Move(float delta)
    {
        if (!Right.IsColliding() || !Left.IsColliding() || IsOnWall())
        {
            ChangeDirection();
        }

        velocity.y += gravity * delta;
        velocity.y = MoveAndSlide(velocity, Vector2.Up).y;
    }

    public void Destroy()
    {
        velocity = Vector2.Zero;
        sprite = null;
    }

    public void FollowPlayer()
    {

    }

    public void Attack()
    {
        if (playerDetect.IsColliding())
        {
            // Play attacking Anim. Chase player ... YARRR!
            // Yer going to be needin ya a hitbox as well laddy.
            player.Play("Attack");
            FollowPlayer();
        }
    }

    public override void _PhysicsProcess(float delta)
    {

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
