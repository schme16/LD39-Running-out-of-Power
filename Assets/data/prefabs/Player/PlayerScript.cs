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

	private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2) {
		Vector2 diference = vec2 - vec1;
		float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
		return Vector2.Angle(Vector2.right, diference) * sign;
	}

	void FixedUpdate () {
		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector2 move = new Vector2(0, 0);

		if (h > 0) {
			move = move + Vector2.right;
		}

		if (h < 0) {
			move  = move + Vector2.left;
		}

		if (v > 0) {
			move = move + Vector2.up;
		}

		if (v < 0) {
			move  = move + Vector2.down;
		}



		if (h == 0 && v == 0) {
			anim.Play("stand");
		}
		else {
			bool shift = Input.GetKey("left shift") | Input.GetKey("right shift");
			transform.eulerAngles = new Vector3(0, 0, AngleBetweenVector2(rb.position, rb.position + move));

			rb.MovePosition(rb.position + (move * ((speed * (shift ? 2 : 1)) * Time.fixedDeltaTime)));
			PowerLevel = PowerLevel - PowerLevelDrainRate * (Time.fixedDeltaTime * (shift ? 2 : 1));
			anim.Play("walk");
		}


		PowerLevel = Mathf.Min(100, Mathf.Max(0, PowerLevel));
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.transform.tag == "citizen") {
			transform.parent.GetComponent<StageScript>().ShowComplimentBox(col.GetComponent<CitizenScript>());
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		transform.parent.GetComponent<StageScript>().HideComplimentBox(col.GetComponent<CitizenScript>());
	}

}
