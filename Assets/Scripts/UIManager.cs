using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private TMP_Text playerWoodText;
    public static int playerWoodAmount = 0;

    void Update() {
        playerWoodText.text = playerWoodAmount.ToString() + " Wood";
    }
}
