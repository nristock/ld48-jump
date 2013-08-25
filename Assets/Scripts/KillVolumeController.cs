using UnityEngine;
using System.Collections;

public class KillVolumeController : MonoBehaviour {
    void Update ()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, -30, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerController>().kill();
        }
    }
}
