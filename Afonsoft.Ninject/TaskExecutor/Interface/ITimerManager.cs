using Afonsoft.Ninject.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afonsoft.Ninject.TaskExecutor.Interface
{
    public interface ITimerManager
    {
        /// <summary>
        /// Is Execute
        /// </summary>
        bool IsRunnig { get; }

        /// <summary>
        /// Day of execute
        /// </summary>
        DayOfWeek DayOfWeek { get; }

        /// <summary>
        /// Exucute By
        /// </summary>
        EnumExecuteBy ExecuteBy { get; }

        /// <summary>
        /// Start Hour
        /// </summary>
        int HourStart { get; }

        /// <summary>
        /// End Hour
        /// </summary>
        int HourEnd { get; }

        /// <summary>
        /// Start Minute
        /// </summary>
        int MinuteStart { get; }

        /// <summary>
        /// End Minute
        /// </summary>
        int MinuteEnd { get; }

        /// <summary>
        /// Day of Execute
        /// </summary>
        int Day { get; }

        void Start();

        void Stop();
    }
}
