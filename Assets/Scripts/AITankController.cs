using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AISandbox 
{
	public class AITankController : MonoBehaviour 
	{

		IActor tankActor;

		public List<MovementRule> rulesList;
		public GeneticAlgorithm mGeneticAlgoObj;

		void Awake()
		{

				tankActor =	GetComponent<IActor>();
				rulesList = new List<MovementRule>();
		}
		void Start () 
		{

			rulesList = mGeneticAlgoObj.GenerateRandomMovementRules();

		}
		public List<MovementRule> GetRuleList()
		{
			return rulesList;
		}

		public void SetNewRules(List<MovementRule> iNewRuleList)
		{
			rulesList = iNewRuleList;
		}
		
		public void StepThroughRules(int index)
		{
				string nameToSet = rulesList[index].treadName;
				float valueToSet = rulesList[index].treadValue;
					if(nameToSet.Equals("LeftTread"))
					{
						tankActor.SetInput(valueToSet,0);
					}
					else if(nameToSet.Equals("RightTread"))
					{
						tankActor.SetInput(0,valueToSet);
					}

		}

	 }
}
