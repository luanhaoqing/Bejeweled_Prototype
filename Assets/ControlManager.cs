using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    /*Public variable*/
    public int PlayerMode=0;//0 for playerMode, 1 for exchange mode, 2 for remove mode, 3 for generate mode
    public GameObject gameboard;
    public float moveSpeed;
    /*Private variable*/
    private GameObject[] gems;
    private int _size;
    private int[] _indexs;
    private GameObject _selectedGem;
    private bool _ischange=false;
    private GameObject _currentGem;
    private Vector3 _currentPos;
    private Vector3 _lastPos;

    void Start () {
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
        if (Input.GetMouseButtonDown(0)&& PlayerMode==0)
        {
            //empty RaycastHit object which raycast puts the hit details into
            RaycastHit hit;
            //ray shooting out of the camera from where the mouse is
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10))
            {
                //print out the name if the raycast hits something
                if (IsNeighbour(_selectedGem, hit.transform.gameObject))
                {
                    DeHighLight();
                    /*Exchange gems*/
                    ExchangeGems(_selectedGem, hit.transform.gameObject);     
                }
                else
                {
                    _selectedGem = hit.transform.gameObject;
                    HighLight(hit.transform.gameObject);
                }
            }
        }

        /*Control of Exchange gems animation*/
        if(_ischange)
        {
            /*set move speed*/
            float step = moveSpeed * Time.deltaTime;
            /*move two gems together*/
           _currentGem.transform.position = Vector3.MoveTowards(_currentGem.transform.position, _lastPos, step);
           _selectedGem.transform.position= Vector3.MoveTowards(_selectedGem.transform.position, _currentPos, step);
            /*after move, set back to choose mode*/
            if(_currentGem.transform.position==_lastPos&& _selectedGem.transform.position==_currentPos)
            {
                _ischange = false;
                PlayerMode = 2;
                _selectedGem = null;
            }
        }



    }

    /*Use this function to high light gems surround the selected gem*/
    void HighLight(GameObject ball)
    {
        _size = gameboard.GetComponent<GemGeneretor>().sizeOfBoard;
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
        if (index + _size <_size*_size)
        {
            gems[index + _size].GetComponent<blink>().enabled = true;
            _indexs[3] = index + _size;
        }
        else
            _indexs[3] = -1;

    }
    /*This function is to de-highlight the highlighted gems*/
    void DeHighLight()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_indexs[i] != -1)
            {
                if(gems[_indexs[i]]!=null)
                    gems[_indexs[i]].GetComponent<blink>().enabled = false;
            }
        }
    }
    /*This function return the boolean value of whether two selected gems are neighbours*/
    bool IsNeighbour(GameObject last, GameObject current)
    {
        if (last == null)
            return false;
        _size = gameboard.GetComponent<GemGeneretor>().sizeOfBoard;
        gems = gameboard.GetComponent<GemGeneretor>().gems;
        int indexOfLast = System.Array.IndexOf(gems, last);
        int indexOfCurrent= System.Array.IndexOf(gems, current);
        if(indexOfCurrent== indexOfLast-1|| indexOfCurrent == indexOfLast + 1|| indexOfCurrent == indexOfLast +_size|| indexOfCurrent == indexOfLast -_size)
        {
            return true;
        }
        return false;
    }

    /*Enchange two gems logically, and then start the animation of exchange*/
    void ExchangeGems(GameObject last, GameObject current)
    {
        //block the choose mode
        PlayerMode = 1;
        //exchange gems logically
        _size = gameboard.GetComponent<GemGeneretor>().sizeOfBoard;
        gems = gameboard.GetComponent<GemGeneretor>().gems;
        int indexOfLast = System.Array.IndexOf(gems, last);
        int indexOfCurrent = System.Array.IndexOf(gems, current);
        GameObject temp = gems[indexOfCurrent];
        gems[indexOfCurrent] = gems[indexOfLast];
        gems[indexOfLast] = temp;
        _currentGem = current;

        //set target move position, and start the animation
        _currentPos = _currentGem.transform.position;
        _lastPos = last.transform.position;
        _ischange = true;
    }

}
