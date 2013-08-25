using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        HelpMenu,
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
                GUI.skin.GetStyle("Label").fontSize = 50;
                GUILayout.BeginArea(new Rect(Screen.width/2 - 200, Screen.height/2 - 150, 400, 250));
                GUILayout.Label("Jump!");
                if (GUILayout.Button("Start Game"))
                {
                    switchGameState(GameState.Game);
                }
                if (GUILayout.Button("Help"))
                {
                    switchGameState(GameState.HelpMenu);
                }
                GUILayout.EndArea();
                break;
            case GameState.HelpMenu:
                GUI.skin.GetStyle("Label").alignment = TextAnchor.MiddleLeft;
                GUI.skin.GetStyle("Label").fontSize = 30;
                GUILayout.BeginArea(new Rect(20, 20, Screen.width - 40, Screen.height - 40));
                GUILayout.Label("How to Play: Simply jump! \nUse A and D to move and Space to jump. \nThe current level will decay if you don't reach the blue platform within 10 seconds.");
                GUILayout.Space(30);
                GUILayout.Label("Blue Crystals: \nBlue Crystals will grant you a x2 speed boost for 3 seconds.");
                GUILayout.Space(30);
                GUILayout.Label("Coins: \nCoins are used to calculate your total score.");
                GUILayout.Space(30);
                GUILayout.Label("Score Calculation: \ntotalScore = totalSeconds + 2*collectedCoins + level*level");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Back"))
                {
                    switchGameState(GameState.MainMenu);
                }
                GUILayout.EndArea();
                break;
            case GameState.Game:
                GUI.skin.GetStyle("Label").alignment = TextAnchor.MiddleLeft;
                GUI.skin.GetStyle("Label").fontSize = 50;
                GUI.Label(new Rect(20, Screen.height - 240, 600, 60),
                          "Current Level: " + WorldManager.LevelManager.currentLevel);
                GUI.Label(new Rect(20, Screen.height - 160, 600, 60),
                          "Collected Coins: " + WorldManager.PlayerController.money);

                GUI.skin.GetStyle("Label").alignment = TextAnchor.UpperCenter;
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, 60));
                GUILayout.Label(String.Format("{0:0.00}", 10 - WorldManager.LevelManager.decayTimer));
                GUILayout.EndArea();
                break;
            case GameState.DeathScreen:
                GUI.skin.GetStyle("Label").alignment = TextAnchor.UpperCenter;
                GUI.skin.GetStyle("Label").fontSize = 50;
                GUILayout.BeginArea(new Rect(Screen.width/2-500, Screen.height/2-250, 1000, 450));
                GUILayout.Label("Awwwwww! You died :(");
                GUILayout.Label("You managed to survive " + String.Format("{0:0.00}", globalTimer) + " seconds, though!");
                GUILayout.Label("Collected Coins: " + WorldManager.PlayerController.money);
                GUILayout.Label("Total Score: " + ((WorldManager.PlayerController.money * 2) + Mathf.Round(globalTimer) + Mathf.Pow(WorldManager.LevelManager.currentLevel, 2)));
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Try again"))
                {
                    switchGameState(GameState.Game);
                }
                if (GUILayout.Button("Quit"))
                {
                    Application.Quit();
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