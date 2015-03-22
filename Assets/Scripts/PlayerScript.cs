using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private CharacterController controller;

    public float runSpeed;
    public float walkSpeed;

    public float shieldDistance;
    public float attackDistance;
    public float swordDistance;

    public Transform shield;
    public Transform attackBounds;
    public Transform playerSprite;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>(); // Character Controller

        // Player Sprite Shadows
        playerSprite.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        playerSprite.GetComponent<SpriteRenderer>().receiveShadows = true;
	}

    Vector3 lastAim;
    float lastTrigger;

    /// <summary>
    /// Get the location on the player's body that enemies should aim bullets at.
    /// This allows enemeis to aim slightly towards the player's weapon.
    /// </summary>
    /// <returns>The location</returns>
    public Vector3 targetPosition() {
        if (lastAim.magnitude > 0.2) return transform.position + 0.2f*lastAim.normalized;
        else return transform.position;
    }

    /// <summary>
    /// Get the position where an attacked bullet should spawn at.
    /// </summary>
    /// <param name="distance">How far from the player the bullet was when it was attacked.</param>
    /// <returns>The position to place the bullet at for the next grame.</returns>
    public Vector3 bulletPosition(float distance) {
        return transform.position + lastAim.normalized * distance;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 aim = new Vector3(Input.GetAxis("AttackHorizontal"), 0, Input.GetAxis("AttackVertical"));
        float trigger = Input.GetAxis("Attack");

        // Mouse controls
        if (Input.GetMouseButton(1)) {
            Vector3 diff = new Vector3(Input.mousePosition.x - Screen.width/2, 0, Input.mousePosition.y - Screen.height / 2);
            aim = diff.normalized;
            if (Input.GetMouseButton(0)) trigger = 1.0f;
        }

        float speed = (aim.magnitude > 0.2) ? walkSpeed : runSpeed;
        controller.SimpleMove(speed * movement);

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

        // Keep track of Input data from the previous frame
        lastAim = aim;
        lastTrigger = trigger;
	}
}
