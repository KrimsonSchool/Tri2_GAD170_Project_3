using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Gems: " + PlayerPrefs.GetInt("Gems") + "\nXero: " + PlayerPrefs.GetInt("Xero");
    }
}
