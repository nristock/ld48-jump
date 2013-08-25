using UnityEngine;
using System.Collections;

public class PlayerTracker : MonoBehaviour {
    public GameObject Player { get; set; }

    void LateUpdate()
    {
        if (Player != null)
        {
            transform.LookAt(Player.transform, Vector3.up);
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        }
    }
}
