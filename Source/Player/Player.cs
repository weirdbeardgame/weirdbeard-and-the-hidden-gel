using Godot;
using System;

public partial class Player : Actor
{
    Timer _coyoteTimer;
    Timer _bufferedJumpTimer;
    Area2D body;

    public TileCommon map;

    public Sprite2D weirdBeard;
    public int PlayerLives = 3;
    public WeaponCommon CurrentWeapon;
    public AnimationPlayer AnimationPlayer;
    public Vector2 direction = Vector2.Right;
    public PowerUp CurrentPowerup;

    Camera2D camera;

    public static Action<PowerUp> OnEquip;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weirdBeard = (Sprite2D)GetNode("CenterContainer/WeirdBeard");
        stateMachine = (StateMachine)GetNode("StateMachine");
        AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        _bufferedJumpTimer = (Timer)GetNode("BufferedJump");

        body = GetNode<Area2D>("ObjectDetect");

        CurrentWeapon = new WeaponCommon();

        _coyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.startNewGame += NewGame;

        camera = (Camera2D)GetNode("Camera2D");

        stateMachine.InitState("IDLE");

        OnEquip += EquipPowerup;

        map = GetParent().GetNode<TileCommon>("TileMap");

        Owner = GetParent();

        if (projectileMotionJump)
        {
            JumpVelocity = ((2.0f * jumpHeight) / jumpPeak) * -1.0f;
            JumpGravity = ((-2.0f * jumpHeight) / (jumpPeak * jumpPeak)) * -1.0f;
            FallGravity = ((-2.0f * jumpHeight) / (jumpDescent * jumpDescent)) * -1.0f;
        }
    }

    public void NewGame()
    {
        PlayerLives = 3;
        ResetState();
        SceneManager.startNewGame -= NewGame;
    }

    public void EquipPowerup(PowerUp power)
    {
        GD.Print("POWER UP");
        if (CurrentPowerup != power)
        {
            if (stateMachine.Has(power.StateName))
            {
                GD.Print("Name: " + power.StateName);
                stateMachine.RemoveState(CurrentPowerup);
                RemoveChild(CurrentPowerup);
            }
            AddChild(power);
            stateMachine.AddState(power, power.StateName);
            CurrentPowerup = power;
        }
    }

    public void ResetState()
    {
        NumJumps = 2;
        wasOnFloor = false;
        Velocity = Vector2.Zero;
        gravity = defaultGravity;
        SetState("IDLE");
        if (!camera.Enabled)
        {
            ActivateCamera();
        }
        map.ClearCollidedObject();
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
        if (_coyoteTimer.IsStopped())
        {
            _coyoteTimer.Start(maxCoyoteTimer);
        }
    }

    public void BufferJump()
    {
        if (_bufferedJumpTimer.IsStopped())
        {
            _bufferedJumpTimer.Start();
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

    public void CenterCamera()
    {
        Vector2 CameraPosition = camera.GlobalPosition;
        CameraPosition.X = GlobalPosition.X;
        camera.Position = CameraPosition;
    }

    public void Die()
    {
        if (PlayerLives > 0)
        {
            PlayerLives -= 1;
            SceneManager.resetLev();
        }
    }

    public bool CanJump()
    {
        GD.Print("Num Jumps: ", NumJumps);
        return ((IsOnFloor() || !_coyoteTimer.IsStopped()) || _bufferedJumpTimer.IsStopped() || NumJumps != 0);
    }

    // Game Grumps joke
    public bool GamaOvar()
    {
        return (PlayerLives <= 0);
    }

    public void EquipWeapon(PackedScene w, Texture2D weapSprite)
    {
        WeaponCommon weapon = w.Instantiate<WeaponCommon>();
        CurrentWeapon = weapon;

        if (weapSprite != null)
        {
            WeaponSlot.updateWSprite.Invoke(weapSprite);
        }

        if (CurrentWeapon == null)
        {
            GD.PrintErr("Weapon NULL");
        }
    }

    public void DetectObjects()
    {
        switch ((Objects)map.Collided(this, "ObjectType"))
        {
            case Objects.LADDER:
                if (Input.IsActionJustPressed("Up"))
                {
                    stateMachine.UpdateState("LADDER");
                }
                break;

            case Objects.SPIKE:
                stateMachine.UpdateState("DEATH");
                break;

            case Objects.WATER:
                // I think my day is going swimmingly!
                break;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        DetectObjects();

        //GD.Print("Velocity: ", Velocity);

        if (CurrentPowerup != null)
        {
            if (CurrentPowerup.CanBeActivated() && Input.IsActionJustPressed("Run"))
            {
                CurrentPowerup.Activate();
            }
        }
    }

    public void GetAirState()
    {
        if (Velocity.Y > 0.0f)
        {
            if (StateMachine.CurrentStateName != "FALL")
            {
                StateMachine.UpdateState("FALL");
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("Attack") && CurrentWeapon != null)
        {
            CurrentWeapon.Attack(direction.Sign(), GetTree().CurrentScene);
        }

        wasOnFloor = IsOnFloor();
        Velocity = ApplyGravity(delta, gravity);
        MoveAndSlide();
    }
}
