using System;

namespace MatchHut.Infrastructure.Paging
{
    public class TimesheetFilter : Filter
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class TimesheetResourceParams : PageResourceParams<TimesheetFilter>
    {
        public DateTime? StartTime => _filterObj?.StartTime;
        public DateTime? EndTime => _filterObj?.EndTime;
    }
}