using UnityEngine;
using System.Collections;

public class LevelContinuationScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            WorldManager.LevelManager.extendLevel();
        }
    }
}
