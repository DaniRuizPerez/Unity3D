using UnityEngine;
using System.Collections;

public class DeathWallBehaviour : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		GameObject go = GameObject.Find("Director");
		DirectorBeh other = (DirectorBeh) go.GetComponent(typeof(DirectorBeh));
		other.GameOver ();

	}
}
