using Godot;
using System;

public class Audio : Node
{

    [Export]
    AudioStreamPlayer audio;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void Play()
    {
        if (!audio.Playing)
        {
            audio.Play();
        }
    }

    public void Stop()
    {

    }
}
