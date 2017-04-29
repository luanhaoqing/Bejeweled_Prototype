/*This Script is used to generate the board, initial gems, and resize the board.*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGeneretor : MonoBehaviour {
    /*Public Variable*/
    public GameObject gemPrefab;
    public Material[] Materials;
    public int sizeOfBoard;
    /*Save all the generated gems*/
    public GameObject[] gems;
    /*Private Variable*/
    private int _gemTypeNumber;


	void Start () {
        GenerateGemsandBoard();
    }
	

	void Update () {
		
	}
    /*Generate a game board with different gems*/
    public void GenerateGemsandBoard()
    {
        gems = new GameObject[sizeOfBoard * sizeOfBoard];
        _gemTypeNumber = Materials.Length;
        this.transform.localScale = new Vector3((float)(sizeOfBoard / 10f), (float)(sizeOfBoard / 10f), (float)(sizeOfBoard / 10f));
        for (int i = 0; i < sizeOfBoard; i++)
        {
            for (int j = 0; j < sizeOfBoard; j++)
            {
                GameObject tmp = Instantiate(gemPrefab);
                int randNum = Random.Range(0, _gemTypeNumber);
                tmp.GetComponent<InitialBall>().Initial(Materials[randNum], randNum);
                /*Adjust position according to the boardsize*/
                tmp.transform.position = new Vector3((-sizeOfBoard / 2) + 0.5f + j, (sizeOfBoard / 2) - i - 0.5f, 0);
                gems[sizeOfBoard * i + j] = tmp;
            }
        }
    }
    /*Use this funciton to generate a new gem in the given position, and put the new gem into the gem list*/
    public void GenerateOneGem(int index, Vector3 Targetposition)
    {
        GameObject tmp = Instantiate(gemPrefab);
        int randNum = Random.Range(0, _gemTypeNumber);
        tmp.GetComponent<InitialBall>().Initial(Materials[randNum], randNum);
        tmp.transform.position = Targetposition;
        gems[index] = tmp;
    }
}
