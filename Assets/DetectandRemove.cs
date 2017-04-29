/*Use this script to detect whether there are more than X gems in a line*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectandRemove : MonoBehaviour {
    /*Public Variable*/
    public int ThresholdNumToRemove;
    public GameObject ControlManager;
    /*Private Variable*/
    private int _boardsize;
    private GameObject[] _gems;
    private ArrayList _gemsToBeRemoved;
	void Start () {
        _boardsize = this.GetComponent<GemGeneretor>().sizeOfBoard;

    }
	
	// Update is called once per frame
	void Update () {
        int CurrentControlMode = ControlManager.GetComponent<ControlManager>().PlayerMode;
        if(CurrentControlMode==2)
        {
            if(Detect())
            {
                RemoveGems();
            }
            else
            {
                ControlManager.GetComponent<ControlManager>().PlayerMode = 0;
            }

        }

    }
    public bool Detect()
    {
        bool _detected=false;
        _gems = this.GetComponent<GemGeneretor>().gems;
        _gemsToBeRemoved = new ArrayList();
        /*Search through line*/
        for(int i=0;i<_boardsize;i++)
        {
            for(int j=0;j<_boardsize-ThresholdNumToRemove+1; j++)
            {
                int index = _boardsize * i + j;
                if (CompareTwoGem(_gems[index],_gems[index+1]))
                {
                    int totalNum = 2;
                    index++;
                    while(index<_boardsize*(i+1)-1)
                    {
                        if(CompareTwoGem(_gems[index], _gems[index + 1]))
                        {
                            index++;
                            totalNum++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if(totalNum>= ThresholdNumToRemove)
                    {
                        _detected = true;
                        for(int k=0;k<totalNum;k++)
                        {
                            _gemsToBeRemoved.Add(_gems[_boardsize * i + j + k]);
                        }
                        
                    }
                }
            }
        }

        /*Search through Column*/
        for (int j = 0; j < _boardsize; j++)
        {
            for (int i = 0; i < _boardsize - ThresholdNumToRemove + 1; i++)
              {
                int index = _boardsize * i + j;
                if (CompareTwoGem(_gems[index], _gems[index + _boardsize]))
                {
                    int totalNum = 2;
                    index+= _boardsize;
                    while (index+_boardsize < _boardsize* _boardsize)
                    {
                        if (CompareTwoGem(_gems[index], _gems[index + _boardsize]))
                        {
                            index+= _boardsize;
                            totalNum++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (totalNum >= ThresholdNumToRemove)
                    {
                        _detected = true;
                        for (int k = 0; k < totalNum; k++)
                        {
                            if(!_gemsToBeRemoved.Contains(_gems[_boardsize * (i + k) + j]))
                                _gemsToBeRemoved.Add(_gems[_boardsize * (i+k) + j]);
                        }

                    }
                }
            }
        }
        return _detected;
    }
    private bool CompareTwoGem(GameObject gem0, GameObject gem1)
    {
        if (gem0 == null || gem1 == null)
            return false;
        if (gem0.GetComponent<InitialBall>().type == gem1.GetComponent<InitialBall>().type)
            return true;
        else
            return false;
    }

    public void RemoveGems()
    {
        while(_gemsToBeRemoved.Count>0)
        {
            GameObject temp = (GameObject)_gemsToBeRemoved[0];
            _gemsToBeRemoved.RemoveAt(0);
            temp.GetComponent<InitialBall>().playAnimationandDestroy();
        }
    }
}
