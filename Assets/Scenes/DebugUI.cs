using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    private GameManager gameManager;
    private Text sliderValText;
    private bool showDebugUI;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DebugUIBuilder.instance.AddLabel("Debug UI");
        DebugUIBuilder.instance.AddToggle("Toggle Marker", ToggleMarker);
        var alphaSlider = DebugUIBuilder.instance.AddSlider("Alpha", 0.1f, 1.0f, CubeAlpha);
        var textElementsInSlider = alphaSlider.GetComponentsInChildren<Text>();
        Text sliderText = textElementsInSlider[0];
        sliderValText = textElementsInSlider[1];
        sliderText.text = "Alpha";
        alphaSlider.GetComponentInChildren<Slider>().value = 0.5f;
        sliderValText.text = alphaSlider.GetComponentInChildren<Slider>().value.ToString();

        CubeAlpha(0.5f);
        showDebugUI = false;
        //gameManager.ToggleHands(!showDebugUI);

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            showDebugUI = !showDebugUI;
            if (showDebugUI)
            {
                DebugUIBuilder.instance.Show();
                //gameManager.ToggleHands(false);
            }
            else
            {
                DebugUIBuilder.instance.Hide();
                //gameManager.ToggleHands(true);
            }
        }
    }


    private void CubeAlpha(float alpha)
    {
        sliderValText.text = alpha.ToString();
        gameManager.bigCube.setAlpha(alpha);
        gameManager.mediumCube.setAlpha(alpha);
        gameManager.smallCube.setAlpha(alpha);
    }

    private void ToggleMarker(Toggle marker)
    {
        gameManager.bigCube.showmarker(marker.isOn);
        gameManager.mediumCube.showmarker(marker.isOn);
        gameManager.smallCube.showmarker(marker.isOn);
    }

}
