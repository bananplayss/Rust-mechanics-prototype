
using UnityEngine;

public class ItemHotbar : MonoBehaviour
{
	[SerializeField] private BaseHotbarItem[] hotBarItems;
	private bool itemInHandBool = false;

	public BaseHotbarItem HotbarItem { get; private set; }


	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			//Axe
			HotbarItem?.UnEquipItem();
			HandleItemEquip(hotBarItems[0]);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			//Pickaxe
			HotbarItem?.UnEquipItem();
			HandleItemEquip(hotBarItems[1]);
		}
	}

	public void HandleItemEquip(BaseHotbarItem equipableItem) {
		if (!itemInHandBool) {
			HotbarItem = equipableItem;
			HotbarItem.EquipItem();
		} else {
			HotbarItem = null;
			HotbarItem.UnEquipItem();
		}
	}
}
