using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pyramid : MonoBehaviour {
	public GameObject prefab;
	public int baseLength = 3;
	public float spacing = 5f;

	private List<GameObject> copies = new List<GameObject>();
	private List<Vector3> locs = new List<Vector3>();

	void Start() {
		Vector3 startPosition = transform.position;
		int layerLength = baseLength;

		for(int y = 0; y < baseLength; y++) {
			for(int x = 0; x < layerLength; x++) {
				Vector3 offset = new Vector3(y * spacing * 0.5f, 
																		 y * spacing,
																		 y * spacing * 0.5f);
				for(int z = 0; z < layerLength; z++) {
					Vector3 loc = new Vector3(x * spacing, 
																		0, 
																		z * spacing) + startPosition + offset;
					locs.Add(loc);
					GameObject copy = Instantiate(prefab, loc, Quaternion.identity) as GameObject;
					copies.Add(copy);
				}
			}				
			layerLength--;
		}
	}
}
