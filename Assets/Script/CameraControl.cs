using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CameraControl : MonoBehaviour {
	int selection = -1;
	GameObject[] CurrentSelected = new GameObject[50];
	GameObject homecitybuttons, settlerbutton;
	public float speed = 2.5f;
	void Start()
	{
		homecitybuttons = GameObject.Find("Homecity");
		settlerbutton = GameObject.Find ("Settlerb");
		homecitybuttons.SetActive (false);
		settlerbutton.SetActive (false);
		for (int i = 0; i != 50; i++) {
			CurrentSelected [i] = null;
		}
	}
	void Update()
	{
		if (selection >= 0) {
			if (CurrentSelected [0] != null) {
				Vector3 Desired = new Vector3 (CurrentSelected [0].transform.position.x - 10f, transform.position.y, CurrentSelected [0].transform.position.z);
				transform.position = Vector3.Lerp (transform.position, Desired, 0.5f);
			}
		} /*else {
			float h = (speed * 3) * Input.GetAxis ("Mouse Y");
			float v = (speed * 3) * Input.GetAxis ("Mouse X");
			transform.Translate (v, 0, h);
		}*/
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hit) {
				Debug.Log ("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "Hero") {
					homecitybuttons.SetActive (false);
					selection++;
					Debug.Log ("Selection " + selection + " - " + hitInfo.transform.name);
					CurrentSelected [selection] = hitInfo.transform.gameObject;
					if (hitInfo.transform.name.Contains ("Settler")) {
						Debug.Log ("Activation du menu settler");
						if(homecitybuttons.activeInHierarchy) 
							homecitybuttons.SetActive (false);
						if(!settlerbutton.activeInHierarchy) 
							settlerbutton.SetActive (true);
					}
				}
				else if (hitInfo.transform.gameObject.tag == "homecity") {
					Debug.Log ("Activation du menu homecity");
					if(settlerbutton.activeInHierarchy) 
						settlerbutton.SetActive (false);
					homecitybuttons.SetActive (true);
				}
			}
		} else if (Input.GetMouseButtonDown (1)) {
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			for (int i = 0; i != 50; i++) {
				if (CurrentSelected [i] != null) {
					if (hit) {
						CurrentSelected [i].GetComponent<NavMeshAgent> ().SetDestination (hitInfo.point);
						Debug.DrawLine (transform.position, hitInfo.point);
						if (hitInfo.transform.gameObject.tag == "Enemy") {
							Debug.Log ("Attacking " + hitInfo.transform.name);
							CurrentSelected [i].GetComponent<Attacking> ().Target = hitInfo.transform.gameObject;
						}
						else if (CurrentSelected [i].GetComponent<Attacking> ().Target != null) {
							CurrentSelected [i].GetComponent<Attacking> ().Target = null;
						}
					}
				}
			}
		}
	}
}
