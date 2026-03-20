using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using QbGameLib.Task;
using UnityEngine;
using UnityEngine.Pool;

namespace QbGameLib.Cms.ECS
{
    public class EntityMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private int worldIndex = 0;
        [SerializeReference, SubclassSelector] List<IComponent> components;

        private int _entityIndex;

        internal void Bake(ref Entity entity)
        {
            _entityIndex = entity.ID;
            entity._monoBehaviour = this;
            for (var i = 0; i < components.Count; i++)
            {
                typeof(World)
                    .GetMethod("InsertComponent")
                    .MakeGenericMethod(components[i].GetType())
                    .Invoke(World.Get(worldIndex), new object[] { entity, components[i] });
            }

            ref TransformComponent transformComponent = ref entity.World.AddComponent<TransformComponent>(ref entity);
            transformComponent.transform = transform;
        }

        private void OnEnable()
        {
            CancellationTokenSource cancellationTokenSource = UniTaskRunner.CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(10));
            CancellationToken token = cancellationTokenSource.Token;
            Initialize(token);
        }

        private async UniTaskVoid Initialize(CancellationToken token)
        {
            World world = null;
            int frames = 0;
            while (frames<10 && world == null && token.IsCancellationRequested == false)
            {
                world = World.Get(worldIndex);
                await UniTaskRunner.DelayFrame(1);
                frames++;
            }

            if (world != null)
            {
                Bake(ref (world.CreateEntity()));
            }
        }

        private void OnDisable()
        {
            World.Get(worldIndex).RemoveEntity(_entityIndex);
        }

        private void OnDestroy()
        {
            World.Get(worldIndex).RemoveEntity(_entityIndex);
        }
    }
}