using Godot;
using System;


public class Player : Actor
{
    public AnimationPlayer player;

    [Export]
    public float maxCoyoteTimer = 2f;

    public bool coyoteTime;

    Camera2D camera;

    Timer timer;

    public Sprite weirdBeard;

    public bool wasOnFloor;

    PowerUp currentPowerup;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        stateMachine = (StateMachine)GetNode("StateMachine");
        weirdBeard = (Sprite)GetNode("WeirdBeard");
        timer = (Timer)GetNode("Timer");

        PlayerData.equipped = (WeaponSlot)GetNode("WeaponSlot");

        ResetState();
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

    public string CurrentState
    {
        get
        {
            return stateMachine.CurrentState.stateName;
        }
    }

    public void NewGame()
    {
        PlayerData.playerLives = 3;
        ResetState();
    }

    public void ResetState()
    {
        canJumpAgain = true;
        velocity = Vector2.Zero;
        gravity = 4000f;
        SetState("IDLE");
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

    public void Die()
    {
        PlayerData.playerLives -= 1;
    }

    public bool CanJump()
    {
        return IsOnFloor() || coyoteTime;
    }

    public bool GamaOvar()
    {
        return (PlayerData.playerLives <= 0);
    }

    public void EquipPowerup(PowerUp power)
    {
        if (currentPowerup != power)
        {
            currentPowerup = power;
        }
        else
        {
            return;
        }
    }

    void ActivatePowerup()
    {
        if (currentPowerup != null)
        {
            stateMachine.UpdateState(currentPowerup.stateName);
        }
    }

    void Attack(float delta)
    {
        Weapon w = (Weapon)PlayerData.equipped.Weapon.Instance();

        if (w.canThrowWeapon)
        {
            GetParent().AddChild(w);
            w.Position = GlobalPosition;
            w.Rotation = GlobalRotation;
            w.Attack(delta);
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

        if (!IsOnFloor() && Input.IsActionJustPressed("Run"))
        {
            ActivatePowerup();
        }

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
