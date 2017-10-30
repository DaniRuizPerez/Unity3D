using UnityEngine;

public class EnemyHealth : Health
{
    public float sinkSpeed = 2.9f;
    public int scoreValue = 10;
   // public AudioClip deathClip;


    AudioSource[] enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
	Health enemyHealth;
    bool isDead;
    bool isSinking;
	float timer;
	
    void Awake ()
    {
		base.Awake ();
        enemyAudio = GetComponents <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
		enemyHealth = GetComponent <Health> ();

    }


    void Update ()
    {
        if(isSinking)
		{	
			timer += Time.deltaTime;
			if (timer >1.5f){
            	transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			}
        }

		if (currentHealth <= 0) 
		{
			Death ();
		} 
    }


	public override void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

//        enemyAudio.Play ();

        currentHealth -= amount;
            
//        hitParticles.transform.position = hitPoint;
//        hitParticles.Play();

        if (currentHealth <= 0) 
		{
			Death ();
		} 


    }


    void Death ()
    {
		enemyAudio[0].Play();
        isDead = true;	
		if (enemyHealth.maximumHealth != 11)
			animation.Play("death");

		StartSinking ();


    }


    public void StartSinking ()
    {
		if (!isSinking)
		{
			GetComponent <NavMeshAgent> ().enabled = false;
	        GetComponent <Rigidbody> ().isKinematic = true;
	        isSinking = true;
	        ScoreManager.score += scoreValue;
			Enemy me = GetComponent <Enemy> ();
			if (me != null)
				GetComponent <Enemy> ().ImDead ();
	        Destroy (gameObject, 4f);
		}
    }
}
