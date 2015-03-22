using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 4.0f * Time.deltaTime);
	}
}
