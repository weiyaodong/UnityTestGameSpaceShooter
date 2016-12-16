using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public GameObject asteroid_explosion;

	private float	total_health;
	private float	asteroid_damage;
	private float	last_time;
	private float	rotate_speed			= 5f;
	private float	speed					= 0.5f;

	const	int		default_moving_kind		= 0;
	const	int		keep_moving_forward		= 0;
	const	int		turn_up_and_stay_still	= 1;
	const	int		cut_in_from_side		= 2;

	const	float	default_stay_start_time = 8;

	private int		moving_kind				= default_moving_kind;

	private int		contain;
	private bool	need_stay				= false;
	private float	stay_start_time			= default_stay_start_time;

	private float	current_time;

	public void set_speed(float _speed)
	{
		speed = _speed;
	}

	public float get_speed()
	{
		return speed;
	}

	public void set_moving_kind(int _moving_kind)
	{
		moving_kind = _moving_kind;
		work();
	}

	public int get_moving_kind()
	{
		return moving_kind;
	}
	
	public void set_contain(int _contain)
	{
		contain = _contain;
	}

	public int get_contain()
	{
		return contain;
	}


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
		if(total_health <= 0f)
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
		if (moving_kind == keep_moving_forward)
		{
			Vector3 direction = gameObject.GetComponent<Transform>().forward;
			gameObject.GetComponent<Rigidbody>().velocity = direction * (-speed);
		}
		else if(moving_kind == turn_up_and_stay_still)
		{
			need_stay = true;
			stay_start_time = default_stay_start_time;
			Vector3 direction = gameObject.GetComponent<Transform>().forward;
			gameObject.GetComponent<Rigidbody>().velocity = direction * (-speed);
		}
	}

	// Use this for initialization
	public void start () {

		set_damage();
		last_time = Time.time;
		set_total_health();
		//set_rotation();
		current_time = Time.time;
		work();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(need_stay && Time.time - current_time > stay_start_time)
		{
			need_stay = false;
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		}

	}
}
