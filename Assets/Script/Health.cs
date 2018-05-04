using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public int current = 100;
	void Start () {
	}
		
	public void Attacked(int damage)
	{
		if (current - damage <= 0) {
			Destroy (gameObject);
		} else {
			current -= damage;
			Debug.Log ("Attacked, hp = "+current);
		}
	}
}
