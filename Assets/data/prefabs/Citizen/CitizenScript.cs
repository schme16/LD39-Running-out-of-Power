using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenScript : MonoBehaviour {

	public bool gender;
	public GameObject[] shirts;
	public GameObject[] heads;
	
	public float x;
	public float y;

	
	public float sadness;

	private Rand rand;
	private float timer;



	private Rigidbody2D rb;
	private GameObject head;
	private GameObject shirt;

	void Start () {
		rand = new Rand();

		rb = gameObject.GetComponent<Rigidbody2D>();

		//Shirt colour
		shirt = Instantiate(shirts[rand.In(0,  shirts.Length - 1)]);
		shirt.transform.parent = transform;
		shirt.transform.localPosition = new Vector2(0, 0);
		shirt.GetComponent<SpriteRenderer>().color = new Color((float)rand.In(0, 100)*0.01f, (float)rand.In(0, 100)*0.01f, (float)rand.In(0, 100)*0.01f, 1f);
		
		//Head
		head = Instantiate(heads[rand.In(0,  heads.Length - 1)]);
		head.transform.parent = transform;
		head.transform.localPosition = new Vector2(0, 0);


		//Position
		transform.localPosition = new Vector2(x, y);


	}
	

	void Update () {
		if (timer > 1) {
			timer = 0;
			int test = rand.In(0, 5);
			if (test == 5) {
				int angle = rand.In(0, 3) * 90;
				transform.eulerAngles = new Vector3(0, 0, angle);
			}
		}

		timer += Time.deltaTime;
	}
}
