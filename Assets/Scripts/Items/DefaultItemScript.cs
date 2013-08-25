using UnityEngine;
using System.Collections;

public class DefaultItemScript : MonoBehaviour
{
    public float itemRotationSpeed = 20;

    void Update () {
	    transform.Rotate(0, itemRotationSpeed * Time.deltaTime, 0);
	}
}
