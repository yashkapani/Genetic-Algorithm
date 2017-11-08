using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox 
{
	public class TankFitness : MonoBehaviour {


		private float _fitnessValue =0;
		public List<GameObject> pickedMines;
		public Collider2D[] listOfNearestMines;
		public float radius;
		public float TankFitnessValue
		{
			get
			{
				return _fitnessValue;
			}
			set
			{
				_fitnessValue = value;
			}

		}

		void Start()
		{
			pickedMines = new List<GameObject>();
		}

		void Update()
		{
			listOfNearestMines = Physics2D.OverlapCircleAll(transform.position,radius,1 << LayerMask.NameToLayer("Mines") );
			if(listOfNearestMines.Length == 0)
			{
				return;
			}

			float minDist = radius*radius;
			foreach(Collider2D c in listOfNearestMines)
			{
				Vector3 direction = c.transform.position - transform.position;
				float distanceSquare = direction.sqrMagnitude;
				if(distanceSquare< minDist)
				{
					minDist = distanceSquare;
				}
			}

			// inverse normalizing the value
			float inversedistanceFitness = (radius*radius) - minDist;
			_fitnessValue += inversedistanceFitness;


		}

	

		void OnTriggerEnter2D(Collider2D obj)
		{

			if(obj.gameObject.name.Equals("Landmine"))
			{

				if(!pickedMines.Contains(obj.gameObject))
				{
					pickedMines.Add(obj.gameObject);
					_fitnessValue += 300;
				}

			}
		}


		public void ClearMineData()
		{
			pickedMines.Clear();
		}
	}
}