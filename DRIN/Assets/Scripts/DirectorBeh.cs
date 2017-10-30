using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;

public class DirectorBeh : MonoBehaviour
{
	int level = 1;
	GameObject director;
	GameObject player;
	ArrayList protectedObjectslist = new ArrayList();

	int spawners = 0;
	
	public void Start() {
		director = GameObject.Find ("Director");
		player = GameObject.Find ("ROBOTCHULISIMO");
		saveGameObjects ();

	}

	public void GameOver() {
		destroySavedObjects ();
		Application.LoadLevel (7);
	}

	void Update() {
		if (nextLevelCondition()) {
			loadNextScene();
		}	

		if ((Input.GetKeyDown (KeyCode.X)))
			GameOver ();
		
	}

	public void spawnerAdd () 
	{
		spawners ++;
	}

	public void spawnerEnded () 
	{
		spawners --;

		if (spawners <= 0)
			loadNextScene ();
	}

	void saveGameObjects() {
		protectedObjectslist.Add (director);
		protectedObjectslist.Add(GameObject.Find ("HUDCanvas"));
		protectedObjectslist.Add(player);
		protectedObjectslist.Add(GameObject.Find ("Directional light"));
		protectedObjectslist.Add(GameObject.Find ("DeathWall"));
		foreach (GameObject go in protectedObjectslist)
			DontDestroyOnLoad (go);
	}

	void destroySavedObjects() {
		foreach (GameObject go in protectedObjectslist)
			Destroy (go);
	}
	
	bool nextLevelCondition() {
		return (Input.GetKeyDown (KeyCode.Z));
	}


	void loadNextScene() {
		GameObject.Find ("Character Controller").transform.position = new Vector3 (50, 150, 50);	
		level++;
		if (level > 6){
			level = 8;
			destroySavedObjects ();
		}
		Application.LoadLevel (level);
	}
}