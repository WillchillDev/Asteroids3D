using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{

	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject asteroid4;

	public GameObject player;

	public int asteroidLimit = 300;
	public float spawnTime = 0.5f;

	private GameObject[] asteroids;
	private int asteroidCounter = 0;

	public GameObject bullet;
	public int bulletLimit = 10;

	void Start()
	{
		//Instantiate all asteroids
		for (int i = 0; i <= asteroidLimit; i++)
		{
			float asteroidToSpawn = Mathf.Round(Random.Range(1, 4));
			if (asteroidToSpawn == 1)
				Instantiate(asteroid1);
			if (asteroidToSpawn == 2)
				Instantiate(asteroid2);
			if (asteroidToSpawn == 3)
				Instantiate(asteroid3);
			if (asteroidToSpawn == 4)
				Instantiate(asteroid4);

		}
		asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		InvokeRepeating("SpawnAsteroid", spawnTime, spawnTime);

		//Instantiate all bullets
		for(int i = 0; i <= bulletLimit; i++)
		{
			Instantiate(bullet);
		}

	}

	void SpawnAsteroid()
	{
		//If the asteroid gameobject is not active
		if (!asteroids[asteroidCounter].activeSelf)
		{
			//Spawn the asteroid
			asteroids[asteroidCounter].GetComponent<AsteroidController>().Spawn();
		}
		asteroidCounter++;
		if (asteroidCounter >= asteroidLimit)
			asteroidCounter = 0;
	}
}
