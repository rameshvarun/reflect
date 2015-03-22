using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

    public Transform leftLung;
    public Transform rightLung;

    public float lungSpeed;

	// Use this for initialization
	void Start () {
        // Heart Sprite Shadows
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<SpriteRenderer>().receiveShadows = true;

        // Lung Sprite Shadows
        leftLung.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        leftLung.GetComponent<SpriteRenderer>().receiveShadows = true;
        rightLung.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        rightLung.GetComponent<SpriteRenderer>().receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.AngleAxis(Time.deltaTime * lungSpeed, Vector3.up);
        leftLung.localPosition = rotation * leftLung.localPosition;
        rightLung.localPosition = rotation * rightLung.localPosition;
	}
}
