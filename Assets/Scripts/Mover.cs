using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float default_bolt_speed = 20;
	public float default_asteroid_speed = -3;
	public float angle = 0;

	void Start()
	{
		float default_speed = 0;

		if (gameObject.tag == "bolt")
			default_speed = default_bolt_speed;
		else if (gameObject.tag == "asteroid")
			default_speed = default_asteroid_speed;

		Vector3 direction = gameObject.GetComponent<Transform>().forward;

		float temp_x = direction.x;
		float temp_z = direction.z;

		
		direction.x = temp_x * Mathf.Cos(angle * Mathf.PI / 180f) - temp_z * Mathf.Sin(angle * Mathf.PI / 180f);
		direction.z = temp_x * Mathf.Sin(angle * Mathf.PI / 180f) + temp_z * Mathf.Cos(angle * Mathf.PI / 180f);

		gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0f, -angle, 0f);
		gameObject.GetComponent<Rigidbody>().velocity = direction * default_speed;
	}

	void Update()
	{
		
	}
}
