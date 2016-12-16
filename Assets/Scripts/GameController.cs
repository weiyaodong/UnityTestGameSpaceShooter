using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject asteroid;

	public	float	start_wait_time		= 3f;
	public	float	spawn_wait_time		= 1f;
	public	float	wave_wait_time		= 5f;
	public	int		wave_count;

	const	float	default_asteroid_health		= 5.0f;
	const	float	default_asteroid_damage		= 5.0f;
	const	float	default_enemy_bolt_damage	= 1.0f;
	const	float	default_enemy_bolt_speed	= 4.5f;
	const	int		default_shooting_kind		= 0;
	const	int		default_contain				= 0;

	const	int		default_asteroid_moving_kind = 0;

	const	int		default_moving_kind = 0;
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

		


	}

	void set_asteroid(int asteroid_moving_kind = default_asteroid_moving_kind,int asteroid_shooting_kind = default_asteroid_shooting_kind)
	{
		
		switch(asteroid_shooting_kind)
		{
			default: 
				break;
			case SINGLE_SHOOTING_ASTEROID:
				if (asteroid_moving_kind == keep_moving_forward)
					set_up_a_asteroid();
				else if (asteroid_moving_kind == turn_up_and_stay_still)
					set_up_a_asteroid(turn_up_and_stay_still);
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

	void set_up_a_asteroid(	int moving_kind = default_moving_kind,
							float spawn_pos_x = -100f,
							float asteroid_health = default_asteroid_health,
							float asteroid_damage = default_asteroid_damage,
							float enemy_bolt_damage = default_enemy_bolt_damage,
							float enemy_bolt_speed = default_enemy_bolt_speed,
							int shooting_kind = default_shooting_kind,
							int contain = default_contain,
							bool tracing = default_trace)
	{

		if (spawn_pos_x == -100f)
		{
			spawn_pos_x = Random.Range(-spawn_value.x, spawn_value.x);
		}

		GameObject current_asteroid = Instantiate(asteroid, transform) as GameObject;

		current_asteroid.GetComponent<AsteroidController>()	.set_moving_kind	( moving_kind		);

		current_asteroid.GetComponent<AsteroidController>()	.set_total_health	( asteroid_health	);
		current_asteroid.GetComponent<AsteroidController>()	.set_damage			( asteroid_damage	);

		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_bolt_damage	( enemy_bolt_damage );
		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_bolt_speed		( enemy_bolt_speed	);
		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_shooting_kind	( shooting_kind		);		
		current_asteroid.GetComponent<CreateEnemyBolts>()	.set_trace			( tracing			);

		current_asteroid.GetComponent<AsteroidController>().set_contain(contain);
	}

	/*
	void spawn_a_asteroid()
	{
		Vector3 spawn_position = new Vector3(Random.Range(-spawn_value.x, spawn_value.x), spawn_value.y, spawn_value.z + 1);
		Instantiate(asteroid, spawn_position, Quaternion.identity);
	}
	*/
}
