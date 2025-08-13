using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	[SerializeField] private Animator handAnimator;
	ItemHotbar itemHotbar;

	private bool canAttack = true;
	public float attackCooldown;
	private float counter;

	private void Start() {
		itemHotbar = GetComponent<ItemHotbar>();
	}

	private void Update() {
		if (Input.GetMouseButton(0) && canAttack) {
			BaseHotbarItem hotBarItem = itemHotbar.HotbarItem;
			if (hotBarItem == null) return;
			hotBarItem.Attack();
			attackCooldown = hotBarItem.GetAttackCooldown();
			canAttack = false;
		}

		if (!canAttack) {
			counter += Time.deltaTime;
			if (counter >= attackCooldown) {
				counter = 0;
				canAttack = true;
			}
		}
	}
}