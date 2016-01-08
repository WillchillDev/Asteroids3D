using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	private Rigidbody rb;
	private GameObject player;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

		rb = GetComponent<Rigidbody>();

		gameObject.SetActive(false);
	}

	void Update()
	{
		if(Vector3.Distance(gameObject.transform.position, player.transform.position) > 50000){
			DeSpawn();
		}
	}

	public void Spawn()
	{
		gameObject.SetActive(true);
		Vector3 newPosition = new Vector3(player.transform.position.x + GenerateOffset(1000f, 5000f), player.transform.position.y + GenerateOffset(1000f, 5000f), player.transform.position.z + GenerateOffset(1000f, 5000f));
		gameObject.transform.position = newPosition;
		Initialise();
	}

	public void DeSpawn()
	{
		gameObject.SetActive(false);
		Instantiate(explosion, transform.position, transform.rotation);
	}

	float GenerateOffset(float min, float max)
	{
		if (Random.value >= 0.5)
		{
			//Return positive value
			return Random.Range(min, max);
		}
		else
		{
			//Return negative value
			return Random.Range(min, max) * -1;
		}
	}

	void Initialise()
	{
		//Random scale
		float scale = Random.Range(5, 500);
		transform.localScale = new Vector3(scale, scale, scale);

		//Make asteroid spin randomly
		Vector3 randomRotate = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
		rb.AddTorque(randomRotate, ForceMode.Force);

		//Make asteroid fly in a random direction
		Vector3 randomForce = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
		rb.AddForce(randomForce * Random.Range(1f,50f), ForceMode.Force);
	}
}
