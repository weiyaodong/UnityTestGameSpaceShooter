using UnityEngine;
using System.Collections;

public class CreateEnemyBolts : MonoBehaviour {

	public	GameObject	enemy_bolt;
	private	GameObject	Player;
	public	GameObject	container;

	private float	enemy_bolt_speed	= 4f;
	private float	enemy_bolt_damage	= 1f;

	private int		total_bolts			= 20;
	private float	shooting_rate		= 0.7f;

	private float	start_wait_time		= 1f;
	private float	wave_wait_time		= 4f;
	private int		wave_count			= 5;

	private int		shooting_kind;

	private int		default_shooting_kind = 2;

	const	int		SINGLE_SHOOT		= 0;
	const	int		DOUBLE_SHOOT		= 1;
	const	int		TRIPLE_SHOOT		= 2;
	const	int		QUAL_SHOOT			= 3;
	const	int		ROUND_SHOOT			= 4;
	const	int		PRALLEL_SHOOT		= 5;

	private bool	tracing				= true;
	private int		shooting_num		= 15;

	private float shooting_delay;

	public void set_bolt_damage(float _damage = 1f)
	{
		enemy_bolt_damage = _damage;
	}

	public float get_bolt_damage()
	{
		return enemy_bolt_damage;
	}

	public void set_bolt_speed(float _speed)
	{
		enemy_bolt_speed = _speed;
	}

	public float get_bolt_speed()
	{
		return enemy_bolt_speed;
	}

	public void set_shooting_kind(int _shooting_kind)
	{
		shooting_kind = _shooting_kind;
	}

	public int get_shooting_kind()
	{
		return shooting_kind;
	}

	public void set_trace(bool _tracing)
	{
		tracing = _tracing;
	}

	public bool get_trace()
	{
		return tracing;
	}

	public void set_shooting_num(int _num = 15)
	{
		shooting_kind = 4;
		shooting_num = _num;
	}

	public int get_shooting_num()
	{
		return shooting_num;
	}

	void Start()
	{
		shooting_kind = default_shooting_kind;
		Player = GameObject.FindWithTag("Player");
		create();
		
	}

	void create()
	{
		shooting_delay = 1.0f / shooting_rate;
		StartCoroutine(create_bolts());
	}

	IEnumerator create_bolts()
	{
		yield return new WaitForSeconds(start_wait_time);

		while (true)
		{

			for (int i = 0; i < wave_count; i++)
			{
				generate_enemy_bolts();
				yield return new WaitForSeconds(shooting_delay);
			}

			yield return new WaitForSeconds(wave_wait_time);

		}

	}
	void generate_enemy_bolts()
	{
		Vector3 basic_direction = -transform.forward;
		float start_angle = 0f;		
		if (tracing)
		{
			basic_direction = Player.transform.position - transform.position;
			start_angle = Mathf.Atan(-basic_direction.x/basic_direction.z) * 180f / Mathf.PI;
			if(Player.transform.position.z > transform.position.z)
			{
				start_angle += 180f;
			}
		}
		if(shooting_kind == SINGLE_SHOOT)
		{	
			generate_a_enemy_bolt(start_angle);
		}
		else if(shooting_kind == DOUBLE_SHOOT)
		{
			generate_a_enemy_bolt(start_angle + 10);
			generate_a_enemy_bolt(start_angle - 10);
		}
		else if(shooting_kind == TRIPLE_SHOOT)
		{
			generate_a_enemy_bolt(start_angle + 7);
			generate_a_enemy_bolt(start_angle + 0);
			generate_a_enemy_bolt(start_angle - 7);
		}
		else if(shooting_kind == QUAL_SHOOT)
		{
			generate_a_enemy_bolt(start_angle + 12);
			generate_a_enemy_bolt(start_angle + 4);
			generate_a_enemy_bolt(start_angle - 4);
			generate_a_enemy_bolt(start_angle - 12);
		}
		else if(shooting_kind == ROUND_SHOOT)
		{
			int actual_num = shooting_num + Random.Range(10, 15);
			float gap = 360f / actual_num;
			for (int i = 0; i < actual_num; i++)
			{
				generate_a_enemy_bolt(start_angle + gap * i);
			}
		}
		else if(shooting_kind == PRALLEL_SHOOT)
		{
			int actual_num = Random.Range(3, 6);
			float total_length = (actual_num + 2f) / (actual_num + 4f) * 1.5f;
			float gap = total_length / (actual_num - 1f);
			for (int i = 0; i < actual_num; i++)
			{
				generate_a_enemy_bolt(total_length * (-0.5f) + gap * i, 2);
			}
		}
	}
	void generate_a_enemy_bolt(float parameter = 0, int kind = 1)
	{
		Vector3 current_position = transform.position;
		GameObject current_bolt = Instantiate(enemy_bolt, current_position, transform.rotation) as GameObject;

		if (kind == 1)
		{
			//nothing need to be done
		}
		else if (kind == 2)
		{
			current_position.x += parameter;
		}
		current_bolt.GetComponent<EnemyBoltController>().set_bolt_damage(enemy_bolt_damage);
		current_bolt.GetComponent<EnemyBoltController>().set_bolt_kind(kind);
		current_bolt.GetComponent<EnemyBoltController>().set_speed(enemy_bolt_speed);
		current_bolt.GetComponent<EnemyBoltController>().set_bolt(parameter);
	}



}
