using Godot;
using System;

public enum Objects { NOTHING = 0, SPIKE = 2, LADDER = 1, WATER = 3 }

public partial class Player : Actor
{
    private int _Obj;
    private Area2D _body;
    private Camera2D _camera;
    private Timer _coyoteTimer;
    private TileMap _CurrentMap;
    private Timer _bufferedJumpTimer;

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

        Destroyed += GamaOvar;

        _CurrentMap = GetParent().GetNode<TileMap>("TileMap");

        StateMachine.InitState("IDLE");
        OnEquip += EquipPowerup;
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

    public void ClearCollidedObject() => _Obj = 0;

    public int Collided(Player Player, string DataLayerName)
    {
        var pos = _CurrentMap.LocalToMap(Player.GlobalPosition);
        var data = _CurrentMap.GetCellTileData(1, pos, true);
        if (data != null)
        {
            var num = data.GetCustomData(DataLayerName);

            _Obj = num.AsInt32();
            return _Obj;
        }
        return 0;
    }


    public void ResetPlayer()
    {
        ResetActor();
        SetState("IDLE");
        if (!_camera.Enabled)
        {
            ActivateCamera();
        }
        ClearCollidedObject();
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
    public void GamaOvar()
    {
        // Davy Jones stuff in here?
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
        switch ((Objects)Collided(this, "ObjectType"))
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

        ApplyGravity((float)delta);
        MoveAndSlide();
    }
}
