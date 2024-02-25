using Godot;
using System;

public partial class Player : Actor
{
    private Area2D _Body;
    private Camera2D _Camera;
    private Timer _CoyoteTimer;
    private Timer _BufferedJumpTimer;

    public TileCommon Map;
    public int PlayerLives = 3;
    public Sprite2D WeirdBeard;
    public PowerUp CurrentPowerup;
    public WeaponCommon CurrentWeapon;
    public AnimationPlayer AnimationPlayer;
    public Vector2 Direction = Vector2.Right;

    public static Action<PowerUp> OnEquip;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        WeirdBeard = (Sprite2D)GetNode("CenterContainer/WeirdBeard");
        _StateMachine = (StateMachine)GetNode("StateMachine");
        AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        _BufferedJumpTimer = (Timer)GetNode("BufferedJump");

        _Body = GetNode<Area2D>("ObjectDetect");

        CurrentWeapon = new WeaponCommon();

        _CoyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.StartNewGame += NewGame;

        _Camera = (Camera2D)GetNode("Camera2D");

        _StateMachine.InitState("IDLE");

        OnEquip += EquipPowerup;

        Map = GetParent().GetNode<TileCommon>("TileMap");

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
        SceneManager.StartNewGame -= NewGame;
    }

    public void EquipPowerup(PowerUp power)
    {
        GD.Print("POWER UP");
        if (CurrentPowerup != power)
        {
            if (_StateMachine.Has(power.StateName))
            {
                GD.Print("Name: " + power.StateName);
                _StateMachine.RemoveState(CurrentPowerup);
                RemoveChild(CurrentPowerup);
            }
            AddChild(power);
            _StateMachine.AddState(power, power.StateName);
            CurrentPowerup = power;
        }
    }

    public void ResetState()
    {
        NumJumps = 2;
        wasOnFloor = false;
        Velocity = Vector2.Zero;
        Gravity = DefaultGravity;
        SetState("IDLE");
        if (!_Camera.Enabled)
        {
            ActivateCamera();
        }
        Map.ClearCollidedObject();
        CanMove = true;
    }

    public void SetState(string state)
    {
        if (_StateMachine != null)
        {
            _StateMachine.UpdateState(state);
        }
    }

    public void StartCoyoteTimer()
    {
        if (_CoyoteTimer.IsStopped())
        {
            _CoyoteTimer.Start(maxCoyoteTimer);
        }
    }

    public void BufferJump()
    {
        if (_BufferedJumpTimer.IsStopped())
        {
            _BufferedJumpTimer.Start();
        }
    }

    public void ActivateCamera()
    {
        _Camera.Enabled = true;
        _Camera.MakeCurrent();
    }

    public void DeactivateCamera()
    {
        _Camera.Enabled = false;
    }

    public void CenterCamera()
    {
        Vector2 CameraPosition = _Camera.GlobalPosition;
        CameraPosition.X = GlobalPosition.X;
        _Camera.Position = CameraPosition;
    }

    public void Die()
    {
        if (PlayerLives > 0)
        {
            PlayerLives -= 1;
            SceneManager.ResetLevel();
        }
    }

    public bool CanJump()
    {
        GD.Print("Num Jumps: ", NumJumps);
        return IsOnFloor() || !_CoyoteTimer.IsStopped() || _BufferedJumpTimer.IsStopped() || NumJumps != 0;
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
        switch ((Objects)Map.Collided(this, "ObjectType"))
        {
            case Objects.LADDER:
                if (Input.IsActionJustPressed("Up"))
                {
                    _StateMachine.UpdateState("LADDER");
                }
                break;

            case Objects.SPIKE:
                _StateMachine.UpdateState("DEATH");
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
            if (_StateMachine.CurrentStateName != "FALL")
            {
                _StateMachine.UpdateState("FALL");
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("Attack") && CurrentWeapon != null)
        {
            CurrentWeapon.Attack(Direction.Sign(), GetTree().CurrentScene);
        }

        if (!IsOnFloor())
        {
            var _Velocity = Velocity;
            _Velocity.Y += (float)delta * Gravity;
            Velocity = _Velocity;
        }
        MoveAndSlide();
    }
}
