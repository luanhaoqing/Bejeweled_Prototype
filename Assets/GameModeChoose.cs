using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeChoose : MonoBehaviour {
    /*Public Variable*/
    public GameObject[] buttons;
    public GameObject Gameboard;
    public GameObject uis;
    public int easyModeSize;
   
    /*private Variable*/
    private Button[] _btns;
   
	void Start () {
        _btns = new Button[buttons.Length];
        for (int i=0;i<buttons.Length;i++)
        {
            int _passbyValue = easyModeSize + 2 * (i);
            _btns[i] = buttons[i].GetComponent<Button>();
            _btns[i].onClick.AddListener(delegate { StartGame(_passbyValue); }); 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void StartGame(int num)
    {
        Debug.Log(num);
        Gameboard.GetComponent<GemGeneretor>().sizeOfBoard = num;
        Gameboard.SetActive(true);
        uis.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
