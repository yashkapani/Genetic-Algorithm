using UnityEngine;
using System.Collections;

public class MineColor : MonoBehaviour {

	SpriteRenderer render;
	public Color tankOverColor;
	public Color tankOutColor;
	// Use this for initialization
	void Start () 
	{

		render = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		render.color =tankOverColor;
	}
	void OnTriggerExit2D(Collider2D obj)
	{
		render.color =tankOutColor;
	}
}
