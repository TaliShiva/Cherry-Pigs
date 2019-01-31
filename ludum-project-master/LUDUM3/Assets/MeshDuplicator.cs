using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDuplicator : MonoBehaviour {

	void Awake() {
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		Mesh mesh2 = Instantiate(mesh);
		GetComponent<MeshFilter>().sharedMesh = mesh2;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
