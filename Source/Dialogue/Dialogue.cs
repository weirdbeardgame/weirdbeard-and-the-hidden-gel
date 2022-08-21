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

public class DialogueEventPublish
{
    DialogueBox box;

    public Action<Dialogue> speak;

    void OpenDialogueBox(Dialogue toSpeak)
    {
        box.Open(toSpeak);
    }

    public DialogueEventPublish()
    {
        speak = null;
    }

    public DialogueEventPublish(Dialogue message)
    {
        GD.Print("DIALOGUE EVENT");
        speak = OpenDialogueBox;
        OnPublish(message);
    }

    public virtual void OnPublish(Dialogue toSpeak)
    {
        if (speak != null)
        {
            speak(toSpeak);
            GD.Print("DIALOGUE EVENT PUBLISHED");
        }
    }

}
