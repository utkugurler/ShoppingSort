using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	#region PUBLIC VARIABLES
	public int Score;
    public Text ScoreText;
    [Space]
    #endregion

    #region PRIVATE VARIABLES
    [SerializeField] private int increasePoint = 5;
    [SerializeField] private int descreasePoint = 3;
    #endregion

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"Score: {Score}";    
    }

    public void IncreasScore()
	{
        Score += increasePoint;
    }

    public void DescreasScore()
	{
        Score -= descreasePoint;

        if(Score < 0)
		{
            Score = 0;
		}
	}


}
