using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCell : MonoBehaviour
{
    public Transform coin;
    private Bounds bounds;
    public Transform coinInside;
    private Vector3 coinOriginalPosition;

    // Start is called before the first frame update
    void Start()
    {
        bounds = this.GetComponent<BoxCollider2D>().bounds;
        coinOriginalPosition = coin.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.isWinning)
        {
            Debug.Log("winning");
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {

            Vector3 coinPosition = coin.position;
            bool inside = bounds.Contains(coinPosition);
            coinInside.gameObject.SetActive(inside);
            // when the coin shows, deactiviate the script
            if (coinInside.gameObject.activeSelf)
            {
                GameManager._instance.combinations.Add(int.Parse(this.gameObject.name.Split(' ')[1]));
                this.GetComponent<WordCell>().enabled = false;

                // remove current word from the list
                GameManager._instance.RemoveCell(this.gameObject.transform);

                GameManager._instance.PlayNextWord();
            }
            // move the coin back to its original position
            coin.position = coinOriginalPosition;

            if (!coinInside.gameObject.activeSelf)
            {
                SoundManager._instance.PlayTryAgainClip();
            }
        }

    }
}
