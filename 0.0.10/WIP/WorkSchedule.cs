using System;
using System.Collections.Generic;

namespace zmDataTypes.Scheduling
{
    public class WorkSchedule
    {
        /// <synopsis>
        /// Parametric class for calculating crew counts, overtime needs, duration, etc.
        /// </synopsis>

        /// <note>
        /// This class is a work in progress. It is intended to be used for calculating
        /// the number of workers needed to complete a given amount of work within a
        /// specified time frame, taking into account the work schedule and hours per week.
        /// 
        /// It should make use of fluent interfaces for setting parameters.
        /// It should also prevent setting parameters that are not compatible with each other,
        /// or would overconstrain the calculation.
        /// </note>

        /// <param name="totalWorkHours">Total hours of work to be done.</param>
        /// <param name="timeSpans">List of TimeSpan objects representing the work schedule.</param>
        /// <param name="hoursPerWeek">Number of hours worked per individual per week.</param>

        private double _totalWorkHours; 
        private List<TimeSpan> _timeSpans = new List<TimeSpan>();
        private double _hoursPerWeek;

        public WorkSchedule()
        {
        }

        public WorkSchedule TotalWorkHours(double totalWorkHours)
        {
            _totalWorkHours = totalWorkHours;
            return this;
        }

    }
}