using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShipController : MonoBehaviour
{
	private Rigidbody rb;
	public float thrust = 0;
	public float acceleration = 1;
	private float deceleration;

	public Text SpeedText;
	private float speed;
	private Vector3 lastPosition;

	public Text ThrottleText;
	private float throttle;

	public GameObject[] bullets;
	public GameObject barrel;
	public Vector3 bulletOffset = new Vector3(0, 0.76f, 13.68f);

	private float shootTimer;
	public float shootDelay = 0.5f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		deceleration = acceleration * 4;
		lastPosition = transform.position;
		shootTimer = 0;

		bullets = GameObject.FindGameObjectsWithTag("Bullet");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Flight();


		shootTimer += Time.deltaTime;

		//Calculate speed
		speed = (transform.position - lastPosition).magnitude * 100;
		//Update the speed text UI element
		SpeedText.text = "Speed: " + Mathf.Round(speed) +" Ku/s";
		//Redifine lastPosition
		lastPosition = transform.position;

		throttle = Mathf.Round(thrust * 2);
		ThrottleText.text = "Throttle: " + throttle + "%";

		if (Input.GetKey(KeyCode.Space) && shootTimer >= shootDelay)
		{
			Shoot();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//If the collision is an obstacle
		if (other.gameObject.CompareTag("Asteroid"))
		{
			//Disable the renderer in every child of the gameobject
			foreach (Renderer r in GetComponentsInChildren<Renderer>())
			{
				r.enabled = false;
			}
		}
	}

	void Shoot()
	{
		Vector3 spawnPos = barrel.transform.position + barrel.transform.forward * 5;
		for(int i = 0; i <= bullets.Length; i++)
		{
			if (bullets[i].activeSelf == false)
			{
				bullets[i].GetComponent<BulletController>().Spawn(spawnPos, gameObject.transform.rotation);
				break;
			}
		}
		shootTimer = 0;
	}

	void Flight()
	{
		//Speeding up
		if (Input.GetKey(KeyCode.LeftShift))
		{
			//Increase thrust
			thrust += acceleration;
			//Cap thrust at 50
			if (thrust > 50)
				thrust = 50;
		}

		//Slowing down
		if (Input.GetKey(KeyCode.LeftControl))
		{
			if (thrust == 0)
			{
				rb.drag = 0.5f;
			}
			//Lower the thrust
			if (thrust > 0)
			{
				if (!(thrust - deceleration < 0))
					thrust -= deceleration;
				else if (thrust - deceleration < 0)
				{
					thrust = 0;
				}
			}

		}

		//Turn throttle to 0% when x is pressed
		if (Input.GetKey(KeyCode.X))
		{
			thrust = 0;
		}

		//Turn throttle to 100% when z is pressed
		if (Input.GetKey(KeyCode.Z))
		{
			thrust = 50;
		}

		//Applying throttle
		rb.AddForce(transform.forward * thrust);

		if (!Input.GetKey(KeyCode.LeftControl))
		{
			rb.drag = 0.0f;
		}

		//Defining yaw, pitch and roll
		float yaw = Input.GetAxis("Horizontal");
		float pitch = Input.GetAxis("Vertical");
		float roll = 0;

		//Assigning value to roll
		if (Input.GetKey(KeyCode.Q))
		{
			roll = 1;
		}
		else if (Input.GetKey(KeyCode.E))
		{
			roll = -1;
		}

		//Applying yaw, pitch and roll values
		if (yaw > 0)
			rb.AddTorque(transform.up, ForceMode.Acceleration);
		if (yaw < 0)
			rb.AddTorque(-transform.up, ForceMode.Acceleration);
		if (pitch > 0)
			rb.AddTorque(transform.right, ForceMode.Acceleration);
		if (pitch < 0)
			rb.AddTorque(-transform.right, ForceMode.Acceleration);
		if (roll > 0)
			rb.AddTorque(transform.forward, ForceMode.Acceleration);
		if (roll < 0)
			rb.AddTorque(-transform.forward, ForceMode.Acceleration);
	}
}
