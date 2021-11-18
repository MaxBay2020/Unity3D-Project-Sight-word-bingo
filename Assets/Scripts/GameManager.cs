using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public List<Transform> cells = new List<Transform>();
    [SerializeField]
    private string currentWord;
    public static GameManager _instance;
    //public List<int> combinations = new List<int>() { 13 };
    public List<int> combinations = new List<int>();
    private List<List<int>> allWinningConditions = new List<List<int>>()
    {
        new List<int>(){ 1,2,3,4,5 },
        new List<int>(){ 6,7,8,9,10 },
        //new List<int>(){ 11,12,13,14,15 },
        new List<int>(){ 16,17,18,19,20 },
        new List<int>(){ 21,22,23,24,25 },

        new List<int>(){ 1,6,11,16,21 },
        new List<int>(){ 2,7,12,17,22 },
        //new List<int>(){ 3,8,13,18,23 },
        new List<int>(){ 4,9,14,19,24 },
        new List<int>(){ 5,10,15,20,25 },

        //new List<int>(){ 1,7,13,19,25 },
        //new List<int>(){ 5,9,13,17,21 }
    };
    public bool isWinning;
    private bool hasCurrentEle;
    private bool isPlayed;
    public Text hintText;
    public GameObject winningPanel;
    public List<int> whichWinningCondition;
    public bool hasFound;
    public List<Transform> whichCondition;
    public List<Transform> allWords = new List<Transform>();

    public int sec_of_3 = 3;

    public List<AudioClip> correctWords = new List<AudioClip>();

    private void Start()
    {
        _instance = this;
        PlayNextWord();
        //StartCoroutine(WordClipPlay(0.1f));
    }

    IEnumerator WordClipPlay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayNextWord();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(currentWord))
        {
            return;
        }
        if (isWinning && !isPlayed)
        {
            Debug.Log("winning");
            hasCurrentEle = false;
            combinations.Sort();
            foreach (var eachCondition in allWinningConditions)
            {

                if(eachCondition.All(t => combinations.Any(b => b == t)))
                {
                    whichWinningCondition = eachCondition;
                    break;
                }
            }
            whichWinningCondition.Sort();
            //print(whichWinningCondition);
            foreach (var item in whichWinningCondition)
            {
                print(item);
            }

            foreach (var eachWord in allWords)
            {
                int codeNumber = int.Parse(eachWord.name.Split(' ')[1]);
                foreach (var item in whichWinningCondition)
                {
                    if (item == codeNumber)
                    {
                        whichCondition.Add(eachWord);
                    }
                }

            }

            foreach (var item in whichCondition)
            {
                GameObject targetCellGO = GameObject.Find(item.name);
                targetCellGO.transform.Find("coin").DOPunchScale(new Vector3(0.1f, 0.1f, 0), sec_of_3, 3,0);
            }
            StartCoroutine(CoinVanish(sec_of_3, whichCondition) );



            //winningPanel.SetActive(true);
            //combinations.RemoveRange(0, combinations.Count);
            SoundManager._instance.PlayGoodJobClip();
            isPlayed = true;
            return;
        }else if (isWinning)
        {
            return;
        }

        // winning condition statement
        if (combinations.Count >= 5)
        {
            foreach (var eachCondition in allWinningConditions)
            {
                foreach (var eachElement in eachCondition)
                {
                    if (combinations.Contains(eachElement))
                    {
                        hasCurrentEle = true;
                    }
                    else
                    {
                        hasCurrentEle = false;
                        break;
                    }
                }

                if (hasCurrentEle)
                {
                    isWinning = true;

                    break;
                }
            }
        }


        foreach (Transform currentCell in cells)
        {
            currentCell.gameObject.SetActive(currentCell.name.Split(' ')[0].ToLower().Equals(currentWord.ToLower()));
        }
    }

    IEnumerator CoinVanish(int sec_of_3, List<Transform> whichCondition)
    {
        yield return new WaitForSeconds(sec_of_3);
        foreach (var coin in whichCondition)
        {
            coin.gameObject.SetActive(false);
        }
        // play word audio one by one
        foreach (var coin in whichCondition)
        {
            string coinName = coin.name.Split(' ')[0];
            foreach (AudioClip eachClip in SoundManager._instance.letterSoundAllClips)
            {
                if (coinName.ToLower() == eachClip.name.ToLower())
                {
                    correctWords.Add(eachClip);
                    //SoundManager._instance.audioSource.PlayOneShot(eachClip);
                }
            }

            // play corrects words one by one
            StartCoroutine(PlayWordClipOneByOne(1.0f));
        }
    }

    IEnumerator PlayWordClipOneByOne(float seconds)
    {
        //new WaitForSeconds(0.5f);
        for (int i = 0; i < correctWords.Count; i++)
        {
            yield return new WaitForSeconds(seconds);
            SoundManager._instance.audioSource.PlayOneShot(correctWords[i]);
            StartCoroutine(CoinsDisplay(seconds));
        }
    }

    IEnumerator CoinsDisplay(float seconds)
    {
        foreach (var coin in whichCondition)
        {
            yield return new WaitForSeconds(seconds);
            coin.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        winningPanel.SetActive(true);
    }

    public void PlayNextWord()
    {
        if (isWinning)
        {
            return;
        }
        StartCoroutine(DelaySec());
        //SoundManager._instance.PlayRandomWordClip(SoundManager._instance.wordClips);
        //currentWord = SoundManager._instance.currentWord;
    }
    IEnumerator DelaySec()
    {
        yield return new WaitForSeconds(1f);
        SoundManager._instance.PlayRandomWordClip(SoundManager._instance.wordClips);
        currentWord = SoundManager._instance.currentWord;
        hintText.text = currentWord.ToLower();
        
    }

    public void RemoveCell(Transform currentCell)
    {
        cells.Remove(currentCell);
    }
}
