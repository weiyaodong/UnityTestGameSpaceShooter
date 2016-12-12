using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public float total_health_point = 20;
	public float asteroid_damage = 1;

	private float current_health_point;

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		current_health_point = total_health_point;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
			return;

		if (other.tag == "Player")
		{
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
		}
		else if(other.tag == "bolt")
		{
			current_health_point -= other.GetComponent<BoltController>().get_damage();
			Destroy(other.gameObject);
			if (current_health_point < 0f)
			{
				Destroy(gameObject);
				Instantiate(explosion, transform.position, transform.rotation);
			}
		}
	}
}
