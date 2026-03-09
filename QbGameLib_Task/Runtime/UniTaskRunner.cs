using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace QbGameLib.Task
{
    public class UniTaskRunner
    {
        //ANY
        public static async UniTaskVoid Run(Func<UniTaskVoid> action)
        {
            UniTask.Void(action);
        }
        
        public static async UniTask<T> Get<T>(Func<UniTask<T>> action) where T : class
        {
            T value = await UniTask.Lazy(action);
            return value;
        }
        
        public static async UniTask<T> GetStruct<T>(Func<UniTask<T>> action) where T : struct
        {
            T value = await UniTask.Lazy(action);
            return value;
        }
        
        public static async UniTaskVoid Loop(Func<CancellationToken, UniTask> action, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Create(action, token);
            }
        }
        
        //RESOURCE
        public static async UniTask<T> LoadResource<T>(string path) where T : Object
        {
            var resource = await Resources.LoadAsync<T>(path);
            return (resource as T);
        }
        
        public static async UniTask<T> LoadResource<T>(string path, CancellationToken token) where T : Object
        {
            var resource = await Resources.LoadAsync<T>(path).WithCancellation(token);
            return (resource as T);
        }
        
        //SCENE
        public static async UniTaskVoid LoadScene(string path, LoadSceneMode mode)
        {
            await SceneManager.LoadSceneAsync(path, mode);
        }
        
        public static async UniTaskVoid LoadScene(string path, LoadSceneParameters parameters)
        {
            await SceneManager.LoadSceneAsync(path, parameters);
        }
        
        public static async UniTaskVoid LoadScene(string path, LoadSceneMode mode, CancellationToken token)
        {
            await SceneManager.LoadSceneAsync(path, mode).WithCancellation(token);
        }
        
        public static async UniTaskVoid LoadScene(string path, LoadSceneParameters parameters, CancellationToken token)
        {
            await SceneManager.LoadSceneAsync(path, parameters).WithCancellation(token);
        }
        
        
        //HTTP
        public static async UniTask<UnityWebRequest> HttpGet<T>(Uri uri) where T : Object
        {
            var resource = await UnityWebRequest.Get(uri).SendWebRequest();
            return resource;
        }
        
        public static async UniTask<UnityWebRequest> HttpGet<T>(Uri uri, CancellationToken token) where T : Object
        {
            var resource = await UnityWebRequest.Get(uri).SendWebRequest().WithCancellation(token);
            return resource;
        }
        
        public static async UniTask<UnityWebRequest> HttpPost<T>(Uri uri, string data) where T : Object
        {
            var resource = await UnityWebRequest.PostWwwForm(uri,data).SendWebRequest();
            return resource;
        }
        
        public static async UniTask<UnityWebRequest> HttpPost<T>(Uri uri, Dictionary<string,string> data) where T : Object
        {
            var resource = await UnityWebRequest.Post(uri,data).SendWebRequest();
            return resource;
        }
        
        public static async UniTask<UnityWebRequest> HttpPost<T>(Uri uri, string data, CancellationToken token) where T : Object
        {
            var resource = await UnityWebRequest.PostWwwForm(uri,data).SendWebRequest().WithCancellation(token);
            return resource;
        }
        
        public static async UniTask<UnityWebRequest> HttpPost<T>(Uri uri, Dictionary<string,string> data, CancellationToken token) where T : Object
        {
            var resource = await UnityWebRequest.Post(uri,data).SendWebRequest().WithCancellation(token);
            return resource;
        }
        
        //DELAY
        public static async UniTask DelayFrame(int count,CancellationToken token)
        {
            await UniTask.DelayFrame(count, cancellationToken: token);
        }
        
        public static async UniTask DelayFrame(int count)
        {
            await UniTask.DelayFrame(count);
        }
        
        public static async UniTask DelayTime(TimeSpan time,CancellationToken token)
        {
            await UniTask.Delay(time, cancellationToken: token);
        }
        
        public static async UniTask DelayTime(TimeSpan time)
        {
            await UniTask.Delay(time);
        }
        
        public static async UniTask DelayTime(Func<UniTaskVoid> action, TimeSpan time)
        {
            await UniTask.Delay(time);
            UniTask.Void(action);
        }
        
        public static async UniTask DelayTime(Func<UniTaskVoid> action, TimeSpan time,CancellationToken token)
        {
            await UniTask.Delay(time, cancellationToken: token);
            UniTask.Void(action);
        }
        
        public static async UniTask DelayTimeLoop(Func<UniTaskVoid> action, TimeSpan time, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await DelayTime(action, time, token);
            }
        }
        
        public static async UniTask Wait(Func<bool> action, TimeSpan time, CancellationToken token)
        {
            while (!action.Invoke() && token.IsCancellationRequested == false)
            {
                await DelayTime(time, token);
            }
        }
        
        //CANCELLATION TOKEN
        public static CancellationTokenSource CancellationTokenSource()
        {
            return new CancellationTokenSource();
        }
        
        public static TimeoutController CancellationTokenTimeout()
        {
            return new TimeoutController();
        }
        
    }
}