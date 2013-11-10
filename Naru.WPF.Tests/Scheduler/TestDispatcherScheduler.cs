﻿using System;
using System.Reactive.Concurrency;

using Microsoft.Reactive.Testing;

using Naru.WPF.Scheduler;

using TaskScheduler = System.Threading.Tasks.TaskScheduler;

namespace Naru.WPF.Tests.Scheduler
{
    public class TestDispatcherScheduler : IDispatcherScheduler
    {
        public TaskScheduler TPL { get; private set; }

        public IScheduler RX { get; private set; }

        public void ExecuteSync(Action action)
        {
            action();
        }

        public TestDispatcherScheduler()
        {
            TPL = new CurrentThreadTaskScheduler();
            RX = new TestScheduler();
        }
    }
}