using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : MonoBehaviour {

	public GameObject Target;
	public int damage = 5;
	public float attackrange = 1.0f;
	void Start () {
		Target = null;
	}
	void WaitAndAttack()
	{
		if(Target != null)
			Target.GetComponent<Health> ().Attacked (damage);
	}
	void Update () {
		if (Target != null) {
			float distance = Vector3.Distance (Target.transform.position,transform.position);
			if (distance != 0 && distance <= attackrange) {
				if (!IsInvoking ("WaitAndAttack")) {
					Debug.Log ("Distance d'attaque en cours " + distance);
					Invoke ("WaitAndAttack", 0.5f);
				}
			} else {
				gameObject.GetComponent<NavMeshAgent> ().SetDestination (Target.transform.position);
			}
		} else {
			if (gameObject.CompareTag ("Hero")) {
				GameObject[] found = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach (GameObject x in found) {
					float distance = Vector3.Distance (x.transform.position, transform.position);
					if (distance <= 4.0f) {
						//Debug.Log ("New target " + x.transform.name + " Distance: " + distance);
						Target = x;
						gameObject.GetComponent<NavMeshAgent> ().SetDestination (x.transform.position);
						break;
					}
				}
			}
			else if (gameObject.CompareTag ("Enemy")) {
				GameObject[] found = GameObject.FindGameObjectsWithTag ("Hero");
				foreach (GameObject x in found) {
					float distance = Vector3.Distance (x.transform.position, transform.position);
					if (distance <= 4.0f) {
						//Debug.Log ("New target " + x.transform.name + " Distance: " + distance);
						Target = x;
						gameObject.GetComponent<NavMeshAgent> ().SetDestination (x.transform.position);
						break;
					}
				}
			}
		}
	}
}
