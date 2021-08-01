using UnityEngine;
using TMPro;

public class TextLocalization : MonoBehaviour
{
    TextMeshProUGUI textField;

    public string token;

    //all text tokens with translations
    string[,] tokens =
    {
        { "Play", "Play", "Играть" },
        { "Settings", "Settings", "Настройки" },
        { "Exit", "Exit", "Выход" },
        { "Width", "Width", "Ширина" },
        { "Heigth", "Heigth", "Высота" },
        { "Language", "Language", "Язык" },
        { "Record", "Record:", "Рекорд:" },
        { "Win", "You won", "Ты выиграл" },
        { "Lose", "You lost", "Ты проиграл" },
        { "Hint", "Hint", "Подсказка" }
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
