using UnityEngine;
using System.Collections;

public class CreateEnemyBolts : MonoBehaviour {

	public GameObject enemy_bolt;
	private float enemy_bolt_speed = 5;
	
	public int total_bolts = 20;
	public float shooting_rate = 0.7f;

	private float start_wait_time = 1f;
	public float wave_wait_time = 4f;
	public int wave_count = 5;

	private float shooting_delay;

	void Start()
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
				generate_enemy_bolts(0f, 360f, total_bolts);

				yield return new WaitForSeconds(shooting_delay);
			}

			yield return new WaitForSeconds(wave_wait_time);

		}

	}

	void generate_enemy_bolts(float start_angle, float end_angle, int total_number)
	{
		float differ = (end_angle - start_angle) / (float)total_number ;

		for (int i = 0; i < total_number; i++)
		{
			generate_a_enemy_bolt(start_angle + (float)i * differ);
		}
	}

	void generate_a_enemy_bolt(float angle)
	{
		GameObject current_enemy_bolt = Instantiate(enemy_bolt, transform.position,transform.rotation) as GameObject;

		float default_speed = enemy_bolt_speed;

		Vector3 direction = gameObject.GetComponent<Transform>().forward;

		float temp_x = direction.x;
		float temp_z = direction.z;

		direction.y = 0;
		direction.x = temp_x * Mathf.Cos(angle * Mathf.PI / 180f) - temp_z * Mathf.Sin(angle * Mathf.PI / 180f);
		direction.z = temp_x * Mathf.Sin(angle * Mathf.PI / 180f) + temp_z * Mathf.Cos(angle * Mathf.PI / 180f);

		current_enemy_bolt.GetComponent<Transform>().eulerAngles = new Vector3(0f, -angle, 0f);
		current_enemy_bolt.GetComponent<Rigidbody>().velocity = direction * default_speed;
	}


}
