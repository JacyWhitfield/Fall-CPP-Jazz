using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarrotManager : MonoBehaviour
{
    public int carrotCount;
    public TextMeshProUGUI carrotText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carrotText.text = "Carrots: " + carrotCount.ToString();
    }
}
