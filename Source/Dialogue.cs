using Godot;
using System;

public class Dialogue : Node
{
    [Export]
    public string buffer;

    int length;

    [Export]
    Sprite speakerHead;

    public Dialogue Open()
    {
        length = buffer.Length;
        return this;
    }


}
