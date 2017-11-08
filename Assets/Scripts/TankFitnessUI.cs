using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace AISandbox 
{
public class TankFitnessUI : MonoBehaviour 
{


	public TankFitness fitnessScore;
	Text fitnessUI;
	string tankeName;
	// Use this for initialization
	void Start () 
	{
		fitnessUI =	GetComponent<Text>();
		tankeName =  fitnessUI.text;
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		fitnessUI.text = tankeName + fitnessScore.TankFitnessValue;
	
	}
}
}