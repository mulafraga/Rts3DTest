using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnthings : MonoBehaviour {
	public GameObject prefab1;
	public GameObject prefab2;
	int houses = 1;
	int population = 0;
	GameObject temp = null;
	GameObject Player;
	bool isBuilding = false;
	void Update()
	{
		if (isBuilding) {
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if(hit)
			{
				Vector3 pos = temp.transform.position;
				pos.x = hitInfo.point.x;
				pos.z = hitInfo.point.z;
				temp.transform.position = pos;
				if (Input.GetMouseButtonDown (0)) {
					isBuilding = false;
					temp.GetComponent<BoxCollider> ().enabled = true;
					houses++;
				} else if (Input.GetKeyDown (KeyCode.A)) {
					isBuilding = false;
					Destroy (temp);
				}
			}
		}
	}
	public void SpawnThing(string name)
	{
		switch (name) {
		case "Settler":
			if (population < (houses * 10)) {
				temp = Instantiate (prefab1);
				temp.transform.position = new Vector3 (transform.position.x - 3.5f, transform.position.y, transform.position.z);
				population++;
			}
			break;
		case "House":
			if (houses < 21) {
				temp = Instantiate (prefab2);
				temp.transform.position = new Vector3 (transform.position.x - 3.5f, transform.position.y, transform.position.z);
				temp.GetComponent<BoxCollider> ().enabled = false;
				isBuilding = true;
			}
			break;
		}

	}
}
