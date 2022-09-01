using Godot;
using System;

// Basically the Abstract Factory Pattern
public class EnemySpawner : Node
{
    [Export]
    PackedScene spawn;

    [Export]
    float maxAmnt, amt;

    [Export]
    EnemyDirection enemyDirection = EnemyDirection.LEFT;

    RandomNumberGenerator randNum;

    Vector2 enePos, screenSize;

    Timer timer;

    Level activeScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enePos = new Vector2();
        randNum = new RandomNumberGenerator();
        timer = (Timer)GetNode("Timer");

        activeScene = (Level)Owner;
    }

    public void Randomize()
    {
        screenSize = GetViewport().GetVisibleRect().Size;
        enePos.x = randNum.RandfRange(0, screenSize.x);
        randNum.Randomize();
        enePos.y = randNum.RandfRange(0, screenSize.y);
        randNum.Randomize();
    }

    public void Spawn()
    {
        int i = 0;

        amt = randNum.RandfRange(0, maxAmnt);
        randNum.Randomize();

        while (i < amt && activeScene.activeEnemies.Count < activeScene.maxEnemyAmnt)
        {
            GD.Print("Spawn");
            Randomize();
            Enemy ene = (Enemy)spawn.Instance();

            ene.SetAsToplevel(true);
            ene.Position = enePos;
            ene.Spawn(enemyDirection);
            activeScene.AddChild(ene);
            activeScene.activeEnemies.Add(ene);
            i += 1;
        }
    }

    void TimeOut()
    {
        if (activeScene.activeEnemies.Capacity < activeScene.maxEnemyAmnt)
        {
            GD.Print("Call Spawn");
            Spawn();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
