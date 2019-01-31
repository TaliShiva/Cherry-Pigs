using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.Animations;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MouseController : MonoBehaviour {

	public Vector3 mousePosition;
	public Camera mainCamera;
	public float laserPower;
	public float laserMeshAffectdistance;
	public ParticleSystem laserFire;
	public ParticleSystem laser;
	public Transform stoneToThrow;
	public static List<GameObject> destPoints = new List<GameObject>();
	public static List<GameObject> pigs = new List<GameObject>();
	public GameObject mainExtractionPoint;
	public static int LastPigs = 0;

	void Awake() {
		laser = Instantiate(laser);
		mousePosition = Vector3.zero;
		mainCamera = Camera.main;
		UpdateLaserParams(7.0f);
	}

	void Start () {
		
	}
	
	void Update () {
		RaycastHit hit;
		Ray ray = new Ray();
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit)) {
			if (Input.GetMouseButton((int) MouseButton.LeftMouse))
			{
				Vector3 newLaserPosition = new Vector3(hit.point.x, 80.0f, hit.point.z);
				laser.transform.position = newLaserPosition;
				laser.Play();

				
				laserFire.Play();
				laserFire.transform.position = hit.point;
				laserFire = Instantiate(laserFire);
				if (hit.collider.tag == "Undestroyable" || hit.collider.tag == "Homeless")
				{
					laserFire.transform.position = new Vector3(hit.point.x, 1.0f, hit.point.z);
					return;
				}

				LaserApply3000(hit.point, hit.transform.gameObject);
			}
			else {
				laser.Stop();
			}
		}
	}

	public void ThrowDestoyedPart(Vector3 worldMousePosition, GameObject objectToDestroy) {
		for (int i = 0; i < 7; ++i)
		{
			Vector3 randomForce = Random.onUnitSphere/2.0f + worldMousePosition;
			stoneToThrow.transform.position = worldMousePosition;
			stoneToThrow.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);
			stoneToThrow = Instantiate(stoneToThrow);

		}
	}

	public void LaserApply3000(Vector3 worldMousePosition, GameObject objectToMelt) {
		Mesh mesh = objectToMelt.GetComponent<MeshFilter>().sharedMesh;
		MeshCollider meshCollider = objectToMelt.GetComponent<MeshCollider>();

		Vector3[] newVertices = mesh.vertices;
		float baseOffsetY = 4.0f;
		for (int i = 0; i < newVertices.Length; ++i) {
			float distanceFromTerrain = objectToMelt.transform.TransformPoint(newVertices[i]).y;
			float randomOffsetX = Random.Range(-1.0f, 1.0f);
			float randomOffsetZ = Random.Range(-1.0f, 1.0f);

			Vector3 worldVertexPosition = objectToMelt.transform.TransformPoint(newVertices[i]);
			float distanceX = Mathf.Abs(worldVertexPosition.x - worldMousePosition.x);
			float distanceZ = Mathf.Abs(worldVertexPosition.z - worldMousePosition.z);
			float distance = Mathf.Sqrt(distanceX * distanceX + distanceZ * distanceZ);
			float offsetY = 0.0f;
			if (distanceX < laserMeshAffectdistance && distanceZ < laserMeshAffectdistance && distanceFromTerrain >= 1.0f)
			{
				offsetY = baseOffsetY / distance;
				newVertices[i].y -= offsetY;
				Vector3 newWorldVertexPosition = objectToMelt.transform.TransformPoint(newVertices[i]);
				float offsetYCorrector = newWorldVertexPosition.y <= 0.5f ? newWorldVertexPosition.y - 0.5f : 0.0f;
				newVertices[i].y -= offsetYCorrector;
			}
		}

		mesh.vertices = newVertices;
		mesh.RecalculateBounds();
		meshCollider.sharedMesh = mesh;
		if (mesh.bounds.size.y < 4.0f)
		{
			ThrowDestoyedPart(worldMousePosition, objectToMelt);
			Destroy(objectToMelt);
		}
	}

	public void UpdateLaserParams(float newPower) {
		laserPower = newPower;
		laserMeshAffectdistance = laserPower;
	}

}
