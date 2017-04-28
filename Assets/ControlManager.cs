using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    /*Public variable*/
    public bool CouldChoose=true;
    public GameObject gameboard;
    /*Private variable*/
    private GameObject[] gems;
    private int _size;
    private int[] _indexs;
	void Start () {
        _size = gameboard.GetComponent<GemGeneretor>().sizeOfBoard;
        /*_indexs is used to save the highlighted index of gems*/
        _indexs = new int[4];
        /*Initial all the index to -1*/
        for(int i=0;i<4;i++)
        {
            _indexs[i] = -1;
        }
    }
	
	
	void Update () {
       
        // this code show nameobject with click   
        if (Input.GetMouseButtonDown(0)&& CouldChoose)
        {
            Debug.Log("Pressed left click.");
            //empty RaycastHit object which raycast puts the hit details into
            RaycastHit hit;
            //ray shooting out of the camera from where the mouse is
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,10))
            {
                //print out the name if the raycast hits something
                Debug.Log("hit");
                HighLight(hit.transform.gameObject);


            }
        }
    }
    void HighLight(GameObject ball)
    {
        /*Before highlight the new one, close the highlight of the origin one*/
        DeHighLight();
        gems = gameboard.GetComponent<GemGeneretor>().gems;
        int index = System.Array.IndexOf(gems, ball);

        /*Left one*/
        if(index-1>=0&&(index-1)% _size!= _size-1)
        {
            gems[index - 1].GetComponent<blink>().enabled = true;
            _indexs[0] = index - 1;
        }
        else
            _indexs[0] = -1;

        /*Right one*/
        if (index+1<= _size* _size&& (index+1)%_size!=0)
        {
            gems[index + 1].GetComponent<blink>().enabled = true;
            _indexs[1] = index + 1;
        }
        else
            _indexs[1] = -1;


        /*Upper one*/
        if(index-_size>=0)
        {
            gems[index -_size].GetComponent<blink>().enabled = true;
            _indexs[2] = index - _size;
        }
        else
            _indexs[2] =-1;


        /*Lower one*/
        if (index + _size <=_size*_size)
        {
            gems[index + _size].GetComponent<blink>().enabled = true;
            _indexs[3] = index + _size;
        }
        else
            _indexs[3] = -1;

    }
    void DeHighLight()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_indexs[i] != -1)
            {
                gems[_indexs[i]].GetComponent<blink>().enabled = false;
            }
        }
    }
}
