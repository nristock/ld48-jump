using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
    public AudioClip collectSound;

    private void OnTriggerEnter()
    {
        WorldManager.PlayerController.money++;
        audio.PlayOneShot(collectSound, .4f);
        renderer.enabled = false;
        collider.enabled = false;
        Destroy(gameObject, 0.25f);
    }
}
