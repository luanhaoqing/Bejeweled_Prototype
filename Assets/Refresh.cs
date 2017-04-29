/*This function is used to refresh the board*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refresh : MonoBehaviour {
    /*Public Variable*/
    public GameObject player;

    /*Private Variable*/
    private Button _mybutton;
    private GameObject[] _gems;
    // Use this for initialization
    void Start () {
        _mybutton = this.GetComponent<Button>();
        _mybutton.onClick.AddListener(delegate { RefreshBoard(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*Refresh the board*/
    private void RefreshBoard()
    {
        /*If not in player select mode, return*/
        if (player.GetComponent<ControlManager>().PlayerMode !=0)
            return;
        /*Destroy all the gems exist*/
        _gems = this.GetComponentInParent<GemGeneretor>().gems;
        for (int i=0;i<_gems.Length;i++)
        {
            _gems[i].GetComponent<InitialBall>().playAnimationandDestroy();
        }
         /*Block the player select mode*/
        player.GetComponent<ControlManager>().PlayerMode = -1;
        /*Start to generate board*/
        StartCoroutine(GenerateNewBoard());
    }
    /*Generate a new board*/
    IEnumerator GenerateNewBoard()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponentInParent<GemGeneretor>().GenerateGemsandBoard();
        StartCoroutine(SetControlModeToDetect());
    }
    /*after generate the new board, change to detect mode*/
    IEnumerator SetControlModeToDetect()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<ControlManager>().PlayerMode = 2;
    }
}
