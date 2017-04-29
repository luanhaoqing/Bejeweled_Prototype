/*Used for exit the game*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {
    /*Private variable*/
    private Button _mybutton;

	void Start () {
        _mybutton = this.GetComponent<Button>();
        _mybutton.onClick.AddListener(delegate { Exit(); });
    }
	void Update () {
		
	}
    /*Exit the game when press*/
    void Exit()
    {
        Application.Quit();
    }
}
