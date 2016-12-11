using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	void Start()
	{
		gameObject.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
	}
}
