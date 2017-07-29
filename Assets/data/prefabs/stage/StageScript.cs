using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour {


	public PlayerScript player;
	public PowerBarScript PowerBar;
	public GameObject baseCitizen;

	private Rand rand;

	void Start () {
		rand = new Rand();
		NewGame(true);
	}

	void NewGame (bool FirstRun) {
		player.PowerLevel = 100f;
		player.PowerLevelDrainRate = 5f;

		float x = 2;
		float y = 2;

		CreateCitizen((float)rand.In(-30,30) *0.1f, (float)rand.In(-30,30) * 0.1f, rand.In(0,1) == 1);


	}

	void CreateCitizen (float x, float y, bool gender) {
		CitizenScript c = Instantiate(baseCitizen).GetComponent<CitizenScript>();
		c.gender = gender;
		c.x = x;
		c.y = y;
		c.sadness = rand.In(10, 100);
	}


	void Update () {
		
		//Set powerbar
		PowerBar.PowerLevel = player.PowerLevel;
	}
}
