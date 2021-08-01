using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameCondition : MonoBehaviour
{
    int timer = 120;
    AudioSource audioSource;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winScreen;
    public GameObject loseScreen;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = (Global.size[0] * Global.size[1] + 4) * 3;
        if (timerText != null)
        {
            timerText.text = Math.Round(timer / 60f, 2) + " min";
            StartCoroutine(CountDown(timer));
        }
    }

    void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    IEnumerator CountDown(int time)
    {
        Card[] cards;
        do
        {
            yield return new WaitForSecondsRealtime(1);
            cards = FindObjectsOfType<Card>();
            if (cards.Length == 0 || cards == null)
            {
                PlayAudio(winSound);
                winScreen.color = new Color(1, 1, 1, 1);
                if (Global.size[0] * Global.size[1] > Global.record[0] * Global.record[1])
                {
                    Global.record = Global.size;
                }
                StartCoroutine(LoadMenu(3f));
                break;
            }
            time--;
            string minutes = (time / 60) < 10 ? "0" + time / 60 : (time / 60).ToString();
            string seconds = (time % 60) < 10 ? "0" + time % 60 : (time % 60).ToString();
            timerText.text = minutes + ":" + seconds;
        } while (time > 0);

        cards = FindObjectsOfType<Card>();
        if (cards.Length > 0)
        {
            PlayAudio(loseSound);
            loseScreen.SetActive(true);
            Global.global.Save();
            StartCoroutine(LoadMenu(5f));
            foreach (Card card in cards)
            {
                card.DestroyCard();
            }
        }
    }

    IEnumerator LoadMenu(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        Global.size = new int[] { 6, 6 };
        Global.global.Save();
        SceneManager.LoadScene("Menu");
    }
}
