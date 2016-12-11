using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;

	public Vector3 spawn_value;

	// Use this for initialization
	void Start () {
		spawn_a_harzard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawn_a_harzard()
	{
		Vector3 spawn_position = new Vector3(Random.Range(-spawn_value.x, spawn_value.x), spawn_value.y, spawn_value.z);
		Instantiate(hazard, spawn_position, Quaternion.identity);
	}
}
