/*Blink the ball*/
using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {
	float cycleTime=1.0f;
	private Color _originalColor;

	void Start(){
        _originalColor = this.GetComponent<MeshRenderer>().material.color;
		cycleTime=Mathf.Abs(cycleTime);
		if(cycleTime==0.0f)cycleTime=1.0f;
	}

	void  Update(){
		float timer=Time.time / cycleTime;
		timer=Mathf.Abs((timer - Mathf.Floor(timer)) * cycleTime - 1);
		this.GetComponent<MeshRenderer>().material.color = Color.Lerp(_originalColor, Color.white, timer );
	}
    private void OnDisable()
    {
        this.GetComponent<MeshRenderer>().material.color = _originalColor;
    }
}
