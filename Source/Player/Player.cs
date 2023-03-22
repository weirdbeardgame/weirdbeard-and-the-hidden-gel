using Godot;
using System;

public partial class Player : Actor
{
    Timer coyoteTimer;
    Timer bufferedJumpTimer;
    PowerUp currentPowerup;

    Area2D body;

    public Sprite2D weirdBeard;
    public int playerLives = 3;
    public WeaponCommon currentWeapon;
    public AnimationPlayer player;
    public Vector2 direction = Vector2.Right;

    Camera2D camera;

    float defaultGravity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weirdBeard = (Sprite2D)GetNode("CenterContainer/WeirdBeard");
        stateMachine = (StateMachine)GetNode("StateMachine");
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        bufferedJumpTimer = (Timer)GetNode("BufferedJump");

        body = GetNode<Area2D>("ObjectDetect");

        currentWeapon = new WeaponCommon();

        coyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.startNewGame += NewGame;
        defaultGravity = gravity;

        camera = (Camera2D)GetNode("Camera2D");

        stateMachine.InitState("IDLE");

        body.BodyEntered += DetectObject;

        if (projectileMotionJump)
        {
            jumpVelocity = ((2.0f * jumpHeight) / jumpPeak) * -1.0f;
            jumpGravity = ((-2.0f * jumpHeight) / (jumpPeak * jumpPeak)) * -1.0f;
            fallGravity = ((-2.0f * jumpHeight) / (jumpDescent * jumpDescent)) * -1.0f;
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
        Velocity = Vector2.Zero;
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
        camera.Enabled = true;
        camera.MakeCurrent();
    }

    public void DeactivateCamera()
    {
        camera.Enabled = false;
    }

    public void MoveCamera()
    {
        camera.Position = Position;
    }

    public void Die()
    {
        if (playerLives > 0)
        {
            playerLives -= 1;
            SceneManager.resetLev();
        }
    }

    public bool CanJump()
    {
        return ((IsOnFloor() || !coyoteTimer.IsStopped()) || bufferedJumpTimer.IsStopped() || canJumpAgain);
    }

    // Game Grumps joke
    public bool GamaOvar()
    {
        return (playerLives <= 0);
    }

    public void EquipWeapon(PackedScene w, Sprite2D weapSprite)
    {
        WeaponCommon weapon = w.Instantiate<WeaponCommon>();
        currentWeapon = weapon;

        if (weapon.weaponType == WeaponType.SHOOT)
        {
            AddChild(weapon);

            weapon.GlobalPosition = GlobalPosition + new Vector2(20, 0);
            weapon.GlobalRotation = GlobalRotation;
        }
        if (weapSprite != null)
        {
            WeaponSlot.updateWSprite.Invoke(weapSprite.Texture);
        }

        if (currentWeapon == null)
        {
            GD.PrintErr("Weapon NULL");
        }
    }

    public void EquipPowerup(PowerUp power)
    {
        if (currentPowerup != power)
        {
            RemoveChild(currentPowerup);
            AddChild(power);
            currentPowerup = power;
        }
        else
        {
            return;
        }
    }

    public void DetectObject(object body)
    {
        if (body is TileData)
        {
            GD.Print("Detecting");
            TileData d = (TileData)body;
            if (((int)d.GetCustomData("objects")) == 1)
            {
                GD.Print("Ladder");
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("Attack") && currentWeapon != null)
        {
            currentWeapon.Attack(direction.Sign());
        }

        MoveCamera();

        wasOnFloor = IsOnFloor();
        Velocity = ApplyGravity(delta, gravity);
        MoveAndSlide();

        if (currentPowerup != null)
        {
            if (currentPowerup.CanBeActivated() && Input.IsActionJustPressed("Run"))
            {
                currentPowerup.Activate();
            }
        }
    }
}
