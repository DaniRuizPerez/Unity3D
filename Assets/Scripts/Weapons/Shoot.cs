using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	int _damagePerShoot = 20;
	bool _isPlayerShoot = true;

	void Start ()
	{
		Destroy (this.gameObject, (float)5);
	}
	
	public void SetDamagePerShoot (int damage)
	{
		_damagePerShoot = damage;
	}

	public void Update() 
	{
		
	}

	public void isPlayerShoot () 
	{
		_isPlayerShoot = true;
	}

	public void isEnemyShoot () 
	{
		_isPlayerShoot = false;
	}

	public bool IsPlayer(Collider other)
	{
		return (other.tag == "Player" || (other.transform.parent != null && other.transform.parent.tag == "Player"));
	}

	void OnTriggerEnter(Collider other) 
	{
		bool isPlayer = IsPlayer (other); 

		if (((isPlayer && _isPlayerShoot) || (!isPlayer && !_isPlayerShoot)) && !(!isPlayer && _isPlayerShoot) )
		{
			return;
		}
		
		if (other.tag == "EnemyLife" || isPlayer) 
		{
			Health health = other.GetComponent <Health> ();
			
			if (health == null) 
				health = other.transform.parent.GetComponent <Health> ();
			
			if (health != null) 
			{
				health.TakeDamage (_damagePerShoot, new Vector3 ());
				Destroy (gameObject);
			} 

			Destroy (gameObject);
		}

		if (other.gameObject.tag == "RangeArea")
			return;
	}
}
