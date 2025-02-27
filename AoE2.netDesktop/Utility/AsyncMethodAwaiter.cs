﻿namespace AoE2NetDesktop.Utility;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Await async methods.
/// </summary>
public class AsyncMethodAwaiter
{
    private readonly object lockObject = new();
    private readonly Dictionary<string, ManualResetEvent> state = new();

    /// <summary>
    /// Notify the completion of the method.
    /// </summary>
    /// <param name="methodName">Completed method name.</param>
    public void Complete([CallerMemberName] string methodName = null)
    {
        lock(lockObject) {
            if(!IsInitState(methodName)) {
                state[methodName] = new ManualResetEvent(false);
            }

            state[methodName].Set();
        }
    }

    /// <summary>
    /// Blocks the current thread until the Complete() is called.
    /// </summary>
    /// <param name="methodName">Method name to wait for completion.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task WaitAsync(string methodName)
    {
        lock(lockObject) {
            if(!IsInitState(methodName)) {
                state[methodName] = new ManualResetEvent(false);
            }
        }

        await Task.Run(() => state[methodName].WaitOne());

        lock(lockObject) {
            state.Remove(methodName);
        }
    }

    private bool IsInitState(string methodName)
    {
        return state.TryGetValue(methodName, out _);
    }
}
