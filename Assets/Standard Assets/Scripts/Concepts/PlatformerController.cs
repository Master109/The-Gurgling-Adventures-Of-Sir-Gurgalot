﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Extensions;

namespace TGAOSG
{
	public class PlatformerController : Platformer, IUpdatable
	{
		public bool PauseWhileUnfocused
		{
			get
			{
				return true;
			}
		}
		public float addToMoveAxis;
		bool jumpInput;
		bool previousJumpInput;

		public virtual void OnEnable ()
		{
			GameManager.updatables = GameManager.updatables.Add(this);
		}
		
		public virtual void DoUpdate ()
		{
			if (GameManager.paused)
				return;
			jumpInput = InputManager.Instance.JumpInput;
			move = InputManager.Instance.MoveInput;
			if (Mathf.Abs(move) > InputManager.Settings.defaultDeadzoneMin)
				move += MathfExtensions.Sign(move) * addToMoveAxis;
			else
				move = 0;
			UpdateWhatICanDo ();
			HandleMoving ();
			HandleFacing ();
			HandleIdle ();
			HandleJumping ();
			previousJumpInput = jumpInput;
		}

		public virtual void OnDisable ()
		{
			GameManager.updatables = GameManager.updatables.Remove(this);
		}
		
		public virtual void HandleMoving ()
		{
			if (move != 0)
				Move (move);
			else
			{
				rigid.velocity = rigid.velocity.SetX(0);
				anim.speed = 0;
				anim.Play ("Walk");
			}
		}
		
		public virtual void HandleIdle ()
		{
			if (activityStatus[Activity.Walking].state != ActivityState.Doing)
				Idle ();
			else
				activityStatus[Activity.Idle].state = ActivityState.NotDoing;
		}
		
		public virtual void HandleJumping ()
		{
			if (rigid.velocity.y < 0)
			{
				rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
				if (activityStatus[Activity.Jumping].state == ActivityState.Doing)
					StopJump ();
			}
			else
			{
				if (activityStatus[Activity.Jumping].state == ActivityState.Doing)
				{
					if (!jumpInput && previousJumpInput && activityStatus[Activity.Falling].canDo && activityStatus[Activity.Falling].state != ActivityState.Doing)
						StopJump ();
				}
				else
					rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
			}
			if (jumpInput && !previousJumpInput && activityStatus[Activity.Jumping].canDo && activityStatus[Activity.Jumping].state != ActivityState.Doing && activityStatus[Activity.Falling].state != ActivityState.Doing)
			{
				rigid.velocity += Vector2.up * jumpSpeed;
				StartJump ();
			}
		}
		
		public override void FaceDirection (int direction)
		{
			if (!InputManager.Instance.SwordInput)
				base.FaceDirection (direction);
		}
	}
}