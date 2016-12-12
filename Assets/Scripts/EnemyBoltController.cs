using UnityEngine;
using System.Collections;

public class EnemyBoltController : MonoBehaviour {

	private float enemy_bolt_damage;

	public float get_damage()
	{
		return enemy_bolt_damage;
	}

	public void set_damage(float _damage)
	{
		enemy_bolt_damage = _damage;
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}

}
