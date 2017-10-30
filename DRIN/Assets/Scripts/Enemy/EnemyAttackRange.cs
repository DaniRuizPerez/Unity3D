using UnityEngine;
using System.Collections;

public class EnemyAttackRange : MonoBehaviour
{
    public float timeBetweenAttacks = 1.0f;
    public int attackDamage = 10;

	public Weapon weapon;
	public Shoot shoot;
	public float shootSpeed = 10f;
	public int shootDamage = 20;


   // Animator anim;
	GameObject position;
    GameObject player;
    bool playerInRange;
    float timer;
	AudioSource[] enemyAudio;
	PlayerHealth playerHealth;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
		position = GameObject.FindGameObjectWithTag ("GameController");
		playerHealth = player.GetComponent <PlayerHealth> ();

        //anim = GetComponent <Animator> ();
		enemyAudio = transform.parent.GetComponents <AudioSource> ();

		if (shoot != null) {
			weapon._shoot = shoot;
		}
		weapon._speed = shootSpeed;
		weapon._damage = shootDamage;
    }


    void OnTriggerEnter (Collider other)
    {
//		Debug.Log ("SOMETHINGENTERED",other.gameObject);

        if(other.gameObject == player)
        {
//			Debug.Log ("inrange",player);
            playerInRange = true;
        }
		if(other.gameObject == position)
		{
//			Debug.Log ("position inrange",player);
			playerInRange = true;
		}
    }


    void OnTriggerExit (Collider other)
    {
//		Debug.Log ("SOMETHINGEXITED",other.gameObject);
	
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
		if(other.gameObject == position)
		{
		//	Debug.Log ("position EXIT inrange",player);
			playerInRange = false;

		}
    }

	void OnTriggerStay (Collider other) 
	{
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			Attack (other);
		}
	}

	void Update ()
	{
		timer += Time.deltaTime;
	}

	void Attack (Collider other)
    {
		Health enemyHealth = transform.parent.GetComponent <Health>();

		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
			Debug.Log (timer +" " + timeBetweenAttacks,playerHealth);
			timer = 0f;
			weapon.Fire();

        }

 //       if(playerHealth.currentHealth <= 0)
 //       {
//            anim.SetTrigger ("PlayerDead");
//        }
    }


 
}
