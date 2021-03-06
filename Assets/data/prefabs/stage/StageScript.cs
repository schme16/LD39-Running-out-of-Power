﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;
using TMPro;

public class StageScript : MonoBehaviour {


	public PlayerScript player;
	public PowerBarScript PowerBar;
	public GameObject baseCitizen;
	

	public GameObject Map;
	public GameObject PlayingCanvas;
	public GameObject Citizens;
	public GameObject MainMenu;
	public GameObject GameOverText;
	public GameObject GameOverButton;
	public GameObject GameOverScore;
	public GameObject PlayButton;
	public float score = 0f;


	

	public GameObject CitizenStage;
	public GameObject CititzenSpawnLocations;
	public AudioClip[] Compliments;

	public ComplimentBoxScript ComplimentBox;

	public string state;

	
	private Rand rand;
	private Camera cam;
	private ProCamera2DTransitionsFX camTrans;
	private Vector2[] CitizenSpawnLocationAsArray;

	void Start () {

		CitizenSpawnLocationAsArray = new Vector2[CititzenSpawnLocations.transform.childCount];

		int _i = 0;
		foreach(Transform loc in CititzenSpawnLocations.transform) {
			CitizenSpawnLocationAsArray[_i] = new Vector2(loc.position.x, loc.position.y);
			loc.gameObject.SetActive(false);
			_i++;
		}

		rand = new Rand();
		cam = Camera.main;
		camTrans = cam.GetComponent<ProCamera2DTransitionsFX>();
		

		ShowMainMenu(true);
	}

	void NewGame (bool FirstRun) {
		score = 0;
		player.PowerLevel = 100f;
		player.PowerLevelDrainRate = 5f;
		player.transform.localPosition = new Vector2(-7.906f, 0.157f);

		float x = 2;
		float y = 2;

		
		CreateCitizens();
		state = "playing";
		camTrans.TransitionEnter();
	}

	void StartNewGame () {
		ShowMainMenu(false);
		NewGame(true);
	}

	void GameOver () {
		state = "gameover";
		camTrans.TransitionExit();
		GameOverText.SetActive(true);
		GameOverButton.SetActive(true);
		GameOverScore.SetActive(true);
		GameOverScore.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
		PlayButton.SetActive(false);
		Invoke("ShowMainMenu", 0.6f);
	}

	void ShowMainMenu () {
		ShowMainMenu(true);
	}
	void ShowMainMenu (bool set) {
		state = set ? "mainmenu" : "playing";
		camTrans.TransitionEnter();
		player.gameObject.transform.parent.gameObject.SetActive(!set);
		MainMenu.SetActive(set);
	}

	CitizenScript CreateCitizen (float x, float y, bool gender) {
		CitizenScript c = Instantiate(baseCitizen).GetComponent<CitizenScript>();
		c.gender = gender;
		c.x = x;
		c.y = y;
		c.sadness = (float)rand.In(70, 100);
		c.gameObject.transform.parent = CitizenStage.transform;
		return c;
	}

	void CreateCitizens () {
		string citizenLocations = "";
		string usedCompliments = "";
		int numCitizens = rand.In(4, CititzenSpawnLocations.transform.childCount - 1);

		for (int i = 0; i < numCitizens; i++) {

			int checkLoc = rand.In(0, CititzenSpawnLocations.transform.childCount - 1);
			while (citizenLocations.Contains("-" + checkLoc + "-") == true) {
				checkLoc = rand.In(0, CititzenSpawnLocations.transform.childCount - 1);
			}
			citizenLocations += ("-" + checkLoc + "-");
			
			int checkCompliment = rand.In(0, Compliments.Length-1);
			while (usedCompliments.Contains("-" + checkCompliment + "-") == true) {
				checkCompliment = rand.In(0, Compliments.Length-1);
			}
			usedCompliments += ("-" + checkCompliment + "-");


			Vector2 pos = (Random.insideUnitCircle * 0.06f) + CitizenSpawnLocationAsArray[checkLoc];
			CitizenScript c = CreateCitizen(pos.x, pos.y, rand.In(0,1) == 1);
			c.compliment = Compliments[checkCompliment];
		}
	}

	public void ShowComplimentBox (CitizenScript citizen) {
		if (citizen.sadness > 0) {
			ComplimentBox.gameObject.SetActive(true);
			ComplimentBox.citizen = citizen;
		}
	}

	public void HideComplimentBox (CitizenScript citizen) {
		ComplimentBox.gameObject.SetActive(false);
	}

	void Update () {
		if (state == "playing") {

			if (ComplimentBox.citizen != null && ComplimentBox.gameObject.activeInHierarchy && Input.GetKeyDown("space")) {
				if (ComplimentBox.citizen.sadness > 0 && ComplimentBox.citizen.gameObject.AddComponent<AudioSource>().isPlaying == false) {
					ComplimentBox.gameObject.SetActive(false);
					AudioSource a = ComplimentBox.citizen.gameObject.AddComponent<AudioSource>();
					a.clip = ComplimentBox.citizen.compliment;
					a.Play();
				}
			}
			//Set powerbar
			PowerBar.PowerLevel = player.PowerLevel;
		}

		if (state == "mainmenu") {
		}


		if (player.PowerLevel == 0 && state == "playing") {
			GameOver();
		}

		foreach(Transform c in CitizenStage.transform) {
			AudioSource a = c.gameObject.GetComponent<AudioSource>();
			if(!!a && !a.isPlaying) {
				CitizenScript cScript = c.gameObject.GetComponent<CitizenScript>();
				player.PowerLevel += cScript.sadness;
				score += cScript.sadness;
				cScript.sadness = 0;
				Destroy(a);
			}
		}
	}
}
