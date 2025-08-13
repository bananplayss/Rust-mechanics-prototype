

using UnityEngine;

public class RockMarker : MonoBehaviour
{
	private RockBehaviour rock;
	private float maxDistance = 10;
	private int hitMarkerDamage = 30;

	[SerializeField] private GameObject hitmarkerParticles;
	[SerializeField] private AudioSource pickaxeHit;
	[SerializeField] private GameObject damageRockParticles;

	public void SetRock(RockBehaviour rock) {
		this.rock = rock;
		rock.SetHitMarker(this);
	}

	private void Update() {
		if(Vector3.Distance(transform.position, Camera.main.transform.position) > maxDistance) {
			gameObject.SetActive(false);
		} else {
			gameObject.SetActive(true);
			
			float yRot = transform.rotation.y;
			yRot = Mathf.Clamp(yRot,30,70);
			transform.rotation = Quaternion.Euler(0,yRot,0);

			transform.LookAt(Camera.main.transform.position);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Pickaxe")) {
			rock.DamageRock(hitMarkerDamage);
			StoneMinedUI.Instance.AddStoneMined(hitMarkerDamage*2);
			rock.MoveMarkerToNextSpot(rock.GetNextHitMesh());
			Instantiate(hitmarkerParticles, transform.position, Quaternion.identity);
			Instantiate(damageRockParticles, transform.position, Quaternion.identity);
			pickaxeHit.Play();
		}
	}
}
