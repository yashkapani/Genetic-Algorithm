using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox 
{
	public class FitnessComparer : IComparer<GameObject>
	{
		public int Compare(GameObject other1,GameObject other2)
		{
			float comp1 =	other1.GetComponent<TankFitness>().TankFitnessValue;
			float comp2 =	other2.GetComponent<TankFitness>().TankFitnessValue;
			if(comp1 < comp2) return 1;
			else if (comp1 > comp2) return -1;
			else return 0;

		}
	}
}
