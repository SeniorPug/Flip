using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonFunctions : MonoBehaviour
{
    public Slider widthSlider;
    public Slider heightSlider;
    public TMP_Dropdown languageDropdown;
    public TextMeshProUGUI widthText;
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI recordValue;
    string textWidthDefault;
    string textHeightDefault;

    public void Play()
    {
        Global.global.Save();
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Global.global.Save();
        Application.Quit();
    }

    public void ChangeSize(bool is_width)
    {
        if (is_width)
        {
            if ((widthSlider.value * heightSlider.value) % 2 > 0 || widthSlider.value * heightSlider.value > 36)
            {
                widthSlider.value--;
            }
            Global.size[0] = (int)widthSlider.value;
            widthText.text = textWidthDefault + ": " + widthSlider.value;
        } else
        {
            if ((widthSlider.value * heightSlider.value) % 2 > 0 || widthSlider.value * heightSlider.value > 36)
            {
                heightSlider.value--;
            }
            Global.size[1] = (int)heightSlider.value;
            heightText.text = textHeightDefault + ": " + heightSlider.value;
        }
    }

    public void ChangeLanguage()
    {
        Global.language = languageDropdown.value + 1;
        Global.global.Save();
        TextMeshProUGUI[] textes = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textMesh in textes)
        {
            if (textMesh.GetComponent<TextLocalization>() != null)
            {
                textMesh.GetComponent<TextLocalization>().SetText();
            }
        };
        textWidthDefault = widthText.text;
        textHeightDefault = heightText.text;
        ChangeSize(true);
        ChangeSize(false);
    }

    void Start()
    {
        Global.global.Load();
        textWidthDefault = widthText.text;
        textHeightDefault = heightText.text;
        languageDropdown.value = Global.language - 1;
        recordValue.text = Global.record[0] + "x" + Global.record[1];
    }
}
