﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGAOSG
{
	public class Spawnable : MonoBehaviour, ISpawnable
	{
		public int prefabIndex;
		public int PrefabIndex
		{
			get
			{
				return prefabIndex;
			}
		}
	}
}