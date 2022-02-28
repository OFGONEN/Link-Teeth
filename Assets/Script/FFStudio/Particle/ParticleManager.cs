/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class ParticleManager : MonoBehaviour
	{
#region Fields
		[ Header( "Event Listeners" ) ]
		public EventListenerDelegateResponse spawnParticleListener;
		public EventListenerDelegateResponse randomParticleListener;

		
		[ SerializeField ] private ParticleEffectPool[] particleEffectPools;
		[ SerializeField ] private RandomParticlePool[] randomParticlePools;
		private Dictionary< string, ParticleEffectPool > particleEffectDictionary;
		private Dictionary< string, RandomParticlePool > randomParticleEffectDictionary;
#endregion

#region UnityAPI

		private void OnEnable()
		{
			spawnParticleListener.OnEnable();
			randomParticleListener.OnEnable();
		}

		private void OnDisable()
		{
			spawnParticleListener.OnDisable();
			randomParticleListener.OnDisable();
		}

		private void Awake()
		{
			spawnParticleListener.response  = SpawnParticle;
			randomParticleListener.response = RandomSpawnParticle;

			particleEffectDictionary = new Dictionary< string, ParticleEffectPool >( particleEffectPools.Length );

			for( int i = 0; i < particleEffectPools.Length; i++ )
			{
				particleEffectPools[ i ].InitPool( transform, false, ParticleEffectStopped );
				particleEffectDictionary.Add( particleEffectPools[ i ].pool_entity.alias, particleEffectPools[ i ] );
			}

			randomParticleEffectDictionary = new Dictionary< string, RandomParticlePool >( randomParticlePools.Length );

			for( var i = 0; i < randomParticlePools.Length; i++ )
			{
				randomParticleEffectDictionary.Add( randomParticlePools[ i ].alias, randomParticlePools[ i ] );
			}
		}
#endregion

#region Implementation
		private void SpawnParticle()
		{
			var spawnEvent = spawnParticleListener.gameEvent as ParticleSpawnEvent;

			ParticleEffectPool pool;

			if( !particleEffectDictionary.TryGetValue( spawnEvent.particle_alias, out pool ) )
			{
				FFLogger.Log( "Particle:" + spawnEvent.particle_alias + " is missing!" );
				return;
			}

			var effect = pool.GetEntity();
			effect.PlayParticle( spawnEvent );
		}

		private void RandomSpawnParticle()
		{
			var spawnEvent = randomParticleListener.gameEvent as ParticleSpawnEvent;

			RandomParticlePool randomPool;

			if( !randomParticleEffectDictionary.TryGetValue( spawnEvent.particle_alias, out randomPool ) )
			{
				FFLogger.Log( "Particle:" + spawnEvent.particle_alias + " is missing!" );
				return;
			}

			var effect = randomPool.GiveRandomEntity();
			effect.PlayParticle( spawnEvent );
		}

		private void ParticleEffectStopped( ParticleEffect particleEffect )
		{
			ParticleEffectPool pool;

			if( !particleEffectDictionary.TryGetValue( particleEffect.alias, out pool ) )
			{
				FFLogger.Log( "Particle:" + particleEffect.alias + " is missing!", particleEffect.gameObject );
				return;
			}

			pool.ReturnEntity( particleEffect );
		}
#endregion
	}
}