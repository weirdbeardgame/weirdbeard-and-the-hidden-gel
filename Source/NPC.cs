using Godot;
using System;
using System.Collections.Generic;

public class NPC : Node
{
    [Export]
    string npcName;

    AnimationPlayer player;

    [Export]
    List<PackedScene> dialogue;

    [Export] PackedScene box;

    DialogueBox boxInstance;

    bool isPlayerCollide;

    bool isOpen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        player.Play("IDLE");
        isPlayerCollide = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (isPlayerCollide && Input.IsActionPressed("Submit"))
        {
            if (!isOpen)
            {
                GD.Print("Dialogue Colision");
                if (box != null)
                {
                    boxInstance = (DialogueBox)box.Instance();
                }
                SceneManager.CurrentScene.AddChild(boxInstance);
                boxInstance.Open((Dialogue)dialogue[0].Instance());
                isOpen = true;
            }
        }
    }

    public void onBodyEntered(object body)
    {
        if (body is Player)
        {
            isPlayerCollide = true;
        }
    }

    public void onBodyExited(object body)
    {
        if (body is Player)
        {
            isPlayerCollide = false;
            isOpen = false;
        }
    }
}
