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
        /*
		float timer=Time.time / cycleTime;
		timer=Mathf.Abs((timer - Mathf.Floor(timer)) * cycleTime - 1);
		this.GetComponent<MeshRenderer>().material.color = Color.Lerp(_originalColor, Color.white, timer );
        */
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float emission = Mathf.PingPong(Time.time, 0.5f);
        Color baseColor = this.GetComponent<MeshRenderer>().material.color; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
    private void OnDisable()
    {
       // this.GetComponent<MeshRenderer>().material.color = _originalColor;
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetColor("_EmissionColor", Color.black);
    }
}
