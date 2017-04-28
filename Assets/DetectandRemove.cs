/*Use this script to detect whether there are more than X gems in a line*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectandRemove : MonoBehaviour {
    /*Public Variable*/
    public int ThresholdNumToRemove;

    /*Private Variable*/
    private int _boardsize;
    private GameObject[] _gems;
    private ArrayList _gemsToBeRemoved;
	void Start () {
        _boardsize = this.GetComponent<GemGeneretor>().sizeOfBoard;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool Detect()
    {
        bool _detected=false;
        _gems = this.GetComponent<GemGeneretor>().gems;
        _gemsToBeRemoved = new ArrayList();
        for(int i=0;i< _boardsize;i++)
        {
            for(int j=0;j< _boardsize;j++)
            {
                
            }
        }
        return _detected;
    }
}
