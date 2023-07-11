using System;

namespace DDX.Clock.TimeProviders.Implements
{
    /// <summary>
    /// quicktype.io generated from "http://worldtimeapi.org/api/ip"
    /// </summary>
    public class WorldTimeApiOrgResponse
    {
        public string Abbreviation { get; set; }
        public string ClientIp { get; set; }
        public DateTime Datetime { get; set; }
        public long DayOfWeek { get; set; }
        public long DayOfYear { get; set; }
        public bool Dst { get; set; }
        public object DstFrom { get; set; }
        public long DstOffset { get; set; }
        public object DstUntil { get; set; }
        public long RawOffset { get; set; }
        public string Timezone { get; set; }
        public long Unixtime { get; set; }
        public DateTime UtcDatetime { get; set; }
        public string UtcOffset { get; set; }
        public long WeekNumber { get; set; }
    }

}
