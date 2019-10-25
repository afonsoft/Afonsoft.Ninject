using Afonsoft.Ninject.Enum;
using Afonsoft.Ninject.TaskExecutor;
using Afonsoft.Ninject.TaskExecutor.Interface;
using Ninject.Modules;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afonsoft.Ninject.NinjectExtension
{
    public class TimerExecutorNinjectTaskModule<T> : NinjectModule where T : ITask
    {
        private readonly DayOfWeek DayOfWeek;
        private readonly EnumExecuteBy ExecuteBy;
        private readonly int HourStart;
        private readonly int HourEnd;
        private readonly int MinuteStart;
        private readonly int MinuteEnd;
        private readonly int Day;
        private readonly string taskName;
        private readonly Dictionary<string, object> constructorArgs;

        
        public TimerExecutorNinjectTaskModule(string taskName, Dictionary<string, object> constructorArgs, EnumExecuteBy executeBy, int day = -1, int hourStart = -1, int hourEnd = -1, DayOfWeek dayOfWeek = DayOfWeek.Monday, int minuteStart = -1, int minuteEnd = -1)
        {
            this.taskName = taskName;
            this.constructorArgs = constructorArgs;
            ExecuteBy = executeBy;
            Day = day;
            HourStart = hourStart;
            HourEnd = hourEnd;
            MinuteStart = minuteStart;
            MinuteEnd = minuteEnd;
            DayOfWeek = dayOfWeek;
        }

        public override void Load()
        {
            IBindingWithOrOnSyntax<T> bindingWithOrOnSyntax = base.Kernel.Bind<ITask>().To<T>().InTaskScope<T>().Named(taskName);

            if (constructorArgs != null)
            {
                foreach (string current in constructorArgs.Keys)
                {
                    bindingWithOrOnSyntax.WithConstructorArgument(current, constructorArgs[current]);
                }
            }

            base.Kernel.Bind<ITask>().To<NinjectTaskScopeManager>()
                .WhenAnyAncestorNamed(this.taskName + "_timerManager")
                .Named(this.taskName + "_ninjectTask")
                .WithConstructorArgument("taskName", taskName);

            base.Kernel.Bind<ITimerManager>().To<TaskExecutorTimerManager>()
                .Named(this.taskName + "_timerManager")
                .WithConstructorArgument("executeBy", ExecuteBy)
                .WithConstructorArgument("day", Day)
                .WithConstructorArgument("hourStart", HourStart)
                .WithConstructorArgument("hourEnd", HourEnd)
                .WithConstructorArgument("dayOfWeek", DayOfWeek)
                .WithConstructorArgument("minuteStart", MinuteStart)
                .WithConstructorArgument("minuteEnd", MinuteEnd);
        }
    }
}
