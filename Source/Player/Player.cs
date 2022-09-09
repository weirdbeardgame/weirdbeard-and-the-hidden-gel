using Godot;
using System;


public class Player : Actor
{
    public AnimationPlayer player;

    Camera2D camera;

    Timer timer;

    public Sprite weirdBeard;

    PowerUp currentPowerup;

    public int playerLives = 3;

    public WeaponSlot equipped;

    public Vector2 direction = Vector2.Right;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        stateMachine = (StateMachine)GetNode("StateMachine");
        weirdBeard = (Sprite)GetNode("WeirdBeard");
        timer = (Timer)GetNode("Timer");

        SceneManager.startNewGame += NewGame;

        equipped = (WeaponSlot)GetNode("WeaponSlot");
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

    public void NewGame()
    {
        playerLives = 3;
        ResetState();
        SceneManager.startNewGame -= NewGame;
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
        playerLives -= 1;
    }

    public bool CanJump()
    {
        return IsOnFloor() || coyoteTime;
    }

    public bool GamaOvar()
    {
        return (playerLives <= 0);
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
        Weapon w = (Weapon)equipped.Weapon.Instance();

        if (w.canThrowWeapon)
        {
            GetParent().AddChild(w);
            w.Position = GlobalPosition;
            w.Rotation = GlobalRotation;
            w.Attack(delta, GlobalPosition);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (equipped.Weapon != null)
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
