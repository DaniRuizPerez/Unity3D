using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Enemy _enemy;
	public int totalSpawns = 5;
	public int totalSimultaneouslly = 1;
	public float timeBetweenSpawns = 5f;


	float timer = 0f;
	int actualSimultaneouslly = 0;
	int total= 0;

	public SpawnPoint point1;
	public SpawnPoint point2;
	public SpawnPoint point3;


	DirectorBeh director;

	void Start(){

	}

	// Use this for initialization
	void Awake () {
		director = (DirectorBeh)GameObject.Find ("Director").GetComponent(typeof(DirectorBeh));
		timer = timeBetweenSpawns + 1;
		director.spawnerAdd ();
		total = totalSpawns;
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		if (timer > timeBetweenSpawns && actualSimultaneouslly < totalSimultaneouslly && total > 0 )
		{
			Transform trans = transform;

			float selection = Random.Range (0, 3);

			if (0 <= selection && selection < 1)
				trans = point1.transform;

			if (1 <= selection && selection < 2)
				trans = point2.transform;

			if (2 <= selection && selection < 3)
				trans = point3.transform;

			Enemy enemy = (Enemy)Instantiate (_enemy, trans.position, trans.rotation);

			enemy.spawner = this;
			actualSimultaneouslly += 1;
			timer = 0f;
			total --;


		}
		if (total <= 0 && actualSimultaneouslly == 0)
			director.spawnerEnded ();
	}

	public void EnemyDead () 
	{
		actualSimultaneouslly -= 1;
	}
}
