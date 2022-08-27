using Godot;
using System;

public class Checkpoint : Node
{

    bool isActive;


    public override void _Ready()
    {

    }

    public void Activate(object body)
    {
        if (body is Player)
        {
            if (!isActive)
            {
                isActive = true;

                // Send event or signal to deactivate other checkpoint in level
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
