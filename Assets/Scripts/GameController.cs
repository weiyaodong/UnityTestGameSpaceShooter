using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;

	public float start_wait_time	= 3f;
	public float spawn_wait_time	= 1f;
	public float wave_wait_time		= 5f;
	public int wave_count;

	public Vector3 spawn_value;

	// Use this for initialization
	void Start () {
		StartCoroutine(create_waves_of_harzard());
	}
	
	IEnumerator create_waves_of_harzard()
	{
		yield return new WaitForSeconds(start_wait_time);

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
		
	}

	void spawn_a_harzard()
	{
		Vector3 spawn_position = new Vector3(Random.Range(-spawn_value.x, spawn_value.x), spawn_value.y, spawn_value.z + 1);
		Instantiate(hazard, spawn_position, Quaternion.identity);
	}
}
