using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float destroy_delay = 2f;

	void Start()
	{
		Destroy(gameObject, destroy_delay);
	}
}
