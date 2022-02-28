/* Created by and for usage of FF Studios (2021). */

using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ Serializable ]
	public struct TransformData
	{
		public Vector3 position;
		public Vector3 rotation; // Euler angles.
		public Vector3 scale; // Local scale.
	}

	[ Serializable ]
	public struct EventPair
	{
		public EventListenerDelegateResponse eventListener;
		public UnityEvent unityEvent;

		public void Pair()
		{
			eventListener.response = unityEvent.Invoke;
		}
	}

	[ Serializable ]
	public struct ParticleData
	{
		public string alias;
		public bool parent;
		public Vector3 offset;
		public float size;
	}
	
	[ Serializable ]
	public struct AnimationParameterData
	{
		public AnimationParameterType parameterType;
		public string parameter_name;
		[ ShowIf( "parameterType", AnimationParameterType.Bool  ) ] public bool parameter_bool;
		[ ShowIf( "parameterType", AnimationParameterType.Int   ) ] public int parameter_int;
		[ ShowIf( "parameterType", AnimationParameterType.Float ) ] public float parameter_float;
	}

	[ Serializable ]
	public struct PalateToothData
	{
		public int tooth_index;
		public int tooth_health;
		public ToothType tooth_type;
		public Color tooth_color;
	}

	[ Serializable ]
	public struct GridToothData
	{
		public ToothType tooth_type;
		public Color tooth_color;
	}

	[ Serializable ]
	public struct CameraTweenData
	{
		public Transform target;
		public float duration;
		public bool does_tween_position;
		public bool does_tween_rotation;
		[ ShowIf( "does_tween_position" ) ] public Ease ease_position;
		[ ShowIf( "does_tween_rotation" ) ] public Ease ease_rotation;
		public bool always_invoke_complete_event;
		public UnityEvent tween_complete_event;
	}

	[ Serializable ]
	public struct RandomParticlePool
	{
		public string alias;
		public ParticleEffectPool[] particleEffectPools;

		public ParticleEffect GiveRandomEntity()
		{
			return particleEffectPools.ReturnRandom< ParticleEffectPool >().GetEntity();
		}
	}
}
