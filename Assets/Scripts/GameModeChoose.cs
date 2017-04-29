/*Choose level and start the game with paremeter*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeChoose : MonoBehaviour {
    /*Public Variable*/
    public GameObject[] buttons;
    public GameObject Gameboard;
    public GameObject uis;
    /*Set basic length of the board*/
    public int easyModeSize;
   
    /*private Variable*/
    private Button[] _btns;
   
	void Start () {
        _btns = new Button[buttons.Length];
        for (int i=0;i<buttons.Length;i++)
        {
            int _passbyValue = easyModeSize + 2 * (i);
            _btns[i] = buttons[i].GetComponent<Button>();
            _btns[i].onClick.AddListener(delegate { StartGame(_passbyValue,3); }); 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void StartGame(int num, int threshold)
    {
        //set the size of the board 
        Gameboard.GetComponent<GemGeneretor>().sizeOfBoard = num;
        //set the min numbers that could be removed in one line
        Gameboard.GetComponent<DetectandRemove>().ThresholdNumToRemove = threshold;
        Gameboard.SetActive(true);
        uis.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
