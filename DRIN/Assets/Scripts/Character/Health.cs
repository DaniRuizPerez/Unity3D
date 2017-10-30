using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

	public int maximumHealth = 100;

	public int currentHealth = 0;

	public virtual void Awake()
	{
		currentHealth = maximumHealth;
	}


	public virtual void TakeDamage (int amount, Vector3 hitPoint)
	{
		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			Destroy (gameObject);
		}
	}
}
