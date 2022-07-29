using Godot;
using System;

public class Weapon : Node
{
    [Export]
    protected Sprite WeaponSprite;

    [Export]
    protected int ID = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public virtual bool Equip()
    {
        return false;
    }

    public virtual void Attack()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
