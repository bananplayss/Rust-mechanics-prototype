
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TreeMarker : MonoBehaviour {
	private TreeBehaviour tree;
	private int hitMarkerDamage = 25;
	private DecalProjector decal;
	public bool Used {  get; private set; }

	private void Start() {
		tree = transform.parent.GetComponentInParent<TreeBehaviour>();
		decal = GetComponent<DecalProjector>();
		Disable();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Axe") && tree.CanDamageTree() && decal.enabled) {
			tree.DamageTree(hitMarkerDamage);
			tree.HandleHitMarkers(transform.position, this);
			tree.InstantiateParticles(transform.position,true);
			Used = true;
			Disable();

		}
	}

	public void Enable() {
		decal.enabled = true;	
	}
	public void Disable() {
		decal.enabled = false;
	}

}
