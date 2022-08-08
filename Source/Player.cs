using Godot;
using System;


public class Player : Actor
{
    public AnimationPlayer player;

    [Export]
    public float maxCoyoteTimer = 2f;

    public bool coyoteTime;

    Timer timer;

    public Sprite weirdBeard;

    public bool wasOnFloor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        stateMachine = (StateMachine)GetNode("StateMachine");
        stateMachine.UpdateState("IDLE");
        weirdBeard = (Sprite)GetNode("WeirdBeard");
        timer = (Timer)GetNode("Timer");
        PlayerData.equipped = (WeaponSlot)Owner.GetNode("HUD/WeaponSlot");
    }

    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

    public void SetState(string state)
    {
        stateMachine.UpdateState(state);
    }

    public void StartCoyoteTimer()
    {
        timer.Start(maxCoyoteTimer);
        coyoteTime = true;
    }

    public void StopCoyoteTimer()
    {
        if (!timer.IsStopped())
        {
            timer.Stop();
        }
        coyoteTime = false;
    }

    public bool CanJump()
    {
        return IsOnFloor() || coyoteTime;
    }

    public bool GamaOvar()
    {
        return (PlayerData.playerLives <= 0);
    }

    void Attack(float delta)
    {
        Weapon w = (Weapon)PlayerData.equipped.Weapon.Instance();

        float direction = 0f;

        if (velocity.x < 0)
        {
            direction = -1.0f;
        }
        else if (velocity.x > 0)
        {
            direction = 1.0f;
        }

        if (w.canThrowWeapon)
        {
            GetParent().AddChild(w);
            w.Position = GlobalPosition;
            w.Rotation = GlobalRotation;
            w.Attack(direction, delta);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (PlayerData.equipped.Weapon != null)
        {
            if (Input.IsActionJustPressed("Attack"))
            {
                Attack(delta);
            }
        }
        wasOnFloor = IsOnFloor();
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, Vector2.Up);

        if (wasOnFloor && !IsOnFloor())
        {
            StartCoyoteTimer();
        }
        else if (IsOnFloor() || timer.IsStopped())
        {
            StopCoyoteTimer();
        }
    }
}
