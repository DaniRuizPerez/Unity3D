using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2.0f;
    public int attackDamage = 10;


   // Animator anim;
	GameObject position;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
	AudioSource[] enemyAudio;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
		position = GameObject.FindGameObjectWithTag ("GameController");
        playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = transform.parent.GetComponent<EnemyHealth>();
        //anim = GetComponent <Animator> ();
		enemyAudio = transform.parent.GetComponents <AudioSource> ();

    }


    void OnTriggerEnter (Collider other)
    {
//		Debug.Log ("SOMETHINGENTERED",player);

		if(other.gameObject == position)
        {
//			Debug.Log ("inrange",position);
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
//		Debug.Log ("SOMETHINGEXITED",position);

	
		if(other.gameObject == position)
        {
            playerInRange = false;
        }
    }

	void OnTriggerStay (Collider other) 
	{
		bool isPlayer = IsPlayer (other);
		if (timer >= timeBetweenAttacks && playerInRange && isPlayer && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			Attack ();
		} else {
			if (timer >= timeBetweenAttacks) {
//				animation.Play ("run");
			}
		}
	}

	public bool IsPlayer(Collider other)
	{
		return ((other != null && other.gameObject != null && other.gameObject.tag == "Player") || (other.transform.parent != null && other.transform.parent.tag == "Player"));
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks) {
			transform.parent.animation.Stop("simpleAttack");
		}

	}


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
			Debug.Log ("attack",playerHealth);
			transform.parent.animation.Play("simpleAttack");
            playerHealth.TakeDamage (attackDamage, new Vector3 ());
			enemyAudio[1].Play();

        }
    }
}
