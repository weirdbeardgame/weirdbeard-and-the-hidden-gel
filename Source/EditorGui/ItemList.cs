using Godot;
using System;

public partial class ItemList : Control
{

    private VBoxContainer _buttonContainer;

    [Export] private PackedScene _toAdd;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
