using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : Health {

	public Slider healthSlider;
	bool damaged;
	AudioSource[] audio;
	//Animator anim;

	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public override void Awake()
	{
		base.Awake ();
		//anim = GetComponent <Animator> ();
		audio = GetComponents <AudioSource> ();

	}
	
	void Update ()
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
		healthSlider.value = currentHealth;
	}

	public override void TakeDamage (int amount, Vector3 hitPoint)
	{
		currentHealth -= amount;
		
		if (currentHealth <= 0)
		{
			Debug.Log ("morro",healthSlider);
			audio[0].Play (); 
			DirectorBeh directorBeh = (DirectorBeh)GameObject.Find ("Director").GetComponent(typeof(DirectorBeh));
			directorBeh.GameOver();

		}
		
		damaged = true;
		
		healthSlider.value = currentHealth;
	}
}
