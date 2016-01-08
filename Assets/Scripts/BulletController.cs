using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private Rigidbody rb;
	private GameObject player;
	private AudioSource shoot;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player");

		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		rb.AddForce(transform.forward * 500);

		if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 10000)
		{
			DeSpawn();
		}
	}

	public void Spawn(Vector3 position, Quaternion rotation)
	{
		gameObject.SetActive(true);

		transform.position = position;
		transform.rotation = rotation;

		AudioSource shoot = GetComponent<AudioSource>();
		shoot.Play();

		rb.velocity = player.GetComponent<Rigidbody>().velocity;
		rb.angularVelocity = new Vector3(0, 0, 0);
		rb.AddForce(transform.forward * 2000, ForceMode.Acceleration);
	}

	void DeSpawn()
	{
		gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Asteroid"))
		{
			other.gameObject.GetComponent<AsteroidController>().DeSpawn();
			DeSpawn();
		}
	}
}
