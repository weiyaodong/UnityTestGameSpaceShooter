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

	public AudioSource	shooting_sound;
	public GameObject	player_explosion;
	public GameObject	bolt;

	public	float	default_asteroid_damage = 4f;
	public	float	default_bolt_damage = 1f;
	private float	default_bolt_speed = 20f;

	private float	default_health_point = 10f;	
	private float	default_speed = 3.0f;
	private float	default_rotate_angle = 6f;
	private float	default_fire_rate = 2.0f;
	private int		default_level = 8;
	private int		default_ultimate = 2;

	private float	speed;
	private float	fire_rate;
	private int		shooting_kind;	
	private float	health_point;
	private int		ultimate;

	private int		shooting_level;
	private int		health_level;
	private int		speed_level;
	private int		fire_rate_level;
	private int		shooting_speed_level;
	private int		ultimate_level;
	private int		damage_level;

	public	int		maximum_level = 8;
	public	int		maximum_ultimate = 5;

	private float last_time;
	public Boundary boundary;
	
		
	const int default_shooting_kind = 1;
	const int grape_shoot = 1;
	const int prallel_shoot = 2;

	const int SHOOTING_KIND		= 0;
	const int HEALTH_POINT		= 1; 
	const int FIRE_RATE			= 2;
	const int SHOOTING_SPEED	= 3;
	const int SPEED				= 4;
	const int SHOOTING			= 5;
	const int ULTIMATE			= 6;
	const int DAMAGE			= 7;


	
	void Start()
	{
		last_time = Time.time;

		shooting_kind   =	default_shooting_kind;
		health_point    =	default_health_point;
		fire_rate       =	default_fire_rate;
		ultimate        =	default_ultimate;
		speed			=	default_speed;
		
		health_level			= default_level;
		shooting_level			= default_level;
		fire_rate_level			= default_level;
		speed_level				= default_level;
		ultimate_level			= default_level;
		shooting_speed_level	= default_level;
		damage_level			= default_level;

		
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
		gameObject.GetComponent<Rigidbody>().rotation =
			Quaternion.Euler(0.0f, 0.0f, gameObject.GetComponent<Rigidbody>().velocity.x * -default_rotate_angle);

	}
	
	void generate_a_bolt(float angle)
	{
		GameObject current_bolt = Instantiate(bolt, transform.position, transform.rotation) as GameObject;

		float default_speed = default_bolt_speed;

		Vector3 direction = gameObject.GetComponent<Transform>().forward;

		float temp_x = direction.x;
		float temp_z = direction.z;

		direction.y = 0;
		direction.x = temp_x * Mathf.Cos(angle * Mathf.PI / 180f) - temp_z * Mathf.Sin(angle * Mathf.PI / 180f);
		direction.z = temp_x * Mathf.Sin(angle * Mathf.PI / 180f) + temp_z * Mathf.Cos(angle * Mathf.PI / 180f);

		current_bolt.GetComponent<Transform>().eulerAngles = new Vector3(0f, -angle, 0f);
		current_bolt.GetComponent<Rigidbody>().velocity = direction * default_speed;
	}

	void generate_a_prallel_bolt(float delta_x = 0)
	{
		Vector3 current_position = transform.position;

		current_position.x += delta_x;

		GameObject current_bolt = Instantiate(bolt, current_position, transform.rotation) as GameObject;

		float default_speed = default_bolt_speed;

		Vector3 direction = gameObject.GetComponent<Transform>().forward;
		current_bolt.GetComponent<Rigidbody>().velocity = direction * default_speed;
	}

	void generate_bolts_as_grape_shots(float start_angle, float end_angle, int total_number)
	{
		float differ = (end_angle - start_angle) / (total_number - 1f);

		for (int i = 0; i < total_number; i++)
		{
			generate_a_bolt(start_angle + (float)i * differ);
		}
	}

	void generate_bolts_as_prallel_shots(int total_number)
	{
		if (total_number == 1)
		{
			generate_a_prallel_bolt(0);
			return;
		}
		float total_length = 1.2f * (total_number) / (total_number + 1f);
		float start_pos = -total_length / 2.0f;
		float delta_x = total_length / (total_number - 1);

		for (int i = 0; i < total_number; i++)
		{
			generate_a_prallel_bolt(start_pos + i * delta_x);
		}

	}

	void shoot_bolts()
	{
		float ping_time = 1.0f / fire_rate;
		if (Time.time - last_time > ping_time)
		{
			last_time = Time.time;
			//shooting_sound.Play();
			if (shooting_kind == grape_shoot)
			{
				//generate_a_prallel_bolt();
				int temp_level = shooting_level * 2 - 1;
				float total_angle = 80f * (temp_level) / (temp_level + 1f);
				generate_bolts_as_grape_shots(-0.5f * total_angle, 0.5f * total_angle, temp_level);
			}
			else if (shooting_kind == prallel_shoot)
			{
				generate_bolts_as_prallel_shots(shooting_level);
			}
		}
	}

	

	void Update()
	{
		//if (Input.GetKey(KeyCode.F))
		{
			shoot_bolts();
			
			//generate_a_prallel_bolt();
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
			health_point -= default_asteroid_damage;
		}
		else if(other.tag == "enemy_bolt")
		{
			health_point -= default_bolt_damage;
		}
		else if(other.tag == "speed_upgrade")
		{
			//upgrade(SPEED);
		}

		if(health_point < 0f)
		{
			Instantiate(player_explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	/*
	void upgrade(int upgrade_kind)
	{
		switch(upgrade_kind)
		{
			case SPEED:
				if(speed_level != maximum_level)
				{
					speed_level++;
					speed += 1.5f;
				}
				break;
			case SHOOTING:
				if(shooting_level != maximum_level)
				{
					shooting_level++;
				}
				break;
			/*case FIRE_RATE:
				if()
		}
	}
*/
}
