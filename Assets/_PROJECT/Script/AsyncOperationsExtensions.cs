using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class AsyncOperationExtensions
{
    public struct AsyncOperationAwaiter : INotifyCompletion
    {
        private AsyncOperation asyncOperation;
        private Action continuation;

        public AsyncOperationAwaiter(AsyncOperation asyncOperation)
        {
            this.asyncOperation = asyncOperation;
            this.continuation = null;
        }

        public bool IsCompleted => asyncOperation.isDone;

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            asyncOperation.completed += OnOperationCompleted;
        }

        private void OnOperationCompleted(AsyncOperation _)
        {
            continuation?.Invoke();
        }

        public void GetResult() { }
    }

    public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation)
    {
        return new AsyncOperationAwaiter(asyncOperation);
    }
}