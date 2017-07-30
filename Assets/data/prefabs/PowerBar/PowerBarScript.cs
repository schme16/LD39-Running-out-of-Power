using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarScript : MonoBehaviour {

	[Range (0, 100)]
	public float PowerLevel;

	public GameObject Fill;

	[SerializeField]
	private Color StartColour;

	[SerializeField]
	private Color EndColour;


	private float FillMaxSize = 3.940416f;
	private SpriteRenderer FillSR;

	void Start () {
		FillSR = Fill.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		if (PowerLevel > 100) PowerLevel = 100f;
		else if (PowerLevel < 0) PowerLevel = 0f;

		FillSR.color = Color.Lerp(StartColour, EndColour, PowerLevel * 0.01f);
		float x = ((FillMaxSize/100) * PowerLevel);
		FillSR.size = new Vector2(Mathf.Lerp(FillSR.size.x, x, 0.01f), FillSR.size.y);

	}
}
