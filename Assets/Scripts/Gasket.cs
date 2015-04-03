using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gasket : MonoBehaviour {
	public GameObject prefab;
	public float spacing = 5f;
	public int size = 3;

	private List<GameObject> copies = new List<GameObject>();
	private List<Vector3> locs = new List<Vector3>();
	private float twoThirds = 2f/3f;


	void Start() {
		GaskIt(new Vector3(-spacing, 0f, -0.57735f * spacing) + transform.position, 
					 new Vector3(spacing, 0f, -0.577357f * spacing) + transform.position,
					 new Vector3(0f, 0f, 1.1547f * spacing) + transform.position,
					 new Vector3(0f, 1.7320508074f * spacing, 0f) + transform.position,
					 size, 1f);
	}

	private void GaskIt(Vector3 a, Vector3 b, Vector3 c, Vector3 d, 
			int n, float scale) {
		if(n > 0) {
			AddIfNotAlreadyPresent(a); AddIfNotAlreadyPresent(b);
			AddIfNotAlreadyPresent(c); AddIfNotAlreadyPresent(d);

			// find midpoints
			Vector3 e = Vector3.Lerp(a, b, 0.5f); Vector3 f = Vector3.Lerp(a, c, 0.5f);
			Vector3 g = Vector3.Lerp(a, d, 0.5f); Vector3 h = Vector3.Lerp(b, c, 0.5f);
			Vector3 i = Vector3.Lerp(b, d, 0.5f); Vector3 j = Vector3.Lerp(c, d, 0.5f);

			// recurse
			GaskIt(a, g, f, e, n - 1, scale * twoThirds);
			GaskIt(e, h, b, i, n - 1, scale * twoThirds);
			GaskIt(c, h, f, j, n - 1, scale * twoThirds);
			GaskIt(d, g, i, j, n - 1, scale * twoThirds);
		}
	}

	private void AddIfNotAlreadyPresent(Vector3 loc) {
		if(!locs.Contains(loc)) {
			locs.Add(loc);
			GameObject copy = Instantiate(prefab, loc, Quaternion.identity) as GameObject;
			copies.Add(copy);
		}
	}

}
