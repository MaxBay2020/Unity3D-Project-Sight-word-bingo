using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Transform> cells = new List<Transform>();
    [SerializeField]
    private string currentWord;
    public static GameManager _instance;
    public List<int> combinations = new List<int>() { 13 };
    private List<List<int>> allWinningConditions = new List<List<int>>()
    {
        new List<int>(){ 1,2,3,4,5 },
        new List<int>(){ 6,7,8,9,10 },
        new List<int>(){ 11,12,13,14,15 },
        new List<int>(){ 16,17,18,19,20 },
        new List<int>(){ 21,22,23,24,25 },

        new List<int>(){ 1,6,11,16,21 },
        new List<int>(){ 2,7,12,17,22 },
        new List<int>(){ 3,8,13,18,23 },
        new List<int>(){ 4,9,14,19,24 },
        new List<int>(){ 5,10,15,20,25 },

        new List<int>(){ 1,7,13,19,25 },
        new List<int>(){ 5,9,13,17,21 }
    };
    public bool isWinning;
    private bool hasCurrentEle;
    private bool isPlayed;
    public Text hintText;
    public GameObject winningPanel;

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
            winningPanel.SetActive(true);
            combinations.RemoveRange(0, combinations.Count);
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
        //if (combinations.Count >= 5)
        //{
        //    foreach (var eachList in allWinningConditions)
        //    {
        //        for (int i = 0; i < eachList.Count; i++)
        //        {
        //            if (!combinations.Contains(eachList[i]))
        //            {
        //                hasAllCurrentListEle = false;
        //                break;
        //            }
                    
        //        }
        //        if (hasAllCurrentListEle)
        //        {
        //            isWinning = true;
        //            return;
        //        }
        //        else
        //        {
        //            isWinning=false;
        //            hasAllCurrentListEle = true;

        //        }

        //    }

        //}

        foreach (Transform currentCell in cells)
        {
            currentCell.gameObject.SetActive(currentCell.name.Split(' ')[0].ToLower().Equals(currentWord.ToLower()));
        }
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
