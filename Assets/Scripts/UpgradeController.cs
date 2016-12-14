using UnityEngine;
using System.Collections;

public class UpgradeController : MonoBehaviour {

	private int upgrade_kind;
	private float value = 0;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
