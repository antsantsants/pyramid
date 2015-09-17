using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pyramid : MonoBehaviour {
	public GameObject prefab;
	public int baseLength = 3;
	public float spacing = 5f;

	public float explosionRadius;
	public float explosionUpwardModifier;

	private List<GameObject> copies = new List<GameObject>();
	private List<Vector3> locs = new List<Vector3>();
	private bool inPlace = true;
	private bool interruptReset = false;
	private bool resetting = false;
	private Vector3 centerPoint;

	void Start() {
		Vector3 startPosition = transform.position;
		centerPoint = startPosition;
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
		for(int i = 0; i < copies.Count; i++) {
			copies[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.E))
			Explode();
	    if(Input.GetKeyDown(KeyCode.R))
	    	Reset();
	}


	public void Explode() {
		inPlace = false;
		interruptReset = true;
		Vector3 explosionPosition = new Vector3(0f, 
																						0.8660254037f * spacing, 
																						0f) + transform.position;
		for(int i = 0; i < copies.Count; i++) {
			Rigidbody rb = copies[i].GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.None;
			rb.AddExplosionForce(Random.Range(600f, 1000f), // force
													 explosionPosition,         // position
													 1000f,											// radius 
													 5f); 											// mode
		}
	}

  public void Reset() {
    if(!resetting && !inPlace) {
      resetting = true;
      interruptReset = false;
      for(int z = 0; z < locs.Count; z++){
        StartCoroutine(MoveObject(copies[z], locs[z], 1.2f));
        //copies[z].transform.rotation = Quaternion.identity;
        //copies[z].rigidbody.constraints = RigidbodyConstraints.FreezeAll;
      }
    }
  }

  IEnumerator MoveObject(GameObject gameObject, Vector3 endPosition, float time) {
    float rate = 1f / time;
    Vector3 startPosition = gameObject.transform.position;
    Quaternion startRotation = gameObject.transform.rotation;

    float i = 0f;
    while (i < 1f) {
      if(interruptReset) {
        resetting = false;
        yield break;
      }
      i += Time.deltaTime * rate;
      gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, i);
      gameObject.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, i);
      yield return 0;
    }

    gameObject.transform.position = endPosition;
    gameObject.transform.rotation = Quaternion.identity;
    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    resetting = false;
    inPlace = true;
  }

}
