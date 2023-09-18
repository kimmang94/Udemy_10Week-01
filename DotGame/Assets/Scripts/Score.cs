using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI text;

    private static int totalscore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        text.text = "Score : " + Player.Instance.score.ToString();
    }
}
