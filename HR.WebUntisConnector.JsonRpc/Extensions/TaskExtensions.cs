// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HR.WebUntisConnector.JsonRpc.Extensions
{
    public static class TaskExtensions
    {
        public static ConfiguredTaskAwaitable WithoutCapturingContext(this Task task) => task.ConfigureAwait(false);
        public static ConfiguredTaskAwaitable<TResult> WithoutCapturingContext<TResult>(this Task<TResult> task) => task.ConfigureAwait(false);
    }
}
