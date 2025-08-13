
using UnityEngine;

public class HitDetector : MonoBehaviour {
    [SerializeField] private Collider pickaxeCollider;
    [SerializeField] private Collider axeCollider;

    public void OpenCollider() {
        pickaxeCollider.enabled = true;
        axeCollider.enabled = true;
    }
    public void CloseCollider() {
        pickaxeCollider.enabled = false;
        axeCollider.enabled = false;    
    }
}
