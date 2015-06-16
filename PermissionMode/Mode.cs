using System;
using System.Linq;
using Aplication.Data.Data;

namespace PermissionMode
{
    public class Mode
    {
        internal static void SeeAllExistingPersons()
        {
            Console.WriteLine("Това е списъка с всички съществуващи работници в базата данни:");
            using (var db = new ObjectContext())
            {
                var listWithPersons = db.Persons.ToList().Where(x => x.IsDelited == false);
                foreach (var person in listWithPersons)
                {
                    Console.WriteLine("{0} {1} {2}", person.PersonId, person.FirstName, person.LastName);
                }
            }
        }
        internal static void SeeAllExistingDetails()
        {
            Console.WriteLine("Това е списъка с всики детайли в базата данни:");
            using (var db = new ObjectContext())
            {
                var listWithDetails = db.Details.ToList();
                foreach (var detail in listWithDetails)
                {
                    Console.WriteLine("{0} {1}", detail.DetailId, detail.DetailName);
                }
            }
        }
        internal static void SeeAllExistingMachines()
        {
            Console.WriteLine("Това е списъка с всички машини в базата данни:");
            using (var db = new ObjectContext())
            {
                var listWithMaschines = db.Machines.ToList().Where(x => x.IsDelited == false);
                foreach (var machine in listWithMaschines)
                {
                    Console.WriteLine("{0} {1}", machine.MachineId, machine.Name);
                }
            }
        }
        internal static void ListWithCardForSelfControlProperty()
        {
            Console.WriteLine(@"""1"" за промяна на броя изработени детайли");
            Console.WriteLine(@"""2"" за промяна името на детайла ");
            Console.WriteLine(@"""3"" за промяна ID -то на машината");
            Console.WriteLine(@"""4"" за промяна ID -то на работника");
            Console.WriteLine(@"""5"" за промяна на смяната");
            Console.WriteLine(@"""6"" промяна на датата на работната карта");
            Console.WriteLine(@"""7"" промяна на типът извършена операция");
        }
    }
}
