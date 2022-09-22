using Godot;
using System;

public class Attack : State
{
    Vector2 direction;

    Weapon w;

    Node2D spawnPoint;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "ATTACK";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
        spawnPoint = (Node2D)player.GetNode("WeapSpawn");
    }

    public override void Start()
    {
        w = (Weapon)player.equipped.Weapon.Instance();
        direction = GetInput().Sign();

        if (w.canThrowWeapon)
        {
            w.Position = spawnPoint.GlobalPosition;
            w.Rotation = spawnPoint.GlobalRotation;
            SceneManager.CurrentScene.AddChild(w);
            w.Attack(spawnPoint.GlobalPosition.Sign());
            Stop();
        }
    }

    public override void Stop()
    {
        stateMachine.ResetToOldState();
    }
}
