using System;

namespace Aplication.Model
{
    public class CardForSelfControl
    {
        private DateTime timeOffCreatingCard;

        public int CardForSelfControlId { get; set; }
        public int NumberOfMadeDetail { get; set; }
        public int DetailId { get; set; }
        public virtual Detail DetailName { get; set; }
        public int MachineId { get; set; }
        public virtual Machine Machine { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public Change Change { get; set; }
        public DateTime StartOperation { get; set; }
        public TypeOffOperation TypeOffOperations { get; set; }
        public DateTime TimeOffCreatingCard
        {
            get
            {
                return this.timeOffCreatingCard;
            }
            private set 
            {
                this.timeOffCreatingCard = value;
            }
        }


        //Constructor
        public CardForSelfControl(int numberOffMadeDetails, int detailId, int machineId, int personId, Change change, DateTime startOperation, TypeOffOperation typeOffOperations)
        {
            this.NumberOfMadeDetail = numberOffMadeDetails;
            this.DetailId = detailId;
            this.MachineId = machineId;
            this.PersonId = personId;
            this.Change = change;
            this.StartOperation = startOperation;
            this.TypeOffOperations = typeOffOperations;
            this.TimeOffCreatingCard = DateTime.Now;
        }
        public CardForSelfControl()
        { }

    }
}
