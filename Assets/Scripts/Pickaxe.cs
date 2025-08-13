
using UnityEngine;

public class Pickaxe : BaseHotbarItem {
	public int damage;
	[SerializeField] private Animator handAnim;
	[SerializeField] private GameObject pickaxeModel;
	[SerializeField] private GameObject damageRockParticles;
	[SerializeField] private GameObject hitMarker;
	[SerializeField] private AudioSource hitPickaxeSfx;

	private const string EQUIP_PICKAXE = "EquipPickaxe";
	private const string HIT_PICKAXE = "HitPickaxe";

	private float attackCooldown = 1.5f;

	private void Start() {
		pickaxeModel.SetActive(false);
	}

	public override void EquipItem() {
		handAnim.Play(EQUIP_PICKAXE);
		pickaxeModel.SetActive(true);
	}

	public override void UnEquipItem() {
		pickaxeModel.SetActive(false);
	}

	public override void Attack() {
		handAnim.Play(HIT_PICKAXE);
	}

	public override float GetAttackCooldown() {
		return attackCooldown;
	}

	private void OnTriggerEnter(Collider other) {
		if (!pickaxeModel.activeSelf) return;
		if (other.gameObject.CompareTag("Rock")) {
			hitPickaxeSfx.Play();
			if (other.gameObject.TryGetComponent<RockBehaviour>(out RockBehaviour rock)) {

				if (rock.CanDamageRock()) {
					rock.DamageRock(damage);
					Instantiate(damageRockParticles, other.ClosestPointOnBounds(transform.position), Quaternion.identity);

					if (!rock.HasHitMarker()) {
						GameObject hitMarkerGO = Instantiate(hitMarker, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
						hitMarkerGO.GetComponent<RockMarker>().SetRock(rock);
						hitMarkerGO.transform.parent = rock.transform;
					}
					if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10)) {
						if (hit.collider.TryGetComponent<RockBehaviour>(out RockBehaviour rockHit)) {
							rockHit.InstantiateRockObject(hit.point);
						}
					}

				}
			}
		}
	}
}
