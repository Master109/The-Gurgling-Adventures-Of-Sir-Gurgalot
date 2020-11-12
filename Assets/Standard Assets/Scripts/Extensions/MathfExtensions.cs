﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassExtensions
{
	public static class MathfExtensions
	{
		public const int NULL_INT = 1234567890;
		public const float NULL_FLOAT = NULL_INT;
		public const float INCHES_TO_CENTIMETERS = 2.54f;
		
		public static float SnapToInterval (float f, float interval)
		{
			return Mathf.Round(f / interval) * interval;
		}
		
		public static int Sign (float f)
		{
			if (f == 0)
				return 0;
			return (int) Mathf.Sign(f);
		}
		
		public static bool AreOppositeSigns (float f1, float f2)
		{
			return Mathf.Abs(Sign(f1) - Sign(f2)) == 2;
		}
		
		public enum RoundingMethod
		{
			HalfOrMoreRoundsUp,
			HalfOrLessRoundsDown,
			RoundUpIfNotWhole,
			RoundDownIfNotWhole
		}

		public static float GetClosestNumber (float f, params float[] numbers)
		{
			float closestNumber = numbers[0];
			float number;
			for (int i = 1; i < numbers.Length; i ++)
			{
				number = numbers[i];
				if (Mathf.Abs(f - number) < Mathf.Abs(f - closestNumber))
					closestNumber = number;
			}
			return closestNumber;
		}

		public static int GetIndexOfClosestNumber (float f, params float[] numbers)
		{
			int indexOfClosestNumber = 0;
			float closestNumber = numbers[0];
			float number;
			for (int i = 1; i < numbers.Length; i ++)
			{
				number = numbers[i];
				if (Mathf.Abs(f - number) < Mathf.Abs(f - closestNumber))
				{
					closestNumber = number;
					indexOfClosestNumber = i;
				}
			}
			return indexOfClosestNumber;
		}
	}
}