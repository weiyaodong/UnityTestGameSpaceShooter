using UnityEngine;
using System.Collections;

public class EnemyBoltDestroyed : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
