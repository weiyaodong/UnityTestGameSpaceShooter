using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	void Start()
	{
		Vector3 random_angular_velocity = Random.insideUnitSphere * tumble;
		random_angular_velocity.y = 0f;
		random_angular_velocity.z = 0f;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
	}
}
