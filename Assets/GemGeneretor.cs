/*This Script is used to generate the board, initial gems, and resize the board.*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGeneretor : MonoBehaviour {
    /*Public Variable*/
    public GameObject gemPrefab;
    public Material[] Materials;
    public int sizeOfBoard;
    /*Private Variable*/
    private int _gemTypeNumber;


	void Start () {
        /*Generate a game board with different gems*/
        _gemTypeNumber = Materials.Length;
        this.transform.localScale = new Vector3((float)(sizeOfBoard / 10f), (float)(sizeOfBoard / 10f), (float)(sizeOfBoard / 10f));
        for (int i=0;i<sizeOfBoard;i++)
        {
            for(int j=0;j<sizeOfBoard;j++)
            {
                GameObject tmp = Instantiate(gemPrefab);
                int randNum = Random.Range(0, _gemTypeNumber);
                tmp.GetComponent<InitialBall>().Initial(Materials[randNum], randNum);
                /*Adjust position according to the boardsize*/
                tmp.transform.position = new Vector3((-sizeOfBoard /2)+0.5f+j, (sizeOfBoard / 2)-i-0.5f,0);
            }
        }
	}
	

	void Update () {
		
	}
}
