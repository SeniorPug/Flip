using UnityEngine;
using TMPro;

public class TextLocalization : MonoBehaviour
{
    TextMeshProUGUI textField;

    public string token;

    //all text tokens with translations
    string[,] tokens =
    {
        { "Play", "Play", "������" },
        { "Settings", "Settings", "���������" },
        { "Exit", "Exit", "�����" },
        { "Width", "Width", "������" },
        { "Heigth", "Heigth", "������" },
        { "Language", "Language", "����" },
        { "Record", "Record:", "������:" },
        { "Win", "You won", "�� �������" },
        { "Lose", "You lost", "�� ��������" },
        { "Hint", "Hint", "���������" }
    };

    public void SetText()
    {
        for (int i = 0; i < tokens.Length; i++)
        {
            if (token == tokens[i, 0])
            {
                textField.text = tokens[i, Global.language];
                break;
            }
        }
    }

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        Global.global.Load();
        SetText();
    }
}
