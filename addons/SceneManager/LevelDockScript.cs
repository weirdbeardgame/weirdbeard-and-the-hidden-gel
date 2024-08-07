using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class LevelDockScript : Control
{
    private SceneManager _sceneManager;
    private FileDialog _dialog;

    private Button _addSceneButton;
    private Button _newSceneButton;

    private FileSelector _playerRef;
    private FileSelector _newGameScene;

    [Export]
    private PackedScene _sceneButton;

    private List<LevelSelectorButton> _sceneChanger;

    private string _currentScene;
    private List<string> _sceneNames;

    private VBoxContainer _container;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _dialog = GetNode<FileDialog>("Panel/FileDialog");
        _sceneManager = SceneManager.Manager;

        _playerRef = GetNode<FileSelector>("_playerRef");
        _playerRef.BrowseButton.Pressed += SelectPlayerRefrence_Button;
        _playerRef.FileSelected += SetPlayerRefrence;
        if (_sceneManager.PlayerRef != null)
        {
            _playerRef.SetPathField(_sceneManager.PlayerRef.ResourcePath);
        }

        _newGameScene = GetNode<FileSelector>("_newGameScene");
        _newGameScene.BrowseButton.Pressed += SetNewGameScene_Button;
        _newGameScene.FileSelected += SetNewGameScene;
        if (_sceneManager.NewGameScene != null)
        {
            _newGameScene.SetPathField(_sceneManager.NewGameScene.ResourcePath);
        }

        _addSceneButton = GetNode<Button>("Panel/AddScene");
        _newSceneButton = GetNode<Button>("Panel/NewScene");

        _addSceneButton.Pressed += AddScene_Button;
        _newSceneButton.Pressed += NewScene_Button;

        _dialog.FileSelected += AddScene;

        _container = GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer");

        _sceneChanger = new List<LevelSelectorButton>();
        _sceneNames = new List<string>();

        UpdateList();
    }

    void AddScene_Button()
    {
        _dialog.Popup();
    }

    void AddScene(string path)
    {
        GD.Print("Path: " + path);
        PackedScene Scene = (PackedScene)ResourceLoader.Load(path);
        if (_sceneManager.Add(Scene))
        {
            GD.Print("Scene Added");
            UpdateList();
        }
    }

    void SetNewGameScene_Button() => _newGameScene.Open("Assets/resources/Levels/Scenes", new string[] { "*.tscn" });
    void SetNewGameScene() => _sceneManager.SetNewGameScene(_newGameScene.Path);

    void SelectPlayerRefrence_Button() => _playerRef.Open();
    void SetPlayerRefrence() => _sceneManager.SetPlayerRef(_playerRef.Path);

    void NewScene_Button()
    {
        _sceneManager.New();
        // ToDo: Level editor right after lads
    }

    void ChangeScene()
    {

    }

    void RemoveScene_Button()
    {
        _sceneManager.Remove(_currentScene);
    }

    void UpdateList()
    {
        if (_sceneManager.SceneNames != null)
        {
            if (_sceneNames != _sceneManager.SceneNames && _sceneManager.SceneNames.Count > 0)
            {
                _sceneNames = _sceneManager.SceneNames;
                foreach (var scene in _sceneNames)
                {
                    LevelSelectorButton SelectorButton = _sceneButton.Instantiate<LevelSelectorButton>();
                    SelectorButton.CreateButton(scene);
                    _sceneChanger.Add(SelectorButton);
                    _container.AddChild(SelectorButton);
                }
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
