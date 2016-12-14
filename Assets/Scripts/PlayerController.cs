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

public class PlayerController : MonoBehaviour {

	public AudioSource	shooting_sound;
	public GameObject	player_explosion;
	public GameObject	bolt;
	private float	default_rotate_angle	= 6f;

	private	float	default_bolt_damage		= 1f;
	private float	default_bolt_speed		= 10f;
	private float	default_health_point	= 5f;	
	private float	default_speed			= 6.0f;	
	private float	default_fire_rate		= 3.0f;

	private int		default_ultimate		= 2;

	private int		default_level			= 5;

	private float	health_per_level		= 2f;
	private float	fire_rate_per_level		= 1f;
	private float	speed_per_level			= 1f;
	private float	bolt_speed_per_level	= 3f;
	private float	damgae_per_level		= 1f;

	private float	speed;
	private float	fire_rate;
	private int		shooting_kind;	
	private float	health_point;
	private float	maximum_health_point;
	private int		ultimate;
	private float	bolt_damage;
	private float	bolt_speed;

	private int		shooting_level;
	private int		health_level;
	private int		speed_level;
	private int		fire_rate_level;
	private int		bolt_speed_level;
	private int		damage_level;

	public	int		maximum_level = 5;
	//public	int		maximum_ultimate = 5;

	private float last_time;
	public Boundary boundary;
	
		
	const int default_shooting_kind = 2;
	const int GRAPE_SHOOT			= 1;
	const int PRALLEL_SHOOT			= 2;

	const int SHOOTING_KIND			= 0;
	const int HEALTH_POINT			= 1; 
	const int FIRE_RATE				= 2;
	const int SHOOTING_SPEED		= 3;
	const int SPEED					= 4;
	const int SHOOTING				= 5;
	const int ULTIMATE				= 6;
	const int DAMAGE				= 7;
	const int MAXIMUM_HEALTH_POINT	= 8;


	
	void Start()
	{
		last_time = Time.time;
		
		health_level		=	default_level;
		shooting_level		=	default_level;
		fire_rate_level		=	default_level;
		speed_level			=	default_level;
		bolt_speed_level	=	default_level;
		damage_level		=	default_level;

		set_data();
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
	
	void generate_a_bolt(float angle = 0)
	{
		Vector3 current_position = transform.position;
		GameObject current_bolt = Instantiate(bolt, current_position, transform.rotation) as GameObject;

		current_bolt.GetComponent<BoltController>().set_bolt_damage(bolt_damage);
		current_bolt.GetComponent<BoltController>().set_bolt_kind(1);
		current_bolt.GetComponent<BoltController>().set_speed(bolt_speed);
		current_bolt.GetComponent<BoltController>().set_bolt(angle);
	}

	void generate_a_prallel_bolt(float delta_x = 0)
	{
		
		Vector3 current_position = transform.position;
		current_position.x += delta_x;
		GameObject current_bolt = Instantiate(bolt, current_position, transform.rotation) as GameObject;

		current_bolt.GetComponent<BoltController>().set_bolt_damage(bolt_damage);
		current_bolt.GetComponent<BoltController>().set_bolt_kind(2);
		current_bolt.GetComponent<BoltController>().set_speed(bolt_speed);
		current_bolt.GetComponent<BoltController>().set_bolt(delta_x);

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
		float total_length = 3.0f * (total_number) / (total_number + 4f);
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
			if(shooting_level == 0)
			{
				generate_a_bolt();
			}
			else if (shooting_kind == GRAPE_SHOOT)
			{
				//generate_a_prallel_bolt();
				int temp_level = shooting_level * 2 + 1;
				float total_angle = 80f * (temp_level) / (temp_level + 4f);
				generate_bolts_as_grape_shots(-0.5f * total_angle, 0.5f * total_angle, temp_level);
			}
			else if (shooting_kind == PRALLEL_SHOOT)
			{
				generate_bolts_as_prallel_shots((int)(shooting_level * 1.8f + 1));
			}
		}
	}

	

	void Update()
	{
		if (Input.GetKey(KeyCode.F))
		{

			shoot_bolts();
			
			//generate_a_bolt(0);
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
			health_point -= other.GetComponent<AsteroidController>().get_damage();
		}
		else if(other.tag == "enemy_bolt")
		{
			health_point -= other.GetComponent<EnemyBoltController>().get_damage();
		}
		else if(other.tag == "upgrade")
		{
			upgrade(other);
		}

		if(health_point < 0f)
		{
			Instantiate(player_explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	
	void set_data()
	{
		/*
		shooting_kind   =	default_shooting_kind;
		health_point    =	default_health_point;
		fire_rate       =	default_fire_rate;
		ultimate        =	default_ultimate;
		speed			=	default_speed;
		bolt_speed		=	default_bolt_speed;
		bolt_damage		=	default_bolt_damage;
		*/

		shooting_kind = default_shooting_kind;
		maximum_health_point	= health_level		* health_per_level		+ default_health_point;
		fire_rate				= fire_rate_level	* fire_rate_per_level	+ default_fire_rate;
		speed					= speed_level		* speed_per_level		+ default_speed;
		bolt_speed				= bolt_speed_level	* bolt_speed_per_level	+ default_bolt_speed;
		bolt_damage				= damage_level		* damgae_per_level		+ default_bolt_damage;
	}

	void upgrade(Collider other)
	{
		int upgrade_kind = other.GetComponent<UpgradeController>().get_upgrade_kind();
		switch(upgrade_kind)
		{
			case SPEED:
				if(speed_level != maximum_level)
				{
					speed_level++;
				}
				break;
			case SHOOTING:
				if(shooting_level != maximum_level)
				{
					shooting_level++;
				}
				break;
			case FIRE_RATE:
				if(fire_rate_level != maximum_level)
				{
					fire_rate_level++;
				}
				break;
			case DAMAGE:
				if(damage_level != maximum_level)
				{
					damage_level++;
				}
				break;
			case HEALTH_POINT:
				health_point += other.GetComponent<UpgradeController>().get_value();
				if (health_point > maximum_health_point)
					health_point = maximum_health_point;
				break;
			case ULTIMATE:
				if(ultimate != maximum_level)
				{
					ultimate++;
				}
				break;
			case SHOOTING_SPEED:
				if(bolt_speed_level != maximum_level)
				{
					bolt_speed_level++;
				}
				break;
			case MAXIMUM_HEALTH_POINT:
				if(health_level != maximum_level)
				{
					health_level++;
				}
				break;
			default:
				break;
		}

		set_data();
	}

}
