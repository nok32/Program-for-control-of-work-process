using System;

namespace Aplication.Model
{
    public class MachinesNotWorkLog
    {
        public int MachinesNotWorkLogId { get; set; }
        public string ProblemInformation { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public int MachineId { get; set; }
        public virtual Machine Mashine { get; set; }

        public MachinesNotWorkLog(string problemInformation, DateTime start, DateTime end, int machineId)
        {
            this.ProblemInformation = problemInformation;
            this.StartPeriod = start;
            this.EndPeriod = end;
            this.MachineId = machineId;
        }
    }
}
