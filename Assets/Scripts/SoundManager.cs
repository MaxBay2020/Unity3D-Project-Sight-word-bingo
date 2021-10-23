using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    private AudioSource audioSource;
    public AudioClip good_job, try_again, instructionClip;
    public List<AudioClip> wordClips = new List<AudioClip>();
    private int randomNumber;
    public string currentWord;
    public GameObject gameManager;
    private bool isDone;


    private void Start()
    {
        _instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(instructionClip);
        StartCoroutine(StartPlaying());
    }


    IEnumerator StartPlaying()
    {
        yield return new WaitForSeconds(instructionClip.length);
        gameManager.SetActive(true);

    }

    public void PlayRandomWordClip(List<AudioClip> wordClips)
    {
        if (GameManager._instance.isWinning)
        {
            Debug.Log("winning");
            return;
        }


        randomNumber = Random.Range(0, wordClips.Count);
        audioSource.PlayOneShot(wordClips[randomNumber]);
        currentWord = wordClips[randomNumber].name;
        wordClips.Remove(wordClips[randomNumber]);
        Debug.Log(currentWord);
    }

    public void PlayTryAgainClip()
    {
        audioSource.PlayOneShot(try_again);
    }

    public void PlayGoodJobClip()
    {
        audioSource.PlayOneShot(good_job);
    }


}
