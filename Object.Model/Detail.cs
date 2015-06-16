using System;

namespace Aplication.Model
{
    public class Detail
    {

        public int DetailId { get; set; }
        public string DetailName { get; set; }
        public int TimeToTurningOffFirstWall { get; set; }
        public int TimeToTurningOffSecondWall { get; set; }
        public int TimeToProcessingOffFrontAperture { get; set; }
        public int TimeToProcessingOffHollowAperture { get; set; }
        public int TimeToProcessingOffHollowApertureSpindle { get; set; }
        public int TimeOffMilling { get; set; }
        public int TimeForAnotherOperations { get; set; }


        public Detail(string name, int timeToTurningOffFirstWall, int timeToTurningOffSecondWall, int timeToProcessingOffFrontAperture, int timeToProcessingOffHollowAperture, int timeToProcessingOffHollowApertureSpindle, int timeOffMilling, int timeForAnotherOperations)
        {
            this.DetailName = name;
            this.TimeToTurningOffFirstWall = timeToTurningOffFirstWall;
            this.TimeToTurningOffSecondWall = timeToTurningOffSecondWall;
            this.TimeToProcessingOffFrontAperture = timeToProcessingOffFrontAperture;
            this.TimeToProcessingOffHollowAperture = timeToProcessingOffHollowAperture;
            this.TimeToProcessingOffHollowApertureSpindle = timeToProcessingOffHollowApertureSpindle;
            this.TimeOffMilling = timeOffMilling;
            this.TimeForAnotherOperations = timeForAnotherOperations;
            
        }
        public Detail()
        { }

        public void PrintdDetailCharacteristicForUpdate()
        {
            Console.WriteLine(@"Press ""1"" промяна на името на детайла");
            Console.WriteLine(@"Press ""2"" за времето за струговане на първата страна");
            Console.WriteLine(@"Press ""3"" за времето за струговане на втората страна");
            Console.WriteLine(@"Press ""4"" време за обработка на проходен отвор");
            Console.WriteLine(@"Press ""5"" време за обработка на глух отвор");
            Console.WriteLine(@"Press ""6"" време за обработка на глух отвор по оста");
            Console.WriteLine(@"Press ""7"" време за фрезоване");
            Console.WriteLine(@"Press ""8"" време за обработване на детайла, чрез други операции");
            Console.WriteLine(@"Press ""край"" за изход");
        }

        public override string ToString()
        {
            string toString = string.Format("---------------------------------------------------------\n\r{0}-{1}\n\rВремето за струговане на първата страна е: {2} секунди\n\rВремете за струговане на втората страна е: {3} секунди\n\rВремето за обработка на проходен отвор: {4} секунди\n\rВреме за обработка на глух отвор: {5} секунди\n\rВреме за обработка на глух отвор по оста {6} секунди\n\rВреме за фрезоване: {7} секунди\n\rВреме за извършване на други операции по детайла: {8} секунди\n\r---------------------------------------------------------", this.DetailId, this.DetailName, this.TimeToTurningOffFirstWall, this.TimeToTurningOffSecondWall, this.TimeToProcessingOffFrontAperture, this.TimeToProcessingOffHollowAperture, this.TimeToProcessingOffHollowApertureSpindle, this.TimeOffMilling, this.TimeForAnotherOperations);
            return toString;
        }
    }
}
