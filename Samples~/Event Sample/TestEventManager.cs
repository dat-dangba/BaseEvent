using System;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.BaseEvent.Sample
{
    public class TestEventManager : BaseEventManager<TestEventManager>
    {
        [ContextMenu("Reset All Event")]
        private void ResetAllEvent()
        {
            foreach (var item in eventDict)
            {
                GetEvent(item.Value.GetType()).ResetEvent();
            }
        }

        protected override void Start()
        {
            base.Start();
            isUpdateEvent = true;
        }

        protected override DateTime GetDateTimeNow()
        {
            return DateTime.Now.ToLocalTime();
        }

        protected override List<GameEvent> GetEvents()
        {
            return new List<GameEvent>
            {
                new TestDailySchedule(),
                new TestWeekDayEvent(),
                new TestWeeklyEvent(),
                new TestMonthlySchedule(),
                new TestCycleSchedule()
            };
        }
    }
}