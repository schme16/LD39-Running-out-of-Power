using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenScript : MonoBehaviour {

	public bool gender;
	public GameObject[] shirts;
	public GameObject[] heads;
	public GameObject Emoji;

	
	
	public float x;
	public float y;
	public AudioClip compliment;


	public float sadness;
	public float sadnessRolling;
	public float initialSadness;

	private Rand rand;
	public float timer;



	private Rigidbody2D rb;
	private GameObject head;
	private GameObject shirt;
	private GameObject emoji;

	void Start () {
		rand = new Rand();

		timer = 0;
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


		//emoji
		emoji = Instantiate(Emoji);
		emoji.transform.parent = transform;
		emoji.transform.localPosition = new Vector2(0, 0.12f);


		sadnessRolling = sadness;
		initialSadness = sadness;
	}
	

	void Update () {
		sadnessRolling = Mathf.Lerp(sadnessRolling, sadness, 0.003f);
		if (timer > 1) {
			timer = 0;
			int test = rand.In(0, 5);
			if (test == 5) {
				int angle = rand.In(0, 3) * 90;
				head.transform.eulerAngles = new Vector3(0, 0, angle);
				shirt.transform.eulerAngles = new Vector3(0, 0, angle);
				
			}
		}

		timer += Time.deltaTime;
	}
}
