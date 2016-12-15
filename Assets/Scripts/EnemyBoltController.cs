using UnityEngine;
using System.Collections;

public class EnemyBoltController : MonoBehaviour {

	private float enemy_bolt_damage;
	private float bolt_speed;
	private float parameter;
	private int bolt_kind;

	public void set_speed(float _speed)
	{
		bolt_speed = _speed;
	}

	public float get_speed()
	{
		return bolt_speed;
	}

	public float get_damage()
	{
		return enemy_bolt_damage;
	}

	public void set_bolt_damage(float _damage)
	{
		enemy_bolt_damage = _damage;
	}

	public void set_bolt_kind(int set_kind)
	{
		switch (set_kind)
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

			Vector3 direction = -gameObject.GetComponent<Transform>().forward;

			float temp_x = direction.x;
			float temp_z = direction.z;

			direction.y = 0;
			direction.x = temp_x * Mathf.Cos(angle * Mathf.PI / 180f) - temp_z * Mathf.Sin(angle * Mathf.PI / 180f);
			direction.z = temp_x * Mathf.Sin(angle * Mathf.PI / 180f) + temp_z * Mathf.Cos(angle * Mathf.PI / 180f);

			gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0f, -angle, 0f);
			gameObject.GetComponent<Rigidbody>().velocity = direction * default_speed;
		}
		else if (bolt_kind == 2)
		{
			float delta_x = var;
			Vector3 current_position = transform.position;

			current_position.x += delta_x;
			float default_speed = bolt_speed;

			Vector3 direction = -gameObject.GetComponent<Transform>().forward;
			gameObject.GetComponent<Transform>().position = current_position;
			gameObject.GetComponent<Rigidbody>().velocity = direction * default_speed;
		}
	}

	/*
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
	*/
}
