

using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    private float damageCooldown = .4f;
    private bool canDamageTree = true;
    private int health = 250;

    [SerializeField] private GameObject dustParticles;
    [SerializeField] private GameObject treeObjectParticles;
    [SerializeField] private GameObject markerParticles;
    [SerializeField] private TreeMarker[] markers;

    public void HandleHitMarkers(Vector3 pos,TreeMarker currentMarker) {

		TreeMarker closestMarker = GetClosestMarker(pos,currentMarker);
		closestMarker.Enable();

	}

    public void InstantiateParticles(Vector3 hitPosition,bool instantiateMarkerParticles = false) {
        Instantiate(dustParticles, hitPosition, Quaternion.identity);
        Instantiate(treeObjectParticles, hitPosition, Quaternion.identity);
        if(instantiateMarkerParticles) Instantiate(markerParticles,hitPosition, Quaternion.identity);
	}

    public TreeMarker GetClosestMarker(Vector3 pos,TreeMarker currentMarker) {
		TreeMarker _marker = markers[0];
		foreach (var marker in markers) {
            if (marker.Used) continue;
            if(currentMarker != null) {
                if (marker == currentMarker) continue;
            }
            Vector3 comparablePos = Vector3.zero;
			float lastDistance = 999;
			if (Vector3.Distance(comparablePos, marker.transform.position) < lastDistance) {
				comparablePos = _marker.transform.position;
				lastDistance = Vector3.Distance(comparablePos, pos);
				_marker = marker;
			}
		}
        return _marker;
	}

    public bool CanDamageTree() {
        return canDamageTree;
    }

    public void DamageTree(int damage) {
        health -= damage;
        if (health < 0) {
            //animation, cue destruction using animation events
        }
    }

	private void Update() {
        if (!canDamageTree) {
            float counter = 0;
            counter += Time.deltaTime;
            if(counter >= damageCooldown) {
                counter = 0;
                canDamageTree = true;
            }
        }
	}
}
