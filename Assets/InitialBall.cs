/*This script is used to initial Ball's type and play the initial animation*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBall : MonoBehaviour {
    /*Public variable*/

    /*0 for Red, 1 for blue, 2 for green, 3 for yellow*/
    public int type;
    public float shownSpeed;

    /*Private variable*/
    private bool _isPlayAnimation = false;
    private float _originScale;

	
	void Start () {
        _originScale = this.transform.localScale.x;
        this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
	
	
	void Update () {
        if (!_isPlayAnimation)
        {
            this.transform.localScale *= Time.deltaTime * shownSpeed*60;
            if(this.transform.localScale.x>=_originScale)
            {
                this.transform.localScale = new Vector3(_originScale, _originScale, _originScale);
                _isPlayAnimation = true;
            }
        }

	}
    /*Initial the ball with type and material*/
    public void Initial(Material _material,int _type)
    {
        this.GetComponent<MeshRenderer>().material = _material;
        this.type = _type;
    }
}
