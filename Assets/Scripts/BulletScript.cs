using UnityEngine;
using System.Collections;

public enum Side {
    PlayerSide,
    EnemySide
}

public class BulletScript : MonoBehaviour {
    private Vector3 velocity;
    private Side side;

	// Use this for initialization
	void Start () {
        side = Side.EnemySide;
        Invoke("EndOfLife", 10.0f);
	}

    public void EndOfLife() {
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SetVelocity(Vector3 vel) {
        GetComponent<Rigidbody>().velocity = vel;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Shield") {
            side = Side.PlayerSide;
            return;
        } 
        
        if(collision.collider.tag == "Enemy" && side == Side.PlayerSide) {
            collision.collider.SendMessageUpwards("Hit");
        }

        EndOfLife();
    }
}
