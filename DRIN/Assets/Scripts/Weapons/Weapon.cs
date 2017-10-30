using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Shoot _shoot;
	public float _speed = 10f;
	public int _damage = 20;

	public bool IsPlayerWeapon = false;

	public int posForward = 2;
	public int posUp = 2;

	public float timeBetweenShoots = 0.5f;
	float timer;

	void Update ()
	{
		timer += Time.deltaTime;

		if (IsPlayerWeapon && Input.GetMouseButtonDown (0)) 
		{
			Fire ();
		}
	}


	public void Fire () {

		if (timer >= timeBetweenShoots) 
		{
			Vector3 shootPosition = transform.TransformPoint (Vector3.forward * posForward + Vector3.up * posUp);
			Shoot shoot = (Shoot)Instantiate (_shoot, shootPosition, transform.rotation);
			timer = 0f;

			shoot.rigidbody.velocity = transform.TransformDirection (Vector3.forward * _speed);

			shoot.isPlayerShoot ();
			shoot.SetDamagePerShoot (_damage);

			(GetComponents <AudioSource> ())[0].Play();

			if (IsPlayerWeapon) {
				shoot.isPlayerShoot ();
			} else {
				shoot.isEnemyShoot ();
			}
		}
	}
}
