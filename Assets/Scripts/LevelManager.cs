using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject paddlePrefab;
    public GameObject playerPrefab;

    public Material normalPaddleMaterial;
    public Material targetPaddleMaterial;

    public float minPaddleDistance = 5;
    public float maxPaddleDistance = 11;

    public int maxPaddlesPerLevel = 15;

    private GameObject playerInstance;
    private GameObject currentEndPaddle;
    private GameObject currentExtensionHandler;
    private readonly List<GameObject> activePaddles = new List<GameObject>();
    private readonly List<GameObject> items = new List<GameObject>(); 

    public float decayTimer { get; private set; }

    public int currentLevel { get; private set; }


    private bool gameRunning = false;

    private void Start()
    {
        WorldManager.LevelManager = this;
    }

    private void spawnPlayer()
    {
        playerInstance = (GameObject) Instantiate(playerPrefab);
        playerInstance.transform.position = currentEndPaddle.transform.position + new Vector3(0, 2, 0);
        setupCamera();
    }

    private void setupCamera()
    {
        Camera.main.GetComponent<PlayerTracker>().Player = playerInstance;
    }

    private GameObject spawnPaddle(Vector3 location)
    {
        var paddleObject = (GameObject) Instantiate(paddlePrefab);
        paddleObject.transform.position = location;

        // Item spawning
        if (Random.Range(0, 100)%10 == 1)
        {
            items.Add(WorldManager.ItemManager.makeRandomItem(paddleObject.transform.position + Vector3.up*2f));
        }
        else
        {
            items.Add(WorldManager.ItemManager.makeMoney(paddleObject.transform.position + Vector3.up*1.5f));
        }

        return paddleObject;
    }

    public void initLevel()
    {
        currentLevel = 0;

        resetTimer();

        while (activePaddles.Count > 0)
        {
            Destroy(activePaddles[0]);
            activePaddles.RemoveAt(0);
        }

        // Spawn initial start platform
        currentEndPaddle = spawnPaddle(Vector3.zero);
        activePaddles.Add(currentEndPaddle);

        // Spawn dat player
        spawnPlayer();

        // Extend dat level
        extendLevel();

        gameRunning = true;
    }

    private void resetTimer()
    {
        decayTimer = 0;
    }

    private void killOldLevel()
    {
        resetTimer();

        while (activePaddles.Count > 1)
        {
            Destroy(activePaddles[0]);
            activePaddles.RemoveAt(0);
        }

        while (items.Count > 1)
        {
            Destroy(items[0]);
            items.RemoveAt(0);
        }
    }

    public void extendLevel()
    {
        killOldLevel();
        currentLevel++;

        currentEndPaddle.renderer.material = normalPaddleMaterial;

        if (currentExtensionHandler != null)
        {
            Destroy(currentExtensionHandler);
        }

        var currentPaddlePosition = currentEndPaddle.transform.position;
        for (int i = 0; i < Mathf.Min(3 + currentLevel, maxPaddlesPerLevel); i++)
        {
            currentPaddlePosition = nextPaddlePosition(currentPaddlePosition);
            currentEndPaddle = spawnPaddle(currentPaddlePosition);
            activePaddles.Add(currentEndPaddle);
        }

        currentEndPaddle.renderer.material = targetPaddleMaterial;

        // Create new extension handler
        currentExtensionHandler = new GameObject("Extension Handler");
        currentExtensionHandler.transform.position = currentEndPaddle.transform.position + Vector3.up;
        currentExtensionHandler.AddComponent<BoxCollider>();
        ((BoxCollider) currentExtensionHandler.collider).isTrigger = true;
        ((BoxCollider) currentExtensionHandler.collider).size = currentEndPaddle.collider.bounds.size*1.5f;
        currentExtensionHandler.AddComponent<LevelContinuationScript>();
    }


    private Vector3 nextPaddlePosition(Vector3 prevPos)
    {
        float distance = Random.Range(minPaddleDistance, maxPaddleDistance);
        float maxXDistance = Mathf.Max(distance - distance/5, minPaddleDistance);

        float xDistance = Random.Range(minPaddleDistance, maxXDistance);
        float yDistance = Mathf.Sqrt(Mathf.Pow(distance, 2) - Mathf.Pow(xDistance, 2));

        if (yDistance >= 5)
        {
            yDistance = Random.Range(minPaddleDistance, 5);
            xDistance = Mathf.Sqrt(distance*distance - yDistance*yDistance);
        }

        if ((Random.Range(-1, 1) < 0) && !(prevPos.y <= -20))
        {
            yDistance *= -1;
        }

        return prevPos + new Vector3(xDistance, yDistance, 0);
    }

    private void Update()
    {
        if (!gameRunning)
        {
            return;
        }

        decayTimer += Time.deltaTime;

        if (decayTimer >= 10)
        {
            killOldLevel();
        }
    }
}