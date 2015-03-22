using UnityEngine;
using System.Collections;

public enum Side {
    PlayerSide,
    EnemySide
}

public class BulletScript : MonoBehaviour {
    private Vector3 velocity;
    private Side side;

    private GameObject shield;
    private GameObject attackBounds;

    private GameObject player;

	// Use this for initialization
	void Start () {
        side = Side.EnemySide;
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("EndOfLife", 10.0f);
	}

    public void EndOfLife() {
        Destroy(this.gameObject);
    }

    void MoveBullet() {
        Vector3 delta = velocity * Time.deltaTime;

        bool hitShield = false;
        if (shield) hitShield = shield.GetComponent<Collider>().bounds.Contains(transform.position);

        bool inAttackBounds = false;
        if (attackBounds) inAttackBounds = attackBounds.GetComponent<Collider>().bounds.Contains(transform.position);


        RaycastHit hit;
        if (Physics.Raycast(transform.position, delta, out hit, delta.magnitude * 1.5f)) {
           if (hit.collider.tag == "Shield") hitShield = true;
           if (hit.collider.tag == "Player") EndOfLife();
        }

        if (side == Side.EnemySide) {
            if (inAttackBounds) {
                side = Side.PlayerSide;
                velocity = velocity.magnitude * attackBounds.transform.forward;
                transform.position = player.GetComponent<PlayerScript>().bulletPosition(Vector3.Distance(transform.position, player.transform.position));
                Debug.Log("Attacked");
                return;
            }

            if(hitShield) {
                velocity = Vector3.Reflect(velocity, shield.transform.forward);
                side = Side.PlayerSide;
                Debug.Log("In Shield");
                return;
            }
        }

        transform.Translate(delta);
    }
	
	// Update is called once per frame
	void Update () {
        shield = GameObject.FindGameObjectWithTag("Shield");
        attackBounds = GameObject.FindGameObjectWithTag("AttackBounds");

        MoveBullet();
	}

    public void SetVelocity(Vector3 vel) {
        velocity = vel;
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if(collision.collider.tag == "Shield") {
            
                GetComponent<Rigidbody>().velocity = -GetComponent<Rigidbody>().velocity;
                side = Side.PlayerSide;
            }
            
        }
    }*/
}
