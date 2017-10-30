using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Spawner spawner = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ImDead ()
	{
		if (spawner!= null)
			spawner.EnemyDead ();
	}
}
