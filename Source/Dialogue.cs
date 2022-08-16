using Godot;
using System;

public class Dialogue : Node
{
    [Export]
    public string buffer;

    public int length;

    [Export]
    public Sprite speakerHead;

    public Dialogue Open()
    {
        length = buffer.Length;
        return this;
    }


}
