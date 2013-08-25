using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour
{
    public AudioClip collectSound;
    
    private void OnTriggerEnter()
    {
        WorldManager.PlayerController.boostSpeed(2, 3);
        audio.Play();
        renderer.enabled = false;
        collider.enabled = false;
        Destroy(gameObject, 0.25f);
    }
}
