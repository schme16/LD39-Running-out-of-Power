using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiScript : MonoBehaviour {

	private Vector2 startPos;
	public float amplitude = 0.6f;
	public float period = 5f;

	public GameObject sad;
	public GameObject happy;
	public GameObject veryhappy;


	void Start () {
		startPos = transform.localPosition;
		happy.SetActive(false);
		veryhappy.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		float theta = Time.timeSinceLevelLoad / period;
		float distance = amplitude * Mathf.Sin(theta);
		transform.localPosition = startPos + Vector2.up * distance;
		float sadness = transform.parent.gameObject.GetComponent<CitizenScript>().sadnessRolling;
		if (sadness > 70f) {
			sad.SetActive(true);
			happy.SetActive(false);
			veryhappy.SetActive(false);
		}
		else if (sadness > 50f) {
			sad.SetActive(false);
			happy.SetActive(true);
			veryhappy.SetActive(false);
		}
		else {
			sad.SetActive(false);
			happy.SetActive(false);
			veryhappy.SetActive(true);
		}
	}
}
