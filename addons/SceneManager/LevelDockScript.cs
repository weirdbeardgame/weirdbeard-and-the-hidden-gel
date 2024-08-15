using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class LevelDockScript : Control
{
    private Button _save;
    private Button _refresh;
    private int _currentIndex;
    private FileDialog _dialog;
    private FileSelector _playerRef;
    private ItemList _levelSelector;
    private PackedScene _sceneButton;
    private FileSelector _newGameScene;
    private SceneManager _sceneManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _dialog = GetNode<FileDialog>("Panel/FileDialog");
        _sceneManager = SceneManager.Manager;

        _save = GetNode<Button>("Panel/Save");
        _refresh = GetNode<Button>("Panel/Refresh");

        _save.Pressed += Save_Button;
        _refresh.Pressed += Refresh_Button;

        _playerRef = GetNode<FileSelector>("Panel/PlayerRef");
        _playerRef.BrowseButton.Pressed += SelectPlayerRefrence_Button;
        _playerRef.FileSelected += SetPlayerRefrence;
        if (_sceneManager.PlayerRef != null)
        {
            _playerRef.SetPathField(_sceneManager.PlayerRef.ResourcePath);
        }

        _sceneButton = ResourceLoader.Load<PackedScene>("addons/SceneManager/LevelSelectorButton.tscn");

        _newGameScene = GetNode<FileSelector>("Panel/NewGameScene");
        _newGameScene.BrowseButton.Pressed += SetNewGameScene_Button;
        _newGameScene.FileSelected += SetNewGameScene;
        if (_sceneManager.NewGameScene != null)
        {
            _newGameScene.SetPathField(_sceneManager.NewGameScene.ResourcePath);
        }

        //_newSceneButton = GetNode<Button>("Panel/NewScene");
        // _newSceneButton.Pressed += NewScene_Button;

        _levelSelector = GetNode<ItemList>("Panel/ItemList");
        _levelSelector.Add.Pressed += AddScene_Button;
        _levelSelector.Remove.Pressed += RemoveScene_Button;

        _dialog.FileSelected += AddScene;
        ItemList.s_IndexUpdate += UpdateIndex;
        _sceneManager.ManagerRefresh += UpdateList;

        UpdateList();
    }

    void AddScene_Button()
    {
        _dialog.Popup();
    }

    void RemoveScene_Button()
    {
        _sceneManager.Remove(_levelSelector.Items[_currentIndex].Text);
        _levelSelector.RemoveItem(_levelSelector.Items[_currentIndex]);
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

    void Save_Button() => _sceneManager.Save();
    void Refresh_Button() => _sceneManager.Refresh();
    void SelectPlayerRefrence_Button() => _playerRef.Open();
    void SetPlayerRefrence() => _sceneManager.SetPlayerRef(_playerRef.Path);
    void SetNewGameScene() => _sceneManager.SetNewGameScene(_newGameScene.Path);
    void SetNewGameScene_Button() => _newGameScene.Open("Assets/resources/Levels/Scenes", new string[] { "*.tscn" });

    void UpdateIndex(int i)
    {
        _currentIndex = i;
        _sceneManager.ChangeSceneInEditor(_sceneManager.SceneNames[_currentIndex]);
    }

    void UpdateList()
    {
        if (_sceneManager.SceneNames != null)
        {
            if (_sceneManager.SceneNames.Count > 0)
            {
                foreach (var scene in _sceneManager.SceneNames)
                {
                    Button testBtn = _levelSelector.Contains(scene);
                    if (testBtn == null)
                    {
                        Button selectorButton = _sceneButton.Instantiate<Button>();
                        selectorButton.Text = scene;
                        _levelSelector.AddItem(selectorButton);
                    }
                }
            }

            if (_levelSelector.ItemCount > _sceneManager.SceneNames.Count)
            {
                CallDeferred(nameof(RemoveDeferred));
            }
        }
    }

    void RemoveDeferred()
    {
        // Need to remove da buttn
        var removeList = _levelSelector.Items.Where(item => !_sceneManager.SceneNames.Any(item2 => item2 == item.Text));

        foreach (var item in removeList)
        {
            _levelSelector.RemoveItem(item);
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
