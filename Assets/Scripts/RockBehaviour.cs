
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
	RockParent rockParent;
	public Mesh mesh;

	[SerializeField] private GameObject rockObject;

	public void InstantiateRockObject(Vector3 position) {
		GameObject rockObjectGO = Instantiate(rockObject, position, Quaternion.identity);
		float rotationX = rockObjectGO.transform.rotation.x;
		rockObjectGO.transform.LookAt(Camera.main.transform.position);
		rockObjectGO.transform.parent = transform;
	}

	private void Start() {
		rockParent = GetComponentInParent<RockParent>();
	}

	public void DamageRock(int damage) {
		rockParent.DamageRock(damage); //weak point;
	}

	public void SetHitMarker(RockMarker marker) {
		rockParent.SetHitMarker(marker);
	}

	public Mesh GetNextHitMesh() {
		return rockParent.nextHitMesh;
	}
	
	public bool CanDamageRock() { return rockParent.CanDamageRock(); }

	public bool HasHitMarker() {
		return rockParent.HasHitMarker();
	}

	public void MoveMarkerToNextSpot(Mesh hitMesh) {
		int maxTries = 100;
		for (int i = 0; i < maxTries; i++) {
			int random = Random.Range(0, hitMesh.vertices.Length);

			Vector3[] verts = hitMesh.vertices;
			int[] indices = hitMesh.triangles;
			Vector3[] normals = hitMesh.normals;
			Vector3 p1 = verts[indices[random]];
			Vector3 p2 = verts[indices[random]];
			Vector3 p3 = verts[indices[random]];

			Vector3 center = ((p1 + p2 + p3) / 3);

			Vector3 side1 = p2 - p1;
			Vector3 side2 = p3 - p1;

			Vector3 perp = Vector3.Cross(side1, side2);
			float vectorSize = (Camera.main.transform.position - rockParent.hitMarker.transform.position).magnitude;
			if (Physics.Raycast(rockParent.hitMarker.transform.position+center+perp*1.3f, Camera.main.transform.position - rockParent.hitMarker.transform.position+center+perp*1.3f, out RaycastHit hit, vectorSize * 1.2f)) {
				if (hit.rigidbody != null) {
					if (hit.rigidbody.CompareTag("Rock")) {
						//Debug.Log("Placed the " + i + ". time.");
					} else {
						rockParent.hitMarker.transform.localPosition = center + perp * 1.3f;
						break;
					}
				}

			}



		}
		
		
	}

	
}
