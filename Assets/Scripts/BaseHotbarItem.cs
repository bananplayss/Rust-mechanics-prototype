using UnityEngine;

public class BaseHotbarItem : MonoBehaviour
{

	public virtual float GetAttackCooldown() {
		return 0;
	}

	public virtual void EquipItem() {

	}
	public virtual void UnEquipItem() {

	}

	public virtual void Attack() {

	}
}
