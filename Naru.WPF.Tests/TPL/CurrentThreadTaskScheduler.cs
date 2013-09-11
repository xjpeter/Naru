﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Naru.WPF.TPL;

namespace Naru.WPF.Tests.TPL
{
    public class CurrentThreadTaskScheduler : DispatcherTaskSchedulerBase
    {
        public override void ExecuteSync(Action action)
        {
            action();
        }

        protected override void QueueTask(Task task)
        {
            TryExecuteTask(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }

        public override int MaximumConcurrencyLevel { get { return 1; } }
    }
}