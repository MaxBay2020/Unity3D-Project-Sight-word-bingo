using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip instructionClip;
    public float delaySeconds = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instructionClip = audioSource.clip;
        StartCoroutine(GoToMainGame(instructionClip.length + delaySeconds));
    }

    IEnumerator GoToMainGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(1);
    }


}
