using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private CharacterController controller;

    public float runSpeed;
    public float walkSpeed;

    public float shieldDistance;

    public Transform shield;
    private Rigidbody shieldBody;

    public Transform playerSprite;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>(); // Character Controller

        // Player Sprite Shadows
        playerSprite.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        playerSprite.GetComponent<SpriteRenderer>().receiveShadows = true;

        shieldBody = shield.GetComponent<Rigidbody>();
	}


    /// <summary>
    /// Get the location on the player's body that enemies should aim bullets at.
    /// This allows enemeis to aim slightly towards the player's weapon.
    /// </summary>
    /// <returns>The location</returns>
    public Vector3 targetPosition() {
        if (isAiming) return transform.position + shieldDistance*aimDirection;
        else return transform.position;
    }

    /// <summary>
    /// Get the position where an attacked bullet should spawn at.
    /// </summary>
    /// <param name="distance">How far from the player the bullet was when it was attacked.</param>
    /// <returns>The position to place the bullet at for the next grame.</returns>
    public Vector3 bulletPosition(float distance) {
        return transform.position + aimDirection * distance;
    }

    void FixedUpdate() {
        if (isAiming) {
            shield.gameObject.SetActive(true);

            float lerpFactor = 10.0f * Time.fixedDeltaTime;

            Vector3 shieldOffset = shieldBody.position - transform.position;
            Vector3 shieldTargetOffset = shieldDistance * aimDirection;

            Vector2 polar = Utils.CartesianToPolar(new Vector2(shieldOffset.x, shieldOffset.z));
            Vector2 targetPolar = Utils.CartesianToPolar(new Vector2(shieldTargetOffset.x, shieldTargetOffset.z));

            Vector2 newPolar = Utils.PolarLerp(polar, targetPolar, lerpFactor);
            Vector2 offset = Utils.PolarToCartesian(newPolar);
            
            shieldBody.MovePosition(transform.position + new Vector3(offset.x, 0, offset.y));
            shieldBody.MoveRotation(Quaternion.Lerp(shieldBody.rotation, Quaternion.LookRotation(aimDirection), lerpFactor));
        } else {
            shield.gameObject.SetActive(false);
        }
    }

    bool isAiming;
    Vector3 aimDirection;
    Vector3 movementVector;
    void UpdateInputs() {
        movementVector.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // Mouse controls
        if (Input.GetMouseButton(1)) {
            Vector3 diff = new Vector3(Input.mousePosition.x - Screen.width / 2, 0, Input.mousePosition.y - Screen.height / 2);
            aimDirection = diff.normalized;
            isAiming = true;
        } else {
            Vector3 aim = new Vector3(Input.GetAxis("AttackHorizontal"), 0, Input.GetAxis("AttackVertical"));
            aimDirection = aim.normalized;
            isAiming = aim.magnitude > 0.2;
        }      
    }

	// Update is called once per frame
	void Update () {
        UpdateInputs();

        float speed = isAiming ? walkSpeed : runSpeed;
        controller.SimpleMove(speed * movementVector);
	}
}
