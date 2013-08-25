using System;
using UnityEngine;
using System.Collections;

public class WorldManager
{
    private static WorldManager instance = null;
    public static WorldManager Instance
    {
        get
        {
            if (WorldManager.instance == null)
            {
                WorldManager.instance = new WorldManager();
            }

            return instance;
        }
        set { instance = value; }
    }
    
    private LevelManager levelManagerInstance;
    public static LevelManager LevelManager
    {
        get { return WorldManager.Instance.levelManagerInstance; }
        set { WorldManager.Instance.setLevelManager(value); }
    }

    private GameManager gameManagerInstance;
    public static GameManager GameManager
    {
        get { return WorldManager.Instance.gameManagerInstance; }
        set { WorldManager.Instance.setGameManager(value); }
    }

    private ItemManager itemManagerInstance;
    public static ItemManager ItemManager
    {
        get { return WorldManager.Instance.itemManagerInstance; }
        set { WorldManager.Instance.setItemManager(value); }
    }

    private PlayerController playerControllerInstance;
    public static PlayerController PlayerController
    {
        get { return WorldManager.Instance.playerControllerInstance; }
        set { WorldManager.Instance.playerControllerInstance = value; }
    }

    //=================================================
    //+++++++++++++++++++++++++++++++++++++++++++++++++
    //=================================================

    private WorldManager()
    {
        
    }

    public void setLevelManager(LevelManager lvlmngrInstance)
    {
        if (levelManagerInstance == null)
        {
            levelManagerInstance = lvlmngrInstance;
        }
        else
        {
            throw new Exception("Multiple LevelManager startups");
        }
    }

    public void setGameManager(GameManager newGameManagerInstance)
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = newGameManagerInstance;
        }
        else
        {
            throw new Exception("Multiple GameManager startups");
        }
    }

    public void setItemManager(ItemManager newItemManagerInstance)
    {
        if (itemManagerInstance == null)
        {
            itemManagerInstance = newItemManagerInstance;
        }
        else
        {
            throw new Exception("Multiple ItemManager startups");
        }
    }
}
