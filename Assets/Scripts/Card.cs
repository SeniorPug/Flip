using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int imageID;
    public Image cardImg;  //image to display on card
    Animator animator;
    AudioSource audioSource;
    public bool is_flipped = true;
    public AudioClip flipSound;
    public AudioClip correctSound;

    void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void Flip()
    {
        if (is_flipped && Global.activeCard != this)
        {
            is_flipped = false;
            animator.Play("Close");
            PlayAudio(flipSound);
        } else if (!is_flipped && Global.can_flip)
        {
            is_flipped = true;
            animator.Play("Open");
            PlayAudio(flipSound);
            if (Global.activeCard == null)
            {
                Global.activeCard = this;
            } else
            {
                Global.can_flip = false;
                StartCoroutine(CheckCards());
            }
        }
    }

    //Flips without clicking (for hints and start of the game)
    public void RegularFlip(bool is_image_up)
    {
        if (is_image_up)
        {
            is_flipped = false;
            animator.Play("Close");
        }
        else
        {
            is_flipped = true;
            animator.Play("Open");
        }
    }

    //to start animation if two cards are the same
    public void DestroyCardAnimation()
    {
        animator.Play("Destroy");
    }

    //to destroy card (to call from animation)
    public void DestroyCard()
    {
        Destroy(gameObject);
    }

    public void RefreshImage()
    {
        try
        {
            cardImg.sprite = FindObjectOfType<LevelGenerator>().images[imageID];
        }
        catch
        {

        }
    }

    private void OnMouseUp()
    {
        if (Global.can_flip && Global.is_game)
        {
            Flip();
        }
    }

    void Start()
    {
        RefreshImage();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //Compares 2 flipped cards
    IEnumerator CheckCards()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Global.can_flip = true;
        Card secondCard = Global.activeCard;
        if (secondCard.imageID == imageID)
        {
            PlayAudio(correctSound);
            Global.activeCard = null;
            secondCard.DestroyCardAnimation();
            DestroyCardAnimation();
        } else
        {
            Global.activeCard = null;
            secondCard.Flip();
            Flip();
        }
    }
}
