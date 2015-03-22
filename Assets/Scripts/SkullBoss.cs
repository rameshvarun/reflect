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

	// Use this for initialization
	void Start () {
	    // Head Sprite Shadows
        head.GetComponentInChildren<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        head.GetComponentInChildren<SpriteRenderer>().receiveShadows = true;

        // Helmet Sprite Shadows
        head.GetComponentInChildren<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        head.GetComponentInChildren<SpriteRenderer>().receiveShadows = true;

        state = SkullState.Helmeted; // Start out in Dormant state
        spawnPosition = transform.position; // Keep track of the starting position
	}
	
	// Update is called once per frame
	void Update () {
        if (state == SkullState.Helmeted) {
            Vector3 targetPosition = spawnPosition + new Vector3(waveAmplitude * Mathf.Sin(Time.realtimeSinceStartup * waveSpeed), 0, 0);
            transform.position = targetPosition;
        }
	}
}
