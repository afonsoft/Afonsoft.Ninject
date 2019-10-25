using Afonsoft.Ninject.Enum;
using Afonsoft.Ninject.TaskExecutor.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Afonsoft.Ninject.TaskExecutor
{
    public class TaskExecutorTimerManager : ITimerManager
    {
        public bool IsRunnig { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }

        public EnumExecuteBy ExecuteBy { get; private set; }

        public int HourStart { get; private set; }

        public int HourEnd { get; private set; }

        public int MinuteStart { get; private set; }

        public int MinuteEnd { get; private set; }

        public int Day { get; private set; }

        private readonly ITask task;
        private Timer timer;

        public TaskExecutorTimerManager(ITask task, EnumExecuteBy executeBy, int day = -1, int hourStart = -1, int hourEnd = -1, DayOfWeek dayOfWeek = DayOfWeek.Monday, int minuteStart = -1, int minuteEnd = -1)
        {
            this.task = task;
            ExecuteBy = executeBy;
            DayOfWeek = dayOfWeek;
            Day = day;
            HourStart = hourStart;
            HourEnd = hourEnd;
            MinuteStart = minuteStart;
            MinuteEnd = minuteEnd;
        }

        public void Start()
        {
            timer = new Timer(new TimerCallback(this.TimerTick), null, 0, 1000);
        }

        public void Stop()
        {
            IsRunnig = false;
            timer.Dispose();
        }

        private void TimerTick(object state)
        {

            try
            {
                if (IsRunnig)
                    return;

                DateTime dateTime = DateTime.Now;

                if (ExecuteBy == EnumExecuteBy.Immediate ||
                    (ExecuteBy == EnumExecuteBy.Hour && HourStart >= dateTime.Hour && HourEnd <= dateTime.Hour && MinuteStart >= dateTime.Minute && MinuteEnd <= dateTime.Minute) ||
                    (ExecuteBy == EnumExecuteBy.Day && Day == dateTime.Day) ||
                    (ExecuteBy == EnumExecuteBy.DayAndHour && Day == dateTime.Day && HourStart >= dateTime.Hour && HourEnd <= dateTime.Hour && MinuteStart >= dateTime.Minute && MinuteEnd <= dateTime.Minute) ||
                    (ExecuteBy == EnumExecuteBy.Week && this.DayOfWeek == dateTime.DayOfWeek) ||
                    (ExecuteBy == EnumExecuteBy.WeekAndHour && this.DayOfWeek == dateTime.DayOfWeek && HourStart >= dateTime.Hour && HourEnd <= dateTime.Hour && MinuteStart >= dateTime.Minute && MinuteEnd <= dateTime.Minute)
                    )
                {
                    IsRunnig = true;
                    task.Execute();
                }
            }
            finally
            {
                IsRunnig = false;
            }
        }
    }
}