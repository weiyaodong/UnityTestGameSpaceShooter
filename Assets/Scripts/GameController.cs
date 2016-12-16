using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject asteroid;

	private	float	start_wait_time		= 3f;
	private	float	spawn_wait_time		= 1f;
	private	float	wave_wait_time		= 5f;
	private	int		wave_count;

	const	float	default_asteroid_health			= 5.0f;
	const	float	default_asteroid_damage			= 5.0f;
	const	float	default_enemy_bolt_damage		= 1.0f;
	const	float	default_enemy_bolt_speed		= 6f;
	const	int		default_shooting_kind			= 0;
	const	int		default_contain					= 0;
	const	float	default_asteroid_moving_speed	= 0.7f;

	const	int		default_asteroid_moving_kind	= 0;

	const	int		default_moving_kind				= 0;

	const	int		keep_moving_forward			= 0;
	const	int		turn_up_and_stay_still		= 1;

	const	int		default_asteroid_shooting_kind = 0;

	const	int		SINGLE_SHOOTING_ASTEROID	= 0;
	const	int		DOUBLE_SHOOTING_ASTEROID	= 1;
	const	int		TRIPLE_SHOOTING_ASTEROID	= 2;
	const	int		QUAL_SHOOTING_ASTEROID		= 3;
	const	int		ROUND_SHOOTING_ASTEROID		= 4;
	const	int		PRALLEL_SHOOTING_ASTEROID	= 5;

	const bool default_trace = true;


	public Vector3 spawn_value;


	void Start ()
	{
		StartCoroutine(create_waves_of_harzard());
	}
	
	IEnumerator create_waves_of_harzard()
	{
		/*
		int total_counter = 0;

		for (total_counter = 1; ; total_counter++)
		{
			int current_counter = wave_count +(int)(0.5f*total_counter);
			for (int i = 0; i < current_counter; i++) 
			{
				spawn_a_harzard();
				yield return new WaitForSeconds(spawn_wait_time);
			}

			float current_wave_wait_time = wave_wait_time + 0.2f * total_counter;

			yield return new WaitForSeconds(current_wave_wait_time);

		}
		*/

		// test level 1:
		yield return new WaitForSeconds(start_wait_time);

		set_up_a_asteroid(-4f, keep_moving_forward, SINGLE_SHOOTING_ASTEROID, 8f, 1f, 2f);
		set_up_a_asteroid(4f, keep_moving_forward, SINGLE_SHOOTING_ASTEROID, 8f, 1f, 2f);
		set_up_a_asteroid(0f, keep_moving_forward, SINGLE_SHOOTING_ASTEROID, 8f, 1f, 2f);

		yield return new WaitForSeconds(8f);

		set_up_a_asteroid(-3f, turn_up_and_stay_still, TRIPLE_SHOOTING_ASTEROID, 6f, 0.5f ,4f);
		set_up_a_asteroid( 3f, turn_up_and_stay_still, TRIPLE_SHOOTING_ASTEROID, 6f, 0.5f ,4f);

		yield return new WaitForSeconds(4f);

		for (int i = 0; i <= 5; i++)
		{
			set_up_a_asteroid(-100f, keep_moving_forward, SINGLE_SHOOTING_ASTEROID, 6f, 1f, 1f);
			yield return new WaitForSeconds(2f);
		}


	}

	void set_asteroid(	float spawn_pos_x = -100f,
						int asteroid_moving_kind = default_asteroid_moving_kind,
						int asteroid_shooting_kind = default_asteroid_shooting_kind
						)
	{
		
		switch(asteroid_shooting_kind)
		{
			default: 
				break;
			case SINGLE_SHOOTING_ASTEROID:
				if (asteroid_moving_kind == keep_moving_forward)
					set_up_a_asteroid(spawn_pos_x, keep_moving_forward, asteroid_shooting_kind);
				else if (asteroid_moving_kind == turn_up_and_stay_still)
					set_up_a_asteroid(spawn_pos_x, turn_up_and_stay_still, asteroid_shooting_kind);
				else if (asteroid_moving_kind == 2)
					;

				break;
			case DOUBLE_SHOOTING_ASTEROID:
				break;
			case TRIPLE_SHOOTING_ASTEROID:
				break;
			case QUAL_SHOOTING_ASTEROID:
				break;
			case ROUND_SHOOTING_ASTEROID:
				break;
			case PRALLEL_SHOOTING_ASTEROID:
				break;
		}
	}

	void set_up_a_asteroid(	float	spawn_pos_x				= -100f,
							int		moving_kind				= default_moving_kind,							
							int		shooting_kind			= default_shooting_kind,
							float	enemy_bolt_speed		= default_enemy_bolt_speed,
							float	asteroid_moving_speed	= default_asteroid_moving_speed,
							float	asteroid_health			= default_asteroid_health,
							float	asteroid_damage			= default_asteroid_damage,
							float	enemy_bolt_damage		= default_enemy_bolt_damage,
							int		contain					= default_contain,
							bool	tracing					= default_trace)
	{

		if (spawn_pos_x == -100f)
		{
			spawn_pos_x = Random.Range(-spawn_value.x, spawn_value.x);
		}

		GameObject current_asteroid = Instantiate(asteroid, transform) as GameObject;

		Vector3 temp_vector = new Vector3(spawn_pos_x, 0, spawn_value.z);

		current_asteroid.GetComponent<Transform>().position = temp_vector;

		current_asteroid.GetComponent<AsteroidController>()	.set_moving_kind	( moving_kind			);
		current_asteroid.GetComponent<AsteroidController>()	.set_total_health	( asteroid_health		);
		current_asteroid.GetComponent<AsteroidController>()	.set_damage			( asteroid_damage		);
		current_asteroid.GetComponent<AsteroidController>()	.set_speed			( asteroid_moving_speed );

		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_bolt_damage	( enemy_bolt_damage		);
		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_bolt_speed		( enemy_bolt_speed		);
		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_shooting_kind	( shooting_kind			);		
		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_trace			( tracing				);

		current_asteroid.GetComponent<AsteroidController>().set_contain(contain);

		current_asteroid.GetComponent<CreateEnemyBolts>().start();
		current_asteroid.GetComponent<AsteroidController>().start();
	}

	/*
	void spawn_a_asteroid()
	{
		Vector3 spawn_position = new Vector3(Random.Range(-spawn_value.x, spawn_value.x), spawn_value.y, spawn_value.z + 1);
		Instantiate(asteroid, spawn_position, Quaternion.identity);
	}
	*/
}
