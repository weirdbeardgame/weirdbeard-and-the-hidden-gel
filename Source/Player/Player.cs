using Godot;
using System;

public partial class Player : Actor
{
    private Area2D _body;
    private Camera2D _camera;
    private Timer _coyoteTimer;
    private Timer _bufferedJumpTimer;

    public TileCommon Map;
    public int PlayerLives = 3;
    public Sprite2D WeirdBeard;
    public PowerUp CurrentPowerup;
    public WeaponCommon CurrentWeapon;
    public AnimationPlayer AnimationPlayer;

    public static Action<PowerUp> OnEquip;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        WeirdBeard = (Sprite2D)GetNode("CenterContainer/WeirdBeard");
        AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        _bufferedJumpTimer = (Timer)GetNode("BufferedJump");

        _body = GetNode<Area2D>("ObjectDetect");
        CurrentWeapon = new WeaponCommon();
        _coyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.StartNewGame += NewGame;
        _camera = (Camera2D)GetNode("Camera2D");

        GetStateMachine();

        StateMachine.InitState("IDLE");
        OnEquip += EquipPowerup;
        Map = GetParent().GetNode<TileCommon>("TileMap");
        Owner = GetParent();

        if (projectileMotionJump)
        {
            JumpVelocity = ((2.0f * jumpHeight) / JumpTimeToPeak) * -1.0f;
            JumpGravity = ((-2.0f * jumpHeight) / (JumpTimeToPeak * JumpTimeToPeak)) * -1.0f;
            FallGravity = ((-2.0f * jumpHeight) / (JumpTimeToDescent * JumpTimeToDescent)) * -1.0f;
        }

        ResetPlayer();
    }

    public void NewGame()
    {
        PlayerLives = 3;
        ResetPlayer();
        SceneManager.StartNewGame -= NewGame;
    }

    public void EquipPowerup(PowerUp power)
    {
        GD.Print("POWER UP");
        if (CurrentPowerup != power)
        {
            if (StateMachine.Has(power.StateName))
            {
                GD.Print("Name: " + power.StateName);
                StateMachine.RemoveState(CurrentPowerup);
                RemoveChild(CurrentPowerup);
            }
            AddChild(power);
            StateMachine.AddState(power, power.StateName);
            CurrentPowerup = power;
        }
    }

    public void ResetPlayer()
    {
        ResetActor();
        SetState("IDLE");
        if (!_camera.Enabled)
        {
            ActivateCamera();
        }
        Map.ClearCollidedObject();
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
        _camera.Enabled = true;
        _camera.MakeCurrent();
    }

    public void DeactivateCamera()
    {
        _camera.Enabled = false;
    }

    public void CenterCamera()
    {
        Vector2 CameraPosition = _camera.GlobalPosition;
        CameraPosition.X = GlobalPosition.X;
        _camera.Position = CameraPosition;
    }

    public bool CanJump()
    {
        GD.Print("Num Jumps: ", NumJumps);
        return IsOnFloor() || !_coyoteTimer.IsStopped() || _bufferedJumpTimer.IsStopped() || NumJumps != 0;
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
                    StateMachine.UpdateState("LADDER");
                }
                break;

            case Objects.SPIKE:
                StateMachine.UpdateState("DEATH");
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
            if (StateMachine.CurrentStateName != "FALL")
            {
                StateMachine.UpdateState("FALL");
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Gravity = GetGravity();
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
