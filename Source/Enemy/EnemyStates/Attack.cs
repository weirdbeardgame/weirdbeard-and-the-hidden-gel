using Godot;
using System;

public partial class Attack : State
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "ATTACK";
        Enemy = GetParent<Enemy>();
        StateMachine = Enemy.GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        Player = Enemy.GetParent<LevelCommon>().GetNode<Player>("Player");
        base.Start();
    }

    public override Vector2 GetInput()
    {
        Vector2 InputVector = new Vector2();
        if (Enemy.Direction != Player.Direction)
        {
            Enemy.Direction = Player.Direction;
        }

        InputVector += Enemy.Direction * Enemy.Speed;

        return InputVector;
    }

    public override void Update(double delta)
    {
        if (!Enemy.PlayerDetected)
        {
            // Reset!
        }
    }

    public override void FixedUpdate(double delta)
    {
        Enemy.Velocity = GetInput();
    }

    public override void Stop()
    {
        base.Stop();
    }

}
