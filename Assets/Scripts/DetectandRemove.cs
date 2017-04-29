/*Use this script to detect whether there are more than X gems in a line*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectandRemove : MonoBehaviour {
    /*Public Variable*/

    //Set the min value that could be removed in one line, default is 3.
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
        /*PlayerMode:2 means check mode*/
        if(CurrentControlMode==2)
        {
            /*If there are gems to be removed*/
            if(Detect())
            {
                /*Remove all the gems need to be removed*/
                RemoveandGenerateGems();
                /*enter re-generate mode*/
                ControlManager.GetComponent<ControlManager>().PlayerMode = 3;
            }
            else
            {
                /*If nothing to be removed, then back to player mode*/
                ControlManager.GetComponent<ControlManager>().PlayerMode = 0;
            }

        }

    }

    /*Use this function to detect whether there are gems need to be removed*/
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
                            if (!_gemsToBeRemoved.Contains(_gems[_boardsize * i + j + k]))
                                _gemsToBeRemoved.Add(_gems[_boardsize * i + j + k]);
                        }
                        
                    }
                    j = j + totalNum-1;
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
                    i += totalNum-1;
                }
            }
        }
        return _detected;
    }
    /*Used to compare whether two gems are the same type*/
    private bool CompareTwoGem(GameObject gem0, GameObject gem1)
    {
        if (gem0 == null || gem1 == null)
            return false;
        if (gem0.GetComponent<InitialBall>().type == gem1.GetComponent<InitialBall>().type)
            return true;
        else
            return false;
    }
    /*remove all the gems in the should be removed list*/
    /*Then generate new gems in that place*/
    public void RemoveandGenerateGems()
    {
        while(_gemsToBeRemoved.Count>0)
        {
            GameObject temp = (GameObject)_gemsToBeRemoved[0];
            _gemsToBeRemoved.RemoveAt(0);
            int index= System.Array.IndexOf(_gems, temp);
            StartCoroutine(GenerateNewGems(index,temp.transform.position));
            temp.GetComponent<InitialBall>().playAnimationandDestroy();
        }
        StartCoroutine(backtoCheckMode());

    }
    /*generate new gems*/
    private IEnumerator  GenerateNewGems(int index, Vector3 Prevposition)
    {
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<GemGeneretor>().GenerateOneGem(index, Prevposition);

    }
    /*After regenerate all the new gems, back to check mode*/
    private IEnumerator backtoCheckMode()
    {
        yield return new WaitForSeconds(1.5f);
        ControlManager.GetComponent<ControlManager>().PlayerMode = 2;
    }
}
