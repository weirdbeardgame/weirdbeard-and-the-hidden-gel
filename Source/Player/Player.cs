using Godot;
using System;

public class Player : Actor
{
    Timer coyoteTimer;
    Timer bufferedJumpTimer;
    PowerUp currentPowerup;

    public Sprite weirdBeard;
    public int playerLives = 3;
    public WeaponSlot equipped;
    public AnimationPlayer player;
    public Vector2 direction = Vector2.Right;

    float defaultGravity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateMachine = (StateMachine)GetNode("StateMachine");
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        bufferedJumpTimer = (Timer)GetNode("BufferedJump");
        equipped = (WeaponSlot)GetNode("WeaponSlot");
        coyoteTimer = (Timer)GetNode("CoyoteTimer");
        weirdBeard = (Sprite)GetNode("WeirdBeard");
        SceneManager.startNewGame += NewGame;

        defaultGravity = gravity;

        if (projectileMotionJump)
        {
            jumpVelocity = ((2.0f * jumpHeight) / jumpPeak) * -1.0f;
            jumpGravity = ((-2.0f * jumpHeight) / (jumpPeak * jumpPeak)) * -1.0f;
            fallGravity = ((-2.0f * jumpHeight) / (jumpDescent * jumpDescent)) * -1.0f;
        }
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
        wasOnFloor = false;
        velocity = Vector2.Zero;
        gravity = defaultGravity;
        SetState("IDLE");
        canMove = true;
    }

    public void SetState(string state)
    {
        stateMachine.UpdateState(state);
    }

    public void StartCoyoteTimer()
    {
        if (coyoteTimer.IsStopped())
        {
            coyoteTimer.Start(maxCoyoteTimer);
        }
    }

    public void BufferJump()
    {
        if (bufferedJumpTimer.IsStopped())
        {
            bufferedJumpTimer.Start();
        }
    }

    public void Die()
    {
        playerLives -= 1;
    }

    public bool CanJump()
    {
        return ((IsOnFloor() || !coyoteTimer.IsStopped()) || bufferedJumpTimer.IsStopped());
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

    public override void _PhysicsProcess(float delta)
    {
        if (equipped.Weapon != null)
        {
            if (Input.IsActionJustPressed("Attack"))
            {
                stateMachine.UpdateState("ATTACK");
            }
        }
        wasOnFloor = IsOnFloor();
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, Vector2.Up);

        if (!IsOnFloor() && Input.IsActionJustPressed("Run"))
        {
            ActivatePowerup();
        }
    }
}
