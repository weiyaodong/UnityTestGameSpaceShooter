using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float zMin;
	public float xMin;
	public float zMax;
	public float xMax;
}

public class ControlPlayer : MonoBehaviour {

	public GameObject player_explosion;
	public GameObject bolt;
	public GameObject left_bolt;
	public GameObject right_bolt;
	
	public float default_health_point = 10;

	public float default_asteroid_damage = 4;

	public Transform shot_spawn;
	public Transform left_shot_spawn;
	public Transform right_shot_spawn;

	public float default_speed = 3.0f;
	public float rotate_angle = 0.00000f;
	public float fire_rate = 2f;

	private float last_time;
	private float current_health_point;

	public Boundary boundary;
	

	
	public int shooting_kind = triple_shooting;

	const int default_shooting_kind = 0;
	const int double_shooting = 1;
	const int triple_shooting = 2;
	
	
	void Start()
	{
		last_time = Time.time;
		current_health_point = default_health_point;
	}

	void Movement()
	{
		float move_horizontal = Input.GetAxis("Horizontal");
		float move_vertical = Input.GetAxis("Vertical");

		float temp_var = 1.0f;

		if (Input.GetKey(KeyCode.LeftShift))
			temp_var *= 0.5f;

		Vector3 movement = new Vector3(move_horizontal, 0.0f, move_vertical) * temp_var;

		gameObject.GetComponent<Rigidbody>()
				  .velocity = movement * default_speed;
		gameObject.GetComponent<Rigidbody>()
				  .position = new Vector3
				  (
					  Mathf.Clamp(gameObject.GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
					  0.0f,
					  Mathf.Clamp(gameObject.GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
				  );
		//gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0f, 0f, 20f);
		gameObject.GetComponent<Rigidbody>().rotation =
			Quaternion.Euler(0.0f, 0.0f, gameObject.GetComponent<Rigidbody>().velocity.x * -rotate_angle);

	}

	void generate_bolts(Vector3 disturb_vector, int kind = 0)
	{
		if (kind == 0)
		{
			Vector3 start_position = shot_spawn.position + disturb_vector;
			GameObject current_bolt = Instantiate(bolt, start_position, shot_spawn.rotation) as GameObject;
		}
		else if (kind == 1)
		{
			Vector3 start_position = left_shot_spawn.position + disturb_vector;
			GameObject current_bolt = Instantiate(left_bolt, start_position, shot_spawn.rotation) as GameObject;
		}
		else if (kind == 2)
		{
			Vector3 start_position = right_shot_spawn.position + disturb_vector;
			GameObject current_bolt = Instantiate(right_bolt, start_position, shot_spawn.rotation) as GameObject;
		}
	}

	void shoot_bolts(int kind)
	{
		float ping_time = 1.0f / fire_rate;
		if (kind == 0)
		{
			if (Time.time - last_time > ping_time)
			{
				last_time = Time.time;
				generate_bolts(new Vector3(0, 0, 0));
			}
		}
		else if (kind == 1)
		{
			if (Time.time - last_time > ping_time)
			{
				last_time = Time.time;
				generate_bolts(new Vector3(-0.2f, 0, 0));
				generate_bolts(new Vector3(0.2f, 0, 0));
			}
		}
		else if (kind == 2)
		{
			if (Time.time - last_time > ping_time)
			{
				last_time = Time.time;
				generate_bolts(new Vector3(-0.3f, 0, 0), 1);
				generate_bolts(new Vector3(0.3f, 0, 0), 2);
				generate_bolts(new Vector3(0.0f, 0, 0), 0);
			}
		}

	}

	void Update()
	{
		if (Input.GetKey(KeyCode.F))
		{
			shoot_bolts(shooting_kind);
		}
	}

	void FixedUpdate()
	{
		Movement();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "asteroid")
		{
			current_health_point -= default_asteroid_damage;
		}

		if(current_health_point < 0f)
		{
			Instantiate(player_explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	
}
