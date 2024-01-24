using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[Tool]
public partial class LevelDockScript : Control
{
    SceneManager SceneManager;
    FileDialog Dialog;

    bool AddScenes;
    bool AddStartScene;

    Button AddSceneButton;
    Button NewSceneButton;
    Button StartSceneButton;
    Button SetPlayerButton;

    [Export]
    PackedScene SceneButton;

    Dictionary<string, LevelSelectorButton> SceneChanger;

    string CurrentScene;
    List<string> SceneNames;

    VBoxContainer Container;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Dialog = GetNode<FileDialog>("Panel/FileDialog");
        SceneManager = SceneManager.Manager;

        AddSceneButton = GetNode<Button>("Panel/AddScene");
        NewSceneButton = GetNode<Button>("Panel/NewScene");
        StartSceneButton = GetNode<Button>("Panel/StartScene");

        AddSceneButton.Pressed += AddScene_Button;
        NewSceneButton.Pressed += NewScene_Button;
        StartSceneButton.Pressed += SetStartScene_Button;

        Dialog.FileSelected += AddScene;
        Dialog.FileSelected += SetStartScene;

        Container = GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer");

        SceneNames = new List<string>();
        SceneChanger = new Dictionary<string, LevelSelectorButton>();

        UpdateList();
    }

    void AddScene_Button()
    {
        AddScenes = true;
        Dialog.Popup();
    }

    void AddScene(string path)
    {
        if (AddScenes)
        {
            PackedScene Scene = (PackedScene)ResourceLoader.Load(path);
            if (SceneManager.Add(Scene))
            {
                GD.Print("Scene Added");
                UpdateList();
            }
        }
    }

    void NewScene_Button()
    {
        SceneManager.New();
        // ToDo: Level editor right after lads
    }

    void SetStartScene_Button()
    {
        Dialog.Show();
        AddStartScene = true;
    }

    void SetStartScene(string Path)
    {
        if (AddStartScene)
        {
            LevelCommon lev = ResourceLoader.Load<PackedScene>(Path).Instantiate<LevelCommon>();
            SceneManager.SetStartScene(lev.LevelName);
            AddStartScene = false;
        }
    }

    void RemoveScene_Button()
    {
        SceneManager.Remove(CurrentScene);
    }

    void UpdateList()
    {
        if (SceneManager.SceneNames != null)
        {
            if (SceneNames != SceneManager.SceneNames && SceneManager.SceneNames.Count > 0)
            {
                SceneNames = SceneManager.SceneNames;

                foreach (var scene in SceneNames)
                {
                    LevelSelectorButton SelectorButton = SceneButton.Instantiate<LevelSelectorButton>();
                    SelectorButton.CreateButton(scene);
                    if (!SceneChanger.ContainsKey(SelectorButton.SceneName))
                    {
                        SceneChanger.Add(SelectorButton.SceneName, SelectorButton);
                        Container.AddChild(SelectorButton);
                    }
                }
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
