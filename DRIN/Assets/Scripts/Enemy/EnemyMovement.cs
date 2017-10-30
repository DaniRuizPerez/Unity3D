using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
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
		if (transform!= null)
			distance = Mathf.Sqrt (Mathf.Pow(position.position.x - transform.position.x,2) + Mathf.Pow(position.position.z - transform.position.z,2));

		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
			//Debug.Log (animation.IsPlaying("simpleAttack") + " " +animation.IsPlaying("death"),position);

			nav.updatePosition = true;
			if (!animation.IsPlaying("simpleAttack") && !animation.IsPlaying("death")){
				//Debug.Log ("play run" + distance,position);

				animation.Play("run");
				nav.SetDestination (position.position);
			}else{
				nav.updatePosition = false;
			}
        }
        else
        {
            nav.enabled = false;
        }
    }
}
