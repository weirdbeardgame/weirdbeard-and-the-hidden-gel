using Godot;
using System;

public class Player : Actor
{
    Timer coyoteTimer;
    Timer bufferedJumpTimer;
    PowerUp currentPowerup;

    public Sprite weirdBeard;
    public int playerLives = 3;
    public Weapon equipped;
    public AnimationPlayer player;
    public Vector2 direction = Vector2.Right;

    Camera2D camera;

    float defaultGravity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weirdBeard = (Sprite)GetNode("CenterContainer/WeirdBeard");
        stateMachine = (StateMachine)GetNode("StateMachine");
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        bufferedJumpTimer = (Timer)GetNode("BufferedJump");
        equipped = (Weapon)GetNode("WeaponSlot");
        if (equipped == null)
        {
            GD.PrintErr("Weapon NULL");
        }
        coyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.startNewGame += NewGame;
        defaultGravity = gravity;

        camera = (Camera2D)GetNode("Camera2D");

        stateMachine.InitState("IDLE");

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
        if (stateMachine != null)
        {
            stateMachine.UpdateState(state);
        }
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

    public void ActivateCamera()
    {
        camera.Current = true;
    }

    public void DeactivateCamera()
    {
        camera.Current = false;
    }

    public void Die()
    {
        playerLives -= 1;
    }

    public bool CanJump()
    {
        return ((IsOnFloor() || !coyoteTimer.IsStopped()) || bufferedJumpTimer.IsStopped() || canJumpAgain);
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

    void ApplyGravity(float delta, float currentGrav = 0f)
    {
        if (!projectileMotionJump)
        {
            velocity.y += gravity * delta;
        }
        else
        {
            velocity.y += currentGrav * delta;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("Attack"))
        {
            equipped.Attack();
        }

        wasOnFloor = IsOnFloor();
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, Vector2.Up);

        if (!IsOnFloor() && Input.IsActionJustPressed("Run") && currentPowerup != null)
        {
            currentPowerup.Activate();
        }
    }
}
