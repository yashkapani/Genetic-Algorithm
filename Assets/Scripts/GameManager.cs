using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


namespace AISandbox {
public class GameManager : MonoBehaviour {


	public Text generationText;
	public Text timeLeftText;
	public Text averageText;
	public Text oldAverageText;

	[SerializeField]
	Transform tankSpawnPoint;

	public List<GameObject> minesList;

	public List<GameObject> tanksList;
	[SerializeField]
	int minesCount ; 

	[SerializeField]
	int tanksCount ; 

	public int GameLoopTimer;
	public int mNumberOfRules;
	
	public int rulesPerSecond;
	
	int mGeneration = 1;
	float newTime ;
	float gameTime;
	float resetCounter =0;

	bool newPopulationGenerated;

	public float averageScore;
	public float totalFitness;


	public GeneticAlgorithm geneticAlgoObj;

	private static GameManager instance;
	public static GameManager Instance
	{
			get{
				return instance;
			}
	}

	void Awake()
	{
			instance = this;
			averageScore =0;
			totalFitness =0;
			GameLoopTimer = 30;
			mNumberOfRules = 120;
			rulesPerSecond = mNumberOfRules/GameLoopTimer;
	}

	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		gameTime = Time.time - resetCounter;
		if(gameTime >= GameLoopTimer && !newPopulationGenerated)
		{
			newPopulationGenerated	 = geneticAlgoObj.GenerateNewPopulation(tanksList);
			mGeneration++;
			ResetTimer();
			ResetTankData();
			return;
		}

		newPopulationGenerated = false;
		newTime =(int) gameTime;

			float t = gameTime / GameLoopTimer;
			int commandIndex = (int)(mNumberOfRules * t);

		{
			for(int i=0;i<tanksList.Count;i++)
			{
				tanksList[i].GetComponent<AITankController>().StepThroughRules(commandIndex);
			}
		}
		CalculateAverage();
		UpdateUI();
	}

	void CalculateAverage()
	{
			totalFitness =0;
			for(int i=0;i<tanksList.Count;i++)
			{
				totalFitness +=	tanksList[i].GetComponent<TankFitness>().TankFitnessValue;
			}
			averageScore = totalFitness /tanksList.Count;
			averageText.text = "Average Score:"+averageScore.ToString();

	}

	void ResetTimer()
	{
		resetCounter = GameLoopTimer * (mGeneration-1);
	}

		void ResetTankData()
	{
			for(int i=0;i<tanksList.Count;i++)
			{

				tanksList[i].GetComponent<IActor>().SetInput(0,0);
				tanksList[i].GetComponent<TankActor>()._orientation = 0.0f;
				tanksList[i].transform.position = tankSpawnPoint.position;
				tanksList[i].GetComponent<TankFitness>().TankFitnessValue = 0;
				tanksList[i].GetComponent<TankFitness>().ClearMineData();
			}

			oldAverageText.text = "Old Average:"+averageScore.ToString();
			totalFitness =0;
			averageScore = 0;
	}


	void UpdateUI()
	{
		generationText.text= "Generation:"+mGeneration;
		timeLeftText.text= "Time Left:"+(GameLoopTimer - newTime);
	}
  }
}
