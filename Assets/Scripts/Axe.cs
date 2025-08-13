
using UnityEngine;

public class Axe : BaseHotbarItem {
	public int damage;
	[SerializeField] private Animator handAnim;
	[SerializeField] private GameObject axeModel;
	[SerializeField] private GameObject hitMarker;
	[SerializeField] private AudioSource hitAxeSfx;

	private const string AXE_EQUIP = "EquipAxe";
	private const string AXE_HIT = "HitAxe";

	private float attackCooldown = .35f;

	private void Start() {
		axeModel.SetActive(false);
	}

	public override void EquipItem() {
		handAnim.Play(AXE_EQUIP);
		axeModel.SetActive(true);
	}

	public override void UnEquipItem() {
		axeModel.SetActive(false);
	}
	public override void Attack() {
		handAnim.Play(AXE_HIT);
	}

	public override float GetAttackCooldown() {
		return attackCooldown;
	}

	private void OnTriggerEnter(Collider other) {
		if (!axeModel.activeSelf) return;
		if (other.gameObject.CompareTag("Tree")) {
			hitAxeSfx.Play();
			Debug.Log(other.transform.name);
			if (other.transform.TryGetComponent<TreeBehaviour>(out TreeBehaviour tree)) {

				if (tree.CanDamageTree()) {
					tree.DamageTree(damage);
					Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
					tree.InstantiateParticles(closestPoint);
					tree.HandleHitMarkers(closestPoint,null);

					//TreeObject handling
					if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit,10)) {
						if(hit.collider.TryGetComponent<TreeHit>(out TreeHit treeHit)) {
							treeHit.InstantiateTreeObject(hit.point);
						}
					}

				}
			}
		}
	}
}
