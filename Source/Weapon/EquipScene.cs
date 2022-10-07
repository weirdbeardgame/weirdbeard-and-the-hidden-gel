using Godot;
using System;

public class EquipScene : Node
{
    [Export]
    PackedScene w;

    Sprite wSprite;

    Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        wSprite = (Sprite)GetNode("sprite");
    }

    public void Equip(object body)
    {
        if (body is Player)
        {
            player = (Player)body;
            player.equipped.AttachToPlayer(w, wSprite);
            QueueFree();
        }
    }
}