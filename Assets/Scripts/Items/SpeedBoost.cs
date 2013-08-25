using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour {
    private void OnTriggerEnter()
    {
        WorldManager.PlayerController.boostSpeed(2, 3);
        Destroy(gameObject);
    }
}
