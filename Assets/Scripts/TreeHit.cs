using UnityEngine;

public class TreeHit : MonoBehaviour
{
	[SerializeField] private GameObject treeObject;

	public void InstantiateTreeObject(Vector3 position) {
		GameObject treeObjectGo = Instantiate(treeObject, position, Quaternion.identity);
		float rotationX = treeObjectGo.transform.rotation.x;
		treeObjectGo.transform.LookAt(Camera.main.transform.position);
		treeObjectGo.transform.rotation = new Quaternion(rotationX, treeObjectGo.transform.rotation.y, treeObjectGo.transform.rotation.z, 0);
		treeObjectGo.transform.parent = transform;
	}
}
