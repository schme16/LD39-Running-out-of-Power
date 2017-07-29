using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float x;
	public float y;
	public float speed = 10f;
	public GameObject sprite;
	public float PowerLevel;
	public float PowerLevelDrainRate;
	
	private Rigidbody2D rb;
	private Animator anim;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = sprite.GetComponent<Animator>();


		anim.Play("stand");
	}

	void Update () {
		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (h > 1) {
			Vector2 x = new Vector2(Vector2.left.x * speed, 0f);
			rb.MovePosition(rb.position + (x * Time.fixedDeltaTime));
		}

		else if (h < 0) {

		}



		if (h == 0 && v == 0) {
			anim.Play("stand");
		}
		else {
			PowerLevel = PowerLevel - PowerLevelDrainRate * Time.deltaTime;
			anim.Play("walk");
		}


		PowerLevel = Mathf.Min(100, Mathf.Max(0, PowerLevel));

	}
}
