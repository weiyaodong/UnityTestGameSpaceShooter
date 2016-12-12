using UnityEngine;
using System.Collections;

public class BoltController : MonoBehaviour {

	private float bolt_damage = 1f;
	private float bolt_speed = 10;
	private int bolt_kind = 1;
	private float parameter = 0;

	public void set_bolt_damage(float set_damage = 1f)
	{
		bolt_damage = set_damage;
	}

	public float get_damage()
	{
		return bolt_damage;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "asteroid")
		{
			Destroy(gameObject);
		}
	}
	
	public void set_speed(float _speed)
	{
		bolt_speed = _speed;
	}

	public void set_bolt_kind(int set_kind)
	{
		switch(set_kind)
		{
			case 1:
				bolt_kind = 1;
				break;
			case 2:
				bolt_kind = 2;
				break;
			case 3:
				bolt_kind = 3;
				break;
			default:
				return;
		}
	}

	public void set_bolt(float var)
	{
		parameter = var;
		if (bolt_kind == 1)
		{
			float angle = var;
			float default_speed = bolt_speed;

			Vector3 direction = gameObject.GetComponent<Transform>().forward;

			float temp_x = direction.x;
			float temp_z = direction.z;

			direction.y = 0;
			direction.x = temp_x * Mathf.Cos(angle * Mathf.PI / 180f) - temp_z * Mathf.Sin(angle * Mathf.PI / 180f);
			direction.z = temp_x * Mathf.Sin(angle * Mathf.PI / 180f) + temp_z * Mathf.Cos(angle * Mathf.PI / 180f);

			gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0f, -angle, 0f);
			gameObject.GetComponent<Rigidbody>().velocity = direction * default_speed;
		}
		else if(bolt_kind == 2)
		{
			float delta_x = var;
			Vector3 current_position = transform.position;

			current_position.x += delta_x;
			float default_speed = bolt_speed;

			Vector3 direction = gameObject.GetComponent<Transform>().forward;
			gameObject.GetComponent<Rigidbody>().velocity = direction * default_speed;
		}
	}
}
