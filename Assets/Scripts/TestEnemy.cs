using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour {
    float time = 0.0f;
    private GameObject player;

    public Transform bullet;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 2) {
            Vector3 direction = (player.GetComponent<PlayerScript>().targetPosition() - transform.position).normalized;
            Transform bulletTransform = Instantiate(bullet, transform.position + 0.5f * direction, Quaternion.identity) as Transform;
            bulletTransform.SendMessage("SetVelocity", direction * 8.0f);

            time = 0.0f;
        }
	}
}
