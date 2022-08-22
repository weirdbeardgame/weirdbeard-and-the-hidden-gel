using Godot;
using System;
using System.Collections.Generic;

public class Dialogue : Node
{
    [Export]
    public List<string> buffer;

    public int length;

    [Export]
    public Sprite speakerHead;

    public Dialogue Open(int i)
    {
        if (i <= buffer.Capacity)
        {
            length = buffer[i].Length;
            return this;
        }

        return null;
    }
}

