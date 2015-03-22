using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private CharacterController controller;
    public float movementSpeed;

    public float shieldDistance;
    public float attackDistance;

    public Transform shield;
    public Transform attackBounds;
    
    public bool shieldMode;

    // Variables for handling attacking 
    private bool attacking;
    private Vector3 attackDirection;
    private float attackTime;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>(); // Character Controller
        attacking = false; // Start out not attacking
	}

    Vector3 lastAim;
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 aim = new Vector3(Input.GetAxis("AttackHorizontal"), 0, Input.GetAxis("AttackVertical"));
        if (attacking) movement = Vector3.zero; // Can't move while attacking

        controller.SimpleMove(movementSpeed*movement);

        if (shieldMode) {
            if (aim.magnitude > 0.2) {
                aim.Normalize();
                shield.gameObject.SetActive(true);
                shield.transform.localPosition = shieldDistance * aim;

                float angleBetween = Vector3.Angle(new Vector3(0, 0, 1), aim);
                if (aim.x < 0) angleBetween = -angleBetween;

                Quaternion rotation = Quaternion.AngleAxis(angleBetween, Vector3.up);
                shield.transform.rotation = rotation;
                
            }
            else {
                shield.gameObject.SetActive(false);
            }
        }
        else {
            shield.gameObject.SetActive(false);

            if (attacking == false && aim.magnitude >= 0.5 && lastAim.magnitude <= 0.5) {
                attacking = true;
                attackTime = 0.1f;
                attackDirection = aim.normalized;
            }
        }

        if (attacking) {
            attackBounds.gameObject.SetActive(true); // Draw the attack bounds

            attackBounds.transform.localPosition = attackDistance * attackDirection; // Set attack bounds to right in fornt of player

            // Rotate attack bounds to be coming out of player
            float angleBetween = Vector3.Angle(new Vector3(0, 0, 1), attackDirection);
            if (attackDirection.x < 0) angleBetween = -angleBetween;
            Quaternion rotation = Quaternion.AngleAxis(angleBetween, Vector3.up);
            attackBounds.transform.rotation = rotation;

            // End of attack time
            attackTime -= Time.deltaTime;
            if (attackTime < 0) attacking = false;
        } else {
            attackBounds.gameObject.SetActive(false); // Don't draw attack bounds
        }

        lastAim = aim; // Keep track of the aim from the previous frame
	}
}
