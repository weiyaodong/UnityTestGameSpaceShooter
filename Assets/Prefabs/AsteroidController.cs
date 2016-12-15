using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public GameObject asteroid_explosion;

	private float total_health;
	private float asteroid_damage;
	private float last_time;
	private float rotate_speed = 5f;
	private float speed = 1f;
	

	public void set_total_health(float set_health = 3)
	{
		total_health = set_health;
	}

	public float get_total_health()
	{
		return total_health;
	}

	public void set_damage(float set_damage = 4)
	{
		asteroid_damage = set_damage;
	}

	public float get_damage()
	{
		return asteroid_damage;
	}

	void destroy()
	{
		Instantiate(asteroid_explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	private void check_health()
	{
		if(total_health < 0f)
		{
			destroy();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "bolt")
		{
			total_health -= other.GetComponent<BoltController>().get_damage();
		}
		else if(other.tag == "Player")
		{
			destroy();
			//Destroy(gameObject);
		}
		check_health();
	}
	
	void set_rotation()
	{
		Vector3 random_angular_velocity = Random.insideUnitSphere * rotate_speed;
		random_angular_velocity.y = 0f;
		random_angular_velocity.z = 0f;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotate_speed;
	}


	void work()
	{
		Vector3 direction = gameObject.GetComponent<Transform>().forward;
		gameObject.GetComponent<Rigidbody>().velocity = direction * (-speed);
	}

	// Use this for initialization
	void Start () {

		set_damage(); last_time = Time.time;
		set_total_health();
		//set_rotation();
		work();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
