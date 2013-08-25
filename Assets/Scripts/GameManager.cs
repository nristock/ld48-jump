using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Game,
        DeathScreen
    }

    public GUISkin guiSkin;

    private GameState gameState = GameState.MainMenu;
    
    private float globalTimer = 0;

    private void Start()
    {
        WorldManager.GameManager = this;
    }

    private void OnGUI()
    {
        GUI.skin = guiSkin;
        
        switch (gameState)
        {
            case GameState.MainMenu:
                GUI.skin.GetStyle("Label").alignment = TextAnchor.UpperCenter;
                GUILayout.BeginArea(new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 150));
                GUILayout.Label("<name here>");
                if (GUILayout.Button("Start Game"))
                {
                    switchGameState(GameState.Game);
                }
                GUILayout.EndArea();
                break;
            case GameState.Game:
                GUI.skin.GetStyle("Label").alignment = TextAnchor.MiddleLeft;
                GUI.Label(new Rect(20, Screen.height - 240, 600, 60),
                          "Current Level: " + WorldManager.LevelManager.currentLevel);
                GUI.Label(new Rect(20, Screen.height - 160, 600, 60),
                          "Collected Coins: " + WorldManager.PlayerController.money);
                GUI.Label(new Rect(20, Screen.height - 80, 600, 60),
                          "Seconds Left: " + Mathf.Round(10 - WorldManager.LevelManager.decayTimer).ToString());
                break;
            case GameState.DeathScreen:
                GUI.skin.GetStyle("Label").alignment = TextAnchor.UpperCenter;
                GUILayout.BeginArea(new Rect(Screen.width/2-500, Screen.height/2-250, 1000, 450));
                GUILayout.Label("Awwwwww! You died :(");
                GUILayout.Label("You managed to survive " + Mathf.Round(globalTimer) + " seconds, though!");
                GUILayout.Label("Collected Coins: " + WorldManager.PlayerController.money);
                GUILayout.Label("Total Score: " + ((WorldManager.PlayerController.money * 2) + Mathf.Round(globalTimer) + WorldManager.LevelManager.currentLevel * 3));
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Try again"))
                {
                    switchGameState(GameState.Game);
                }
                GUILayout.EndArea();
                break;
        }
    }

    private void Update()
    {
        if (gameState == GameState.Game)
        {
            globalTimer += Time.deltaTime;
        }
    }

    public void switchGameState(GameState newGameState)
    {
        gameState = newGameState;

        if (newGameState == GameState.Game)
        {
            globalTimer = 0;
            WorldManager.LevelManager.initLevel();
        }
    }
}