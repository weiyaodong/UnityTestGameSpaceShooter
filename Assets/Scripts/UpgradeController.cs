using UnityEngine;
using System.Collections;

public class UpgradeController : MonoBehaviour {

	public float left_boundary;
	public float right_boundary;
	public float up_boundary;
	public float down_boundary;

	private int upgrade_kind;
	private float value = 0;
	private int max_crash_time = 16;
	private int crash_time;

	public int get_upgrade_kind()
	{
		return upgrade_kind;
	}

	public void set_upgrade_kind(int _upgrade_kind)
	{
		upgrade_kind = _upgrade_kind;
	}

	public float get_value()
	{
		return value;
	}

	public void set_value(float _value)
	{
		value = _value;
	}

	void initialize()
	{
		Vector3 current_speed = transform.forward;

		float angle = Random.Range(-45f, 45f);
		float speed = Random.Range(4f, 5f);

		current_speed *= speed;

		float temp_x = current_speed.x;
		float temp_z = current_speed.z;

		

		current_speed.x = temp_x * Mathf.Cos(angle * Mathf.PI / 180f) - temp_z * Mathf.Sin(angle * Mathf.PI / 180f);
		current_speed.z = temp_x * Mathf.Sin(angle * Mathf.PI / 180f) + temp_z * Mathf.Cos(angle * Mathf.PI / 180f);

		current_speed.x += Random.Range(2f, 3f);

		gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0f, -angle, 0f);
		gameObject.GetComponent<Rigidbody>().velocity = current_speed;
	}

	// Use this for initialization
	void Start () {

		initialize();

	}

	void check_position()
	{
		Vector3 temp_position = transform.position;
		Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;

		if(temp_position.x < left_boundary || temp_position.x > right_boundary)
		{
			v.x *= -1f;
			crash_time++;
		}
		if(temp_position.z < down_boundary || temp_position.z > up_boundary)
		{
			v.z *= -1f;
			crash_time++;
		}

		gameObject.GetComponent<Rigidbody>().velocity = v;

		if(crash_time >= max_crash_time)
		{
			Destroy(gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {

		check_position();

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
