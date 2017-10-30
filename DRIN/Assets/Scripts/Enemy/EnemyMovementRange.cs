using UnityEngine;
using System.Collections;

public class EnemyMovementRange : MonoBehaviour
{
	Transform player;
	Transform position;
	Health playerHealth;
	Health enemyHealth;
    NavMeshAgent nav;
	float distance;


    void Awake ()
    {

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		position = GameObject.FindGameObjectWithTag ("GameController").transform;
		playerHealth = player.GetComponent <Health> ();
		enemyHealth = GetComponent <Health> ();
        nav = GetComponent <NavMeshAgent> ();
		distance = Mathf.Sqrt (Mathf.Pow(position.position.x - transform.position.x,2) + Mathf.Pow(position.position.z - transform.position.z,2));
	}


    void Update ()
    {
		distance = Mathf.Sqrt (Mathf.Pow(position.position.x - transform.position.x,2) + Mathf.Pow(position.position.z - transform.position.z,2));
		//Debug.Log ("distance" + distance,position);

		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 )
        {
			if (distance > 10){
				nav.updatePosition = true;
            	nav.SetDestination (position.position);
				if (enemyHealth.maximumHealth != 11)
					animation.Play("run");
				
			}
			else {
				nav.updatePosition = false;
				//animation.Play("idle");

			}
        }
        else
        {
            nav.enabled = false;
        }
    }
}
