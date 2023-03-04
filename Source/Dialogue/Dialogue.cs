using Godot;
using System;
using Godot.Collections;

public partial class Dialogue : Node
{
    [Export]
    public Array<string> buffer;

    public int length;

    //[Export]
    //public Sprite2D speakerHead;

    public Dialogue Open(int i)
    {
        if (i <= buffer.Count)
        {
            length = buffer[i].Length;
            return this;
        }

        return null;
    }
}

