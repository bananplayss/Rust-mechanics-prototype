
using TMPro;
using UnityEngine;

public class StoneMinedUI : MonoBehaviour
{
	int stoneMined = 0;
	TextMeshProUGUI stonedMinedText;

    public static StoneMinedUI Instance { get; private set; }

	private void Awake() {
		Instance = this;
	}

	private void Start() {
		stonedMinedText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void AddStoneMined(int stoneMined) {
		this.stoneMined += stoneMined;
		stonedMinedText.SetText($"Stone mined: {this.stoneMined}");
	}
}
