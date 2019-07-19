﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace TAoKR.SkillTree
{
	public class MagicCapeCanGlide : Skill
	{
		public new static MagicCapeCanGlide instance;
		
		public override void Start ()
		{
			base.Start ();
			instance = this;
		}
	}
}