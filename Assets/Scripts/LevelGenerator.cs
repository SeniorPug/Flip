using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Sprite[] images;  //array of possible images of card
    public GameObject card;

    //Generate NEW Game
    void Generate()
    {
        List<int> imgIDs = new List<int>();

        //Adding pairs of cards (images) to a List
        for (int i = 0; i < Global.size[0] * Global.size[1]; i++)
        {
            imgIDs.Add(i / 2);
        }

        //Spawning cards with random image and removing it from List
        for (int i = 0; i < Global.size[0]; i++)
        {
            for (int j = 0; j < Global.size[1]; j++)
            {
                int randNum = Random.Range(0, imgIDs.Count);
                GameObject cur_card = Instantiate(card, new Vector3(i * 2, j * 2, 0), Quaternion.identity);
                cur_card.GetComponent<Card>().imageID = imgIDs[randNum];
                imgIDs.Remove(imgIDs[randNum]);
            }
        }

        
        if (Global.is_game)
        {
            //set camera's position to fit all cards on screen
            Camera.main.transform.position = new Vector3(Global.size[0] - 1, Global.size[1] - 1, (Global.size[0] + Global.size[1]) * -1f);

            //to flip cards back
            StartCoroutine(FlipCards(2.5f, true));
        } else
        {
            //idle for Menu
            StartCoroutine(RandomCardFlip());
        }
    }

    public void Hint()
    {
        StopAllCoroutines();
        if (Global.activeCard == null)
        {
            List<int> imgIDs = new List<int>();
            Card[] allCards = FindObjectsOfType<Card>();
            foreach (Card card in allCards)
            {
                imgIDs.Add(card.imageID);
            }
            foreach (Card card in allCards)
            {
                int randNum = Random.Range(0, imgIDs.Count);
                card.imageID = imgIDs[randNum];
                imgIDs.Remove(imgIDs[randNum]);
                card.RefreshImage();
            }
            StartCoroutine(FlipCards(0f, false));
            StartCoroutine(FlipCards(2f, true));
        }
    }

    void Awake()
    {
        Generate();
    }

    //for hints or start of game
    IEnumerator FlipCards(float timer, bool is_image_up)
    {
        yield return new WaitForSecondsRealtime(timer);
        Card[] allCards = FindObjectsOfType<Card>();
        foreach (Card card in allCards)
        {
            card.RegularFlip(is_image_up);
        }
        Global.activeCard = null;
        Global.can_flip = is_image_up ? true : false;
    }

    IEnumerator RandomCardFlip()
    {
        yield return new WaitForSecondsRealtime(1);
        Card[] allCards = FindObjectsOfType<Card>();
        foreach (Card card in allCards)
        {
            card.RegularFlip(true);
        }
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0.25f, 1f));
            Card card = allCards[Random.Range(0, allCards.Length)];
            if (card.is_flipped)
            {
                card.RegularFlip(true);
            } else
            {
                card.RegularFlip(false);
            }
        }
    }
}
