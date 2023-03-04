using Godot;
using System;
using Godot.Collections;

public partial class InterfaceManager : CanvasLayer
{
    public DialogueManager manager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        manager = GetNode("DialogueManager") as DialogueManager;
    }

    public void Open(Array<PackedScene> d)
    {
        manager.Open(d);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
