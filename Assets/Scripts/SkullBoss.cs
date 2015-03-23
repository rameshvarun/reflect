using UnityEngine;
using System.Collections;

public class SkullBoss : MonoBehaviour {

    public Transform head;
    public Transform helmet;

    private Vector3 spawnPosition;

    private enum SkullState {
        Dormant,
        Helmeted,
        Naked,
        Dying,
        Dead
    }
    private SkullState state;

    public float waveSpeed;
    public float waveAmplitude;

    private GameObject player;

    public Transform leftHand;
    public Transform rightHand;

    private int health = 2;

	// Use this for initialization
	void Start () {
	    // Head Sprite Shadows
        head.GetComponentInChildren<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        head.GetComponentInChildren<SpriteRenderer>().receiveShadows = true;

        // Helmet Sprite Shadows
        helmet.GetComponentInChildren<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        helmet.GetComponentInChildren<SpriteRenderer>().receiveShadows = true;

        state = SkullState.Helmeted; // Start out in Dormant state
        spawnPosition = head.transform.position; // Keep track of the starting position

        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("FireHandBullet", 0.0f, 4.0f);
	}

    private bool fireFromRight;
    public Transform handBullet;
    public Transform rightHandBulletSpawn;
    public Transform leftHandBulletSpawn;
    public float handFireSpeed;
    void FireHandBullet() {
        if (state == SkullState.Helmeted || state == SkullState.Naked) {
            Transform spawnedBullet = Instantiate(handBullet, fireFromRight ? rightHandBulletSpawn.transform.position : leftHandBulletSpawn.transform.position, Quaternion.identity) as Transform;
            spawnedBullet.SendMessage("SetVelocity", new Vector3(fireFromRight ? -handFireSpeed : handFireSpeed,0,0));
            fireFromRight = !fireFromRight;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (state == SkullState.Helmeted) {
            
        }

        if ((state == SkullState.Helmeted && health == 0) || state == SkullState.Naked) {
            Vector3 targetPosition = spawnPosition + new Vector3(waveAmplitude * Mathf.Sin(Time.realtimeSinceStartup * waveSpeed), 0, 0);
            head.transform.position = Vector3.Lerp(head.transform.position, targetPosition, 1.0f*Time.deltaTime);
        }

        if (state == SkullState.Helmeted || state == SkullState.Naked) {
            Vector3 leftHandTarget = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y, player.transform.position.z);
            leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, leftHandTarget, 3.0f * Time.deltaTime);
            Vector3 rightHandTarget = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, player.transform.position.z);
            rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, rightHandTarget, 3.0f * Time.deltaTime);
        }

        HelmetFlash();
	}

    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
    }

    void ExplodeHelmet() {
        helmet.gameObject.SetActive(false);
    }

    public Sprite[] helmetCracking;
    void Hit() {
        Debug.Log("Head Hit By Bullet");

        if (state == SkullState.Helmeted) {
            if (health == 2) {
                helmet.GetComponent<SpriteRenderer>().sprite = helmetCracking[1];
                helmetFlashTime = 0.5f;
            } else if (health == 1) {
                helmet.GetComponent<SpriteRenderer>().sprite = helmetCracking[2];
                helmetFlashTime = 0.5f;
            } else if (health == 0) {
                state = SkullState.Naked;
                ExplodeHelmet();
            }

            --health;
        }
    }

    public float helmetFlashTime;
    void HelmetFlash() {
        if (helmetFlashTime > 0) {
            helmetFlashTime -= Time.deltaTime;
        } else {
            //helmet.GetComponent<SpriteRenderer>().color
        }
    }
}
