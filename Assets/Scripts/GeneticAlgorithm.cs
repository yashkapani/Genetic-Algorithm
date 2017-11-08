using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AISandbox 
{
	public class GeneticAlgorithm : MonoBehaviour 
	{
		
		
		string treadNames;
		
		[SerializeField]
		int numberOfRules;
		
		List<GameObject> oldPopulation;
		public	List<List< MovementRule>> newPopulation;
		
		List<List< MovementRule>> parents;
		List<List< MovementRule>> children;
		
		public int indexValue ;
		
		float elitismPercent= 10f;
		float crossOverProbablity = 0.7f;
		float mutateProbability = 0.1f;
		int eliteCount;
		
		void Start () 
		{
			
			oldPopulation = new List<GameObject>();
			parents = new List<List<MovementRule>>();
			children = new List<List<MovementRule>>();
			
		}
		
		
		public 	bool GenerateNewPopulation(List<GameObject> iOldPopulation )
		{
			oldPopulation.AddRange(iOldPopulation);
			
			//sort according to fitness
			FitnessComparer fintessComp = new FitnessComparer();
			oldPopulation.Sort(fintessComp);
			newPopulation = new List<List< MovementRule>>();
			
			Elitism();
			SelectionChrom();
			Replace();
			
			ClearPopulation();
			return true;
		}
		
		void ClearPopulation()
		{
			newPopulation.Clear();
			parents.Clear();
			children.Clear();
			oldPopulation.Clear();
		}
		
		void Elitism()
		{
			eliteCount = (int)elitismPercent * oldPopulation.Count / 100;
			
			for(int j=0;j< eliteCount;j++)
			{
				newPopulation.Add(oldPopulation[j].GetComponent<AITankController>().GetRuleList());
				oldPopulation.Remove(oldPopulation[j]);
			}
			
		}
		
		void SelectionChrom()
		{

			for(int i=0;i<oldPopulation.Count;i+=2)
			{
				List<MovementRule> parent1 = RouletteWheel();

				List<MovementRule> parent2 = RouletteWheel();
				CrossOver(parent1,parent2);

			}
			
		}
		
		void CrossOver(List<MovementRule> p1,List<MovementRule> p2)
		{

			float prob = UnityEngine.Random.Range(0.0f,1.0f);
			if(prob > crossOverProbablity)
			{
				Mutate(p1,p2);
				return;
			}


			int rulesCount = GameManager.Instance.mNumberOfRules;

				List<MovementRule> childRule1 = new List<MovementRule>();
				for(int j=0;j<rulesCount/2;j++)
				{
					MovementRule parentRule = p1[j];
					childRule1.Add(parentRule);
				}
				for(int k=rulesCount/2;k<rulesCount;k++)
				{
					MovementRule parentRule = p2[k];
					childRule1.Add(parentRule);
				}
				
				List<MovementRule> childRule2 = new List<MovementRule>();
				for(int j=0;j<rulesCount/2;j++)
				{
					MovementRule parentRule = p2[j];
					childRule2.Add(parentRule);
				}
				for(int k=rulesCount/2;k<rulesCount;k++)
				{
					MovementRule parentRule = p1[k];
					childRule2.Add(parentRule);
				}
				
				Mutate(childRule1,childRule2);
			
			
		}

		void Mutate(List<MovementRule> c1,List<MovementRule> c2)
		{
			
			float prob = UnityEngine.Random.Range(0.0f,1.0f);
			if(prob > mutateProbability)
			{
				newPopulation.Add(c1);
				newPopulation.Add(c2);
				return;
			}

				int randomRuleValue = UnityEngine.Random.Range(0,20);
				MovementRule rule;
				if(randomRuleValue >10)
				{
					rule.treadName = "LeftTread";
					rule.treadValue = UnityEngine.Random.Range(-1.0f,1.0f);
				}
				else
				{
					rule.treadName = "RightTread";
					rule.treadValue = UnityEngine.Random.Range(-1.0f,1.0f);
				}
				int randomRule = UnityEngine.Random.Range(0,c1.Count);
				c1[randomRule] = rule;


				randomRuleValue = UnityEngine.Random.Range(0,20);
				if(randomRuleValue >10)
				{
					rule.treadName = "LeftTread";
					rule.treadValue = UnityEngine.Random.Range(-1.0f,1.0f);
				}
				else
				{
					rule.treadName = "RightTread";
					rule.treadValue = UnityEngine.Random.Range(-1.0f,1.0f);
				}
				randomRule = UnityEngine.Random.Range(0,c1.Count);
				c2[randomRule] = rule;

			newPopulation.Add(c1);
			newPopulation.Add(c2);
		}

		public List<MovementRule> GenerateRandomMovementRules()
		{
			numberOfRules = GameManager.Instance.mNumberOfRules;
			List<MovementRule> generatedList = new List<MovementRule>();
			
			for(int i=0;i<numberOfRules;i++)
			{
				MovementRule rule ;
				
				int result =  UnityEngine.Random.Range(1,50);
				
				if(result% 2==0)
					treadNames= "LeftTread";
				else
					treadNames= "RightTread";
				
				rule.treadName = treadNames;
				rule.treadValue = UnityEngine.Random.Range(-1.0f,1.0f);
				generatedList.Add(rule);
			}
			
			return generatedList;
		}

		List<MovementRule> RouletteWheel()
		{
			float totalFitness=0;
			for(int i=0;i<oldPopulation.Count;i++)
			{
				totalFitness += oldPopulation[i].GetComponent<TankFitness>().TankFitnessValue;
			}

			float randomNumber = UnityEngine.Random.Range(0,totalFitness);
			List<MovementRule> chosen = new List<MovementRule>();
			float fitnessSofar=0;
			for(int i=0;i<oldPopulation.Count;i++)
			{

				fitnessSofar +=oldPopulation[i].GetComponent<TankFitness>().TankFitnessValue;
				if(fitnessSofar >= randomNumber)
				{
					chosen = oldPopulation[i].GetComponent<AITankController>().GetRuleList();
					break;
				}
			}

			return chosen;
		}


		void Replace()
		{
			
			for(int i=0;i<newPopulation.Count;i++)
			{
				GameManager.Instance.tanksList[i].GetComponent<AITankController>().SetNewRules(newPopulation[i]);
			}
			
		}
		
		
	}
}