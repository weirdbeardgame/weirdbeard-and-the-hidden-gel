using Godot;
using System;

public partial class SubLevel : LevelCommon
{

    Level parentLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void EnterSubLevel(Player p, Level parent)
    {
        player = p;
        parentLevel = parent;
        player.ResetState();
    }


    public override void ExitSubLevel()
    {
        player = null;
        GetTree().Root.RemoveChild(this);
        GetTree().Root.AddChild(parentLevel);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
