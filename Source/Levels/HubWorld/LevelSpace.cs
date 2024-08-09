using Godot;
using System;

public partial class LevelSpace : Control
{
    [Export]
    string LevelName;

    Level Scene;
    SceneManager Scenes;

    Label TextLabel;

    private OptionButton _optionButton;

    public override void _Ready()
    {
        TextLabel = GetNode<Label>("LevelName");
        //_optionButton = GetNode<OptionButton>("OptionButton");
        Scenes = SceneManager.Manager;
        if ((Scene = (Level)Scenes.GetLevel(LevelName)) != null)
        {
            TextLabel.Text = Scene.LevelName;
        }
    }

    public void ActivateLevel(Player p)
    {
        GD.Print("SUBMIT!");
        SceneManager.ChangeScene(LevelName, p);
    }
}
