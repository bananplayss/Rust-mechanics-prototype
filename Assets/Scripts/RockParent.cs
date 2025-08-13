
using UnityEngine;

public class RockParent : MonoBehaviour
{
    [SerializeField] private RockBehaviour[] rocks;

    private int health = 120;
    private int damageTaken;
    private int activePhase;
	private bool canTakeDamage = true;
	private float damageCooldown = 1.5f;
	private float counter;
	private int finalStoneMined = 130;
	public RockMarker hitMarker { get; set;}

	public Mesh nextHitMesh;

	private void Start() {
		counter = damageCooldown;
	}

	public bool HasHitMarker() {
		return hitMarker != null;
	}
	public void SetHitMarker(RockMarker hitMarker) {
		this.hitMarker = hitMarker;
	}

	public bool CanDamageRock() {
		return canTakeDamage;
	}

	public void DamageRock(int damage) {
		if (!canTakeDamage) return;
		canTakeDamage = false;
		health -= damage;
        damageTaken += damage;
		int random = Random.Range(1, 3);
		StoneMinedUI.Instance.AddStoneMined(damage*random);
		if(damageTaken+damage >= 40) {
			if (health - damage <= 0) {
				
			}
			else{
				nextHitMesh = rocks[activePhase + 1].mesh;
			}
			
		}
		if (health <= 0) {
			StoneMinedUI.Instance.AddStoneMined(finalStoneMined*random);
			Destroy(gameObject);
		}
		else if (damageTaken>=40) {
			rocks[activePhase].gameObject.SetActive(false);
			activePhase++;
			rocks[activePhase].gameObject.SetActive(true);
			hitMarker.transform.parent = rocks[activePhase].transform;
			damageTaken = 0;
		}	
	}

	private void Update() {
		if (!canTakeDamage) {
			counter -= Time.deltaTime;
			if (counter < 0) {
				counter = damageCooldown;
				canTakeDamage = true;
			}
		}
	}
}
