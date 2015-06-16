using System;
using System.Linq;
using Aplication.Data.Data;
using Aplication.Model;

namespace PermissionMode
{
    public class AdministratorMode: Mode
    {
        public static void StartAdmistratorMode()
        {
            SeeListWithAdministratorscommand();
            string command = Console.ReadLine();
            while (command != "край")
            {
                switch (command)
                {
                    case "1":
                        command = AddPerson(command);
                        break;
                    case "2":
                        command = UpdatePerson(command);
                        break;
                    case "3":
                        command = Help(command);
                        break;
                    case "4"://command "end"
                        command = "край";
                        return;
                    case "5":
                        command = AddDetail(command);
                        break;
                    case "6":
                        command = UpdateDetail(command);
                        break; 
                    case "7":
                        command = AddMachine(command);
                        break;
                    case "8":
                        command = UpdateMachine(command);
                        break;
                    case "9":
                        command = AddPersonComment(command);
                        break;
                    case "10":
                        command = FindCommentForSomePerson(command);
                            break;
                    case "11":
                            command = PersonStatistic(command);
                            break;
                    case "12":
                            command = RemovePerson(command);
                            break;
                    case "13":
                            command = MachineStatistic(command);
                            break;
                    case "14":
                            command = RemoveMachine(command);
                            break;
                    case "15":
                            command = SeeListWithAllPersons(command);
                            break;
                    case "16":
                            command = SeeListWithAllMachines(command);
                            break;
                    case "17":
                            command = SeeListWithAllDetails(command);
                            break;
                    case "18":
                            command = SeeMachineNotWorkLogs(command);
                            break;
                    case "19":
                            command = SeeDetailedReferenceOfSomeObject(command);
                            break;
                    case "20":
                            command = CreateNewAdminsitratorAccount(command);
                            break;
                    case "21":
                            command = CreateNewOperatorAccount(command);
                            break;
                    case "22":
                            command = DeleteOperatorAccount(command);
                            break;
                    default:
                        Console.WriteLine(@"Вие въведохте невалидна команда, ако желаете да излезете от програмата, моля въведете командата ""край""");
                        command = Console.ReadLine();
                        break;
                }
            }
        }

        private static string AddPerson(string command)
        {
            using (var db = new ObjectContext())
            {
                Console.WriteLine("Моля въведете малкото име на работника");
                string firstName = Console.ReadLine();
                Console.WriteLine("Моля въведете фамилията на работника");
                string lastname = Console.ReadLine();
                Person newPerson = new Person(firstName, lastname);
                db.Persons.Add(newPerson);
                db.SaveChanges();
                Console.WriteLine(@"Ако не желаете да въведете в базата данни други работници или да изпълните друга команда, моля въведете командата ""край""!");
                SeeListWithAdministratorscommand();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string UpdatePerson(string command)
        {
            Console.WriteLine("Моля въведете работника за който желаете да промените съществуващата инфомация!");
            using (var db = new ObjectContext())
            {
                try
                {
                    Console.WriteLine("Моля въведете малкото има на работника!");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Моля въведете фамилията на работника");
                    string lastName = Console.ReadLine();
                    var person = db.Persons.First(x => x.FirstName == firstName && x.LastName == lastName && x.IsDelited == false);
                    Console.WriteLine("Моля изберете какво искате да промените");
                    Console.WriteLine("Натиснете 1 ако желаете да промените малкото име!");
                    Console.WriteLine("Натиснете 2 ако желаете да промените фамилното име!");
                    string partToUpdate = Console.ReadLine();
                    while (partToUpdate != "край")
                    {
                        switch (partToUpdate)
                        {
                            case "1":
                                Console.WriteLine("Моля въведете новата стойност!");
                                person.FirstName = Console.ReadLine();
                                db.SaveChanges();
                                partToUpdate = InputNewcommandForUpdatePerson(partToUpdate);
                                break;
                            case "2":
                                Console.WriteLine("Моля въведете новата стойност!");
                                person.LastName = Console.ReadLine();
                                db.SaveChanges();
                                partToUpdate = InputNewcommandForUpdatePerson(partToUpdate);
                                break;
                            default:
                                Console.WriteLine(@"Вие въведохте невалидна команда, ако желаете на излезете от операцията ""промяна на данни за работник"", моля въведете командата ""край""!");
                                partToUpdate = InputNewcommandForUpdatePerson(partToUpdate);
                                break;
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Няма намерен работник в базата данни с въведените от Вас имена!");
                }
            }
            Console.WriteLine(@"Ако желаете да промените данните за друг работник въведете командата ""да"", ако желаете да изпълните друга команда от главното меню натиснете 1 в противен случай въведете командата ""край""!");
            command = Console.ReadLine();
            if (command == "да")
            {
                command = "2";//command will be "update Person"
            }
            else if (command == "1")
            {
                SeeListWithAdministratorscommand();
                command = Console.ReadLine();
            }
            else if (command == "край")
            {
                //do nothing, just return command "край"
            }
            return command;
        }

        private static string AddDetail(string command)
        {
            using (var db = new ObjectContext())
            {
                Console.WriteLine("Моля въведете името на детайла");
                string name = Console.ReadLine();
                Console.WriteLine("Моля въведете времето за струговане на първата страна(в секунди)");
                int timeToTurningOffFirstWall = int.Parse(Console.ReadLine());
                Console.WriteLine("Моля въведете времето за струговане на втората страна(в секунди)");
                int timeToTurningOffSecondWall = int.Parse(Console.ReadLine());
                Console.WriteLine("Моля въведете времето за обработване на проходен отвор(в секунди)");
                int timeToProcessingOffFrontAperture = int.Parse(Console.ReadLine());
                Console.WriteLine("Моля въведете времето за обработване на глух отвор(в секунди)");
                int timeToProcessingOffHollowAperture = int.Parse(Console.ReadLine());
                Console.WriteLine("Моля въведете времето за обработване на глух отвор по оста(в секунди)");
                int timeToProcessingOffHollowApertureSpindle = int.Parse(Console.ReadLine());
                Console.WriteLine("Моля въведете времето за фрезоване(в секунди)");
                int timeOffMilling = int.Parse(Console.ReadLine());
                Console.WriteLine("Моля въведете времето нужно за други операции по детайла(в секунди)");
                int timeForAnotherOperations = int.Parse(Console.ReadLine());
                Detail detail = new Detail(name, timeToTurningOffFirstWall, timeToTurningOffSecondWall, timeToProcessingOffFrontAperture, timeToProcessingOffHollowAperture, timeToProcessingOffHollowApertureSpindle, timeOffMilling, timeForAnotherOperations);
                db.Details.Add(detail);
                db.SaveChanges();
                Console.WriteLine(@"Детайлът е добавен успешно в базата данни!");
                SeeListWithAdministratorscommand();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string UpdateDetail(string command)
        {
            Console.WriteLine(@"Моля въведете ""ID""-то на детайла, за който желаете да промените съществуващата информация!");
            using (var db = new ObjectContext())
            {
                int detailId = int.Parse(Console.ReadLine());
                var detail = db.Details.First(x => x.DetailId == detailId);
                Console.WriteLine("Моля изберете кое свойство на детайла желаете да промените");
                detail.PrintdDetailCharacteristicForUpdate();
                string commandForUpdateDetail = Console.ReadLine();
                while (commandForUpdateDetail != "край")
                {
                    switch (commandForUpdateDetail)
                    {
                        case "1":
                            Console.WriteLine("Въведете новото име на детайла");
                            string name = Console.ReadLine();
                            detail.DetailName = name;
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "2":
                            Console.WriteLine("Въведете новото време за струговане на първата стена(в секунди)!");
                            detail.TimeToTurningOffFirstWall = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "3":
                            Console.WriteLine("Въведете новото време за струговане на втората стена(в секунди)!");
                            detail.TimeToTurningOffSecondWall = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "4":
                            Console.WriteLine("Въведете новото време за обработка на проходен отвор(в секунди)");
                            detail.TimeToProcessingOffFrontAperture = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "5":
                            Console.WriteLine("Въведете новото време за обработка на глух отвор(в секунди)");
                            detail.TimeToProcessingOffHollowAperture = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "6":
                            Console.WriteLine("Въведете новото време за обработка на глух отвор по оста(в секунди)!");
                            detail.TimeToProcessingOffHollowApertureSpindle = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "7":
                            Console.WriteLine("Въведете новото време за фрезоване(в секунди)!");
                            detail.TimeOffMilling = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        case "8":
                            Console.WriteLine("Въведете новото време за други операции по детайла(в секунди)!");
                            detail.TimeForAnotherOperations = int.Parse(Console.ReadLine());
                            commandForUpdateDetail = InputNewCommandForUpdateDetail(commandForUpdateDetail);
                            break;
                        default:
                            Console.WriteLine(@"Вие въведохте невалидна команда, ако желаете да напуснете операция ""промяна на детайл"", моля въведете командата ""край""!");
                            commandForUpdateDetail = Console.ReadLine();
                            break;
                    }
                }
                db.SaveChanges();
            }
            Console.WriteLine(@"Операцията по промяна на дадена информация за съществуващ детайл е извършена успешно!");
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string AddMachine(string command)
        {
            using (var db = new ObjectContext())
            {
                Console.WriteLine("Моля въведете името на машината");
                string name = Console.ReadLine();
                Machine machine = new Machine(name);
                db.Machines.Add(machine);
                db.SaveChanges();
                Console.WriteLine(@"Машината е добавена успешно в азата данни!");
                SeeListWithAdministratorscommand();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string UpdateMachine(string command)
        {
            Console.WriteLine("Моля въведете машината за която желаете да промените съществуващата информация");
            using (var db = new ObjectContext())
            {
                Console.WriteLine("Моля въведете името на машината");
                string name = Console.ReadLine();
                var machine = db.Machines.First(x => x.Name == name);
                Console.WriteLine("Моля въведете новото име!");
                string newName = Console.ReadLine();
                machine.Name = newName;
                db.SaveChanges();
            }
            Console.WriteLine(@"Операцията по промяна на дадена информация на съществуваща машина е извършена успешно!");
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string AddPersonComment(string command)
        {
            Console.WriteLine("Моля въведете работника за когото желаете да създадете нов коментар");
            Console.WriteLine("Моля въведете малкото има на работника!");
            string firstName = Console.ReadLine();
            Console.WriteLine("Моля въведете фамилното име на работника!");
            string lastName = Console.ReadLine();
            using (var db = new ObjectContext())
            {
                Console.WriteLine("Моля въведете коментара за избраният работник:");
                string comment = Console.ReadLine();
                var person = db.Persons.First(x => x.FirstName == firstName && x.LastName == lastName && x.IsDelited == false);
                PersonCommentLog commentLog = new PersonCommentLog(comment, person.PersonId);
                db.PersonCommentLogs.Add(commentLog);
                db.SaveChanges();
                Console.WriteLine(@"Коментарът е добавен успешно в базата данни!");
                SeeListWithAdministratorscommand();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string FindCommentForSomePerson(string command)
        {
            SeeAllExistingPersons();
            Console.WriteLine("Моля въведете ID-то на работника за когото желаете да прегледате съществуващите коментари!");
            int workerId = int.Parse(Console.ReadLine());
            using (var db = new ObjectContext())
            {
                var person =
                    from p in db.Persons
                    join l in db.PersonCommentLogs on p.PersonId equals l.PersonId
                    where p.PersonId == workerId && p.IsDelited == false
                    select new { p.FirstName, p.LastName, l.Coments, l.TimeOffPersonComent };
                Console.WriteLine(@"Моля въведете командата 1 ако желаете да видите всички съществуващи коментари за избраният работник, в противен случай въведете стартова дата от която ще получите всички коментари (във формат Year/Month/Day Hour:Min:Second--пожелание се въвежда часът, минутите и секундите""!");
                string start = Console.ReadLine();
                while (start != "край")
                {
                    if (start == "1" )
                    {
                        foreach (var p in person)
                        {
                            Console.WriteLine("{0} {1}", p.FirstName, p.LastName);
                            Console.WriteLine(@"коментар: ""{0}""", p.Coments);
                            Console.WriteLine("Датата на която е направен коментара: {0}", p.TimeOffPersonComent);
                            Console.WriteLine();
                        }
                        start = "край";//Automatik stop while loop
                    }
                    else
                    {
                        DateTime startTime = DateTime.Parse(start);
                        Console.WriteLine("Моля въведете крайната дата преди която са направени коментарите");
                        DateTime end = DateTime.Parse(Console.ReadLine());
                        foreach (var p in person.Where(x => x.TimeOffPersonComent > startTime && x.TimeOffPersonComent < end))
                        {
                            Console.WriteLine("{0} {1}",p.FirstName,p.LastName);
                            Console.WriteLine(@"коментар: ""{0}""",p.Coments);
                            Console.WriteLine("Дата на която е направен коментара: {0}",p.TimeOffPersonComent);
                            Console.WriteLine();
                        }
                        start = "край";//Automatik stop while loop
                    }
                }
                Console.WriteLine("Това са всички коментари за въведения от Вас работник за определения период!");
                SeeListWithAdministratorscommand();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string Help(string command)
        {
            Console.WriteLine("Моля въведетет командата за която жеалете да видите информация!");
            SeeListWithAdministratorscommand();
            string commandHelp = Console.ReadLine();
            while (commandHelp != "край")
            {
                switch (commandHelp)
                {
                    case "1":
                        Console.WriteLine(@"""добавяне на работник"" командата добавя нов работник в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "2":
                        Console.WriteLine(@"""променете съществуваща информация за работник"" командата ви позволява да промените съществуваща информация за избран работник!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "3":
                        Console.WriteLine(@"""помощ"" командата ви предоставя информация за другите команди");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "4":
                        Console.WriteLine(@"""край"" или ""4"" командата прекъсва изпълняващата се команда");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "край":
                        Console.WriteLine(@"""край"" или ""4"" командата прекъсва изпълняващата се команда");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "5":
                        Console.WriteLine(@"""добавяне на детайл"" командата добавя нов детайл в базата данни");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "6":
                        Console.WriteLine(@"""промени съществуваща информация за детайл"" командата ви позволява да промените съществуващата информация за избран детайл");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "7":
                        Console.WriteLine(@"""добави машина"" командата добавя нова машина в базата данни");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "8":
                        Console.WriteLine(@"""промени съществуваща информация за машина"" командата ви позволява да промените съществуваща информация за избрана машина");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "9":
                        Console.WriteLine(@"""добавете коментар за работник"" командата ви позволява да правите коментари за избран работник");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "10":
                        Console.WriteLine(@"""преглеждане на коментарите за избран работник"" командата ви позволява да прегледате някой или всички коментари за избран работник");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "11":
                        Console.WriteLine(@"""виж статистиката за избран работник"" командата ви позволява да видите статистиката за избран работник за избран период от време");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "12":
                        Console.WriteLine(@"""изтрий рабитник"" командата ви позволява да изтриете избран работник от съществуващата база данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "13":
                        Console.WriteLine(@"""виж статистиката за избрана машина"" командата ви позволява да видите статистиката на избрана машина за посочен период от време!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "14":
                        Console.WriteLine(@"""изтрий машина"" командата ви позволява да изтриете избрана от вас машина от съществуващата база данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "15":
                        Console.WriteLine(@"""виж всички работници"" командата ви позволява да видите списък със всички съществуващи работници в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "16":
                        Console.WriteLine(@"""виж всички машини"" командата ви позволява да видите списък със всички съществуващи машини в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "17":
                        Console.WriteLine(@"""виж всички детайли"" командата ви позволява да видите списък със всички съществуващи детайли в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "18":
                        Console.WriteLine(@"""виж списък на възникнали проблеми и прекъсване на работният процес на машините"" командата ви позволява да видите списък с всички създадени списъци с информация за прекъсване на работният процес на избрана машина!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "19":
                        Console.WriteLine(@"""виж пълна харектеристика за даден обект"" командата ви позволява да видите пълната харектеристика на избран детайл или машина!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "20":
                        Console.WriteLine(@"""прoменете съществуващ администраторски акаунт"" командата ви позволява да ви позволява да промените потребителското име и/или парората на администраторският акаунт, едновремено може да съществува само един администраторски акаунт!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "21":
                        Console.WriteLine(@"""създайте нов операторски акаунт"" командата ви позволява да създадете нов операторски акаунт!");
                        break;
                    case "22":
                        Console.WriteLine(@"""изтриване на операторски акаунт"" командата ви позволява да изтриете съществуващ операторски акаунт!");
                        break;
                    default:
                        Console.WriteLine(@"Въведохте невалидна команда, ако желаете да напуснете текущото меню въведете ""край""!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                }
            }
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string PersonStatistic(string command)
        {
            Mode.SeeAllExistingPersons();
            Console.WriteLine(@"Моля въведете ""ID""-то на работника за когото желаете да видите статистиката!");
            int personId = int.Parse(Console.ReadLine());
            Console.WriteLine("Моля въведете началната дата от която да започват статистическите данни във формат Year/Month/Day");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Моля въведете крайната дата до която да бъдат статистическите данни във формат Year/Month/Day");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            using (var db = new ObjectContext())
            {
                try
                {
                    var personStats =
                        from per in db.Persons
                        where per.PersonId == personId
                        join card in db.CardsForSelfControl on per.PersonId equals card.PersonId
                        where card.StartOperation >= startDate && card.StartOperation <= endDate
                        join det in db.Details on card.DetailId equals det.DetailId
                        where per.IsDelited == false
                        orderby card.StartOperation, (int)card.Change
                        select new { per.PersonId, per.FirstName, per.LastName, card.TypeOffOperations, card.NumberOfMadeDetail, card.StartOperation, card.Change,det.TimeToTurningOffFirstWall,Name = det.DetailName,det.TimeToTurningOffSecondWall,det.TimeToProcessingOffFrontAperture,det.TimeToProcessingOffHollowAperture,det.TimeToProcessingOffHollowApertureSpindle,det.TimeOffMilling,det.TimeForAnotherOperations };
                    //var machineStats =
                    //   from m in db.Machines
                    //   join card in db.CardsForSelfControl on m.MachineId equals card.MachineId
                    //   where card.StartOperation >= startDate && card.StartOperation <= endDate
                    //   join per in db.Persons on card.PersonId equals per.PersonId
                    //   orderby card.StartOperation, (int)card.Change
                    //   select m;
                    //int machineAnTheyWorkerWorks = 0;
                    //foreach (var item in machineStats)
                    //{
                    //    machineAnTheyWorkerWorks++;
                    //}
                    double timeToCreateAllDetails = 0;
                    int countOffAllDetailCreate = 0;
                    double timeForTreatmentOffDetails = 0;
                    Change changeStart = personStats.First().Change;
                    double workTimeForAllPeriod = 0.0;
                    switch (changeStart)
                    {
                        case Change.ПърваСмяна:
                            workTimeForAllPeriod = 8 * 60;
                            break;
                        case Change.ВтораСмяна:
                            workTimeForAllPeriod = 8 * 60;
                            break;
                        case Change.ТретаСмяна:
                            workTimeForAllPeriod = 6.5 * 60;
                            break;
                        case Change.ИзвънреднаСмяна:
                            workTimeForAllPeriod = 6 * 60;
                            break;
                        default:
                            break;
                    }
                    DateTime dateStart = personStats.First().StartOperation;
                    double allTimeNeedetToMakeAllOperation = 0.0;
                    foreach (var person in personStats)
                    {
                        Change curentChange = person.Change;
                        DateTime curentTimeOffCreatingCard = person.StartOperation;
                        if (curentChange != changeStart || dateStart != curentTimeOffCreatingCard)
                        {
                            changeStart = curentChange;
                            switch (person.Change)
                            {
                                case Change.ПърваСмяна:
                                    workTimeForAllPeriod += 8 * 60;
                                    break;
                                case Change.ВтораСмяна:
                                    workTimeForAllPeriod += 8 * 60;
                                    break;
                                case Change.ТретаСмяна:
                                    workTimeForAllPeriod += 6.5 * 60;
                                    break;
                                case Change.ИзвънреднаСмяна:
                                    workTimeForAllPeriod += 6 * 60;
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("{0} {1}:", person.FirstName, person.LastName);
                        Console.WriteLine("броят изработени детайли за смяната на {0} - {1} е {2}!", person.StartOperation, person.Change, person.NumberOfMadeDetail);
                        double workTimeForChange = 0.0;
                        switch (person.Change)
                        {
                            case Change.ПърваСмяна:
                                workTimeForChange = 8 * 60;
                                break;
                            case Change.ВтораСмяна:
                                workTimeForChange = 8 * 60;
                                break;
                            case Change.ТретаСмяна:
                                workTimeForChange = 6.5 * 60;
                                break;
                            case Change.ИзвънреднаСмяна:
                                workTimeForChange = 6 * 60;
                                break;
                            default:
                                break;
                        }
                        switch (person.TypeOffOperations)
                        {
                            case TypeOffOperation.СтругованеНаПърватаСтрана:
                                timeForTreatmentOffDetails = person.TimeToTurningOffFirstWall;
                                allTimeNeedetToMakeAllOperation += (person.TimeToTurningOffFirstWall * person.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.СтругованеНаВторатаСтрана:
                                timeForTreatmentOffDetails = person.TimeToTurningOffSecondWall;
                                allTimeNeedetToMakeAllOperation += person.TimeToTurningOffSecondWall * person.NumberOfMadeDetail;
                                break;
                            case TypeOffOperation.ОбработванеНаПроходенОтвор:
                                timeForTreatmentOffDetails = person.TimeToProcessingOffFrontAperture;
                                allTimeNeedetToMakeAllOperation += (person.TimeToProcessingOffFrontAperture * person.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.ОбработванеНаГлухОтвор:
                                timeForTreatmentOffDetails = person.TimeToProcessingOffHollowAperture;
                                allTimeNeedetToMakeAllOperation += (person.TimeToProcessingOffHollowAperture * person.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.ОбработванеНаГлухОтворПоОста:
                                timeForTreatmentOffDetails = person.TimeToProcessingOffHollowApertureSpindle;
                                allTimeNeedetToMakeAllOperation += (person.TimeToProcessingOffHollowApertureSpindle * person.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.Фрезоване:
                                timeForTreatmentOffDetails = person.TimeOffMilling;
                                allTimeNeedetToMakeAllOperation += (person.TimeOffMilling * person.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.ДругиОперации:
                                timeForTreatmentOffDetails = person.TimeForAnotherOperations;
                                allTimeNeedetToMakeAllOperation += (person.TimeForAnotherOperations * person.NumberOfMadeDetail);
                                break;
                            default:
                                break;
                        }
                        int intDigitForMinuteOfTimeForTreatmentDetails = (int)((timeForTreatmentOffDetails * person.NumberOfMadeDetail) / 60);
                        int intDigitForSecondOfTimeForTreatmentDetails = (int)(timeForTreatmentOffDetails * person.NumberOfMadeDetail) - intDigitForMinuteOfTimeForTreatmentDetails * 60;
                        double percentRoundetToSecondDigitAfterComa = Math.Round((((timeForTreatmentOffDetails / 60) * person.NumberOfMadeDetail) / workTimeForChange) * 100.00, 2);                 
                        Console.WriteLine(@"Реалното работно време за извършването на операцията ""{0}"" на бройката от {1} детайла с име {2} за тази смяна е {3} минути и {4} секунди, което е {5}% от работното време изключващо времето за почивка а именно {6} минути!", person.TypeOffOperations, person.NumberOfMadeDetail, person.Name, intDigitForMinuteOfTimeForTreatmentDetails, intDigitForSecondOfTimeForTreatmentDetails, percentRoundetToSecondDigitAfterComa, workTimeForChange);
                        timeToCreateAllDetails += timeForTreatmentOffDetails * (double)person.NumberOfMadeDetail;
                        countOffAllDetailCreate += person.NumberOfMadeDetail;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Работно време за този интервал е {0} минути", workTimeForAllPeriod);
                    double persent = (((allTimeNeedetToMakeAllOperation / 60.00)) / workTimeForAllPeriod) * 100.00;
                    Console.WriteLine("{0}% е реалното отработено време", Math.Round(persent,2));
                    Console.WriteLine("Това е цялата статистика за този работник за въведеният интервал!");
                    SeeListWithAdministratorscommand();
                    command = Console.ReadLine();
                    return command;
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Няма намерено съвпадение за въведеният период!");
                    SeeListWithAdministratorscommand();
                    command = Console.ReadLine();
                    return command;
                }
            }
        }

        private static string RemovePerson(string command)
        {
            Console.WriteLine(@"Моля въведете малкото име на работника!");
            string firstName = Console.ReadLine();
            Console.WriteLine(@"Моля въведете фамилното име на работника!");
            string lastName = Console.ReadLine();
            using (var db = new ObjectContext())
            {
                var person = db.Persons.First(x => x.FirstName == firstName && x.LastName == lastName);
                person.IsDelited = true;
                db.SaveChanges();
            }
            Console.WriteLine(@"Операцията по изтриване на определен от Вас работник е извършена успешно!");
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string MachineStatistic(string command)
        {
            Mode.SeeAllExistingMachines();
            Console.WriteLine(@"Моля въведете ""ID""-то на машината за която желаете да видите статистиката!");
            int machineId = int.Parse(Console.ReadLine());
            Console.WriteLine("Моля въведете началната дата от която да започват статистическите данни във формат Year/Month/Day");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Моля въведете крайната дата до която да бъдат статистическите данни във формат Year/Month/Day");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            using (var db = new ObjectContext())
            {
                try
                {
                    var machineStats =
                        from m in db.Machines
                        where m.MachineId == machineId
                        join card in db.CardsForSelfControl on m.MachineId equals card.MachineId
                        where card.StartOperation >= startDate && card.StartOperation <= endDate
                        join det in db.Details on card.DetailId equals det.DetailId
                        orderby card.StartOperation, (int)card.Change
                        select new { m.MachineId, m.Name, card.TypeOffOperations, card.NumberOfMadeDetail, card.StartOperation, card.Change, det.TimeToTurningOffFirstWall, det.DetailName, det.TimeToTurningOffSecondWall, det.TimeToProcessingOffFrontAperture, det.TimeToProcessingOffHollowAperture, det.TimeToProcessingOffHollowApertureSpindle, det.TimeOffMilling, det.TimeForAnotherOperations };
                    double timeToCreateAllDetails = 0;
                    int countOffAllDetailCreate = 0;
                    double timeForTreatmentOffDetails = 0;
                    Change changeStart = machineStats.First().Change;
                    double workTimeForAllPeriod = 0.0;
                    switch (changeStart)
                    {
                        case Change.ПърваСмяна:
                            workTimeForAllPeriod = 8 * 60;
                            break;
                        case Change.ВтораСмяна:
                            workTimeForAllPeriod = 8 * 60;
                            break;
                        case Change.ТретаСмяна:
                            workTimeForAllPeriod = 6.5 * 60;
                            break;
                        case Change.ИзвънреднаСмяна:
                            workTimeForAllPeriod = 6 * 60;
                            break;
                        default:
                            break;
                    }
                    DateTime dateStart = machineStats.First().StartOperation;
                    double allTimeNeededToMakeAllOperations = 0.0;
                    foreach (var machine in machineStats)
                    {
                        Change curentChange = machine.Change;
                        DateTime curentTimeOffCreatingCard = machine.StartOperation;
                        if (curentChange != changeStart || dateStart != curentTimeOffCreatingCard)
                        {
                            changeStart = curentChange;
                            switch (machine.Change)
                            {
                                case Change.ПърваСмяна:
                                    workTimeForAllPeriod += 8 * 60;
                                    break;
                                case Change.ВтораСмяна:
                                    workTimeForAllPeriod += 8 * 60;
                                    break;
                                case Change.ТретаСмяна:
                                    workTimeForAllPeriod += 6.5 * 60;
                                    break;
                                case Change.ИзвънреднаСмяна:
                                    workTimeForAllPeriod += 6 * 60;
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("{0}", machine.Name);
                        Console.WriteLine("броят изработени детайли за смяната на {0} - {1} е {2}!", machine.StartOperation, machine.Change, machine.NumberOfMadeDetail);
                        double workTimeForChange = 0.0;
                        switch (machine.Change)
                        {
                            case Change.ПърваСмяна:
                                workTimeForChange = 8 * 60;
                                break;
                            case Change.ВтораСмяна:
                                workTimeForChange = 8 * 60;
                                break;
                            case Change.ТретаСмяна:
                                workTimeForChange = 6.5 * 60;
                                break;
                            case Change.ИзвънреднаСмяна:
                                workTimeForChange = 6 * 60;
                                break;
                            default:
                                break;
                        }
                        switch (machine.TypeOffOperations)
                        {
                            case TypeOffOperation.СтругованеНаПърватаСтрана:
                                timeForTreatmentOffDetails = machine.TimeToTurningOffFirstWall;
                                allTimeNeededToMakeAllOperations += (machine.TimeToTurningOffFirstWall * machine.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.СтругованеНаВторатаСтрана:
                                timeForTreatmentOffDetails = machine.TimeToTurningOffSecondWall;
                                allTimeNeededToMakeAllOperations += machine.TimeToTurningOffSecondWall * machine.NumberOfMadeDetail;
                                break;
                            case TypeOffOperation.ОбработванеНаПроходенОтвор:
                                timeForTreatmentOffDetails = machine.TimeToProcessingOffFrontAperture;
                                allTimeNeededToMakeAllOperations += (machine.TimeToProcessingOffFrontAperture * machine.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.ОбработванеНаГлухОтвор:
                                timeForTreatmentOffDetails = machine.TimeToProcessingOffHollowAperture;
                                allTimeNeededToMakeAllOperations += (machine.TimeToProcessingOffHollowAperture * machine.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.ОбработванеНаГлухОтворПоОста:
                                timeForTreatmentOffDetails = machine.TimeToProcessingOffHollowApertureSpindle;
                                allTimeNeededToMakeAllOperations += (machine.TimeToProcessingOffHollowApertureSpindle * machine.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.Фрезоване:
                                timeForTreatmentOffDetails = machine.TimeOffMilling;
                                allTimeNeededToMakeAllOperations += (machine.TimeOffMilling * machine.NumberOfMadeDetail);
                                break;
                            case TypeOffOperation.ДругиОперации:
                                timeForTreatmentOffDetails = machine.TimeForAnotherOperations;
                                allTimeNeededToMakeAllOperations += (machine.TimeForAnotherOperations * machine.NumberOfMadeDetail);
                                break;
                            default:
                                break;
                        }
                        int intDigitForMinuteOfTimeForTreatmentDetails = (int)((timeForTreatmentOffDetails * machine.NumberOfMadeDetail) / 60);
                        int intDigitForSecondOfTimeForTreatmentDetails = (int)(timeForTreatmentOffDetails * machine.NumberOfMadeDetail) - intDigitForMinuteOfTimeForTreatmentDetails * 60;
                        double percentRoundetToSecondDigitAfterComa = Math.Round((((timeForTreatmentOffDetails / 60) * machine.NumberOfMadeDetail) / workTimeForChange) * 100.00, 2);                 
                        Console.WriteLine(@"Реалното работно време за извършването на операцията ""{0}"" на бройката от {1} детайла с име {2} за тази смяна е {3} минути и {4} секунди, което е {5}% от работното време изключващо времето за почивка а именно {6} минути!", machine.TypeOffOperations, machine.NumberOfMadeDetail, machine.DetailName, intDigitForMinuteOfTimeForTreatmentDetails, intDigitForSecondOfTimeForTreatmentDetails, percentRoundetToSecondDigitAfterComa, workTimeForChange);
                        timeToCreateAllDetails += timeForTreatmentOffDetails * (double)machine.NumberOfMadeDetail;
                        countOffAllDetailCreate += machine.NumberOfMadeDetail;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Работно време за този интервал е {0} минути", workTimeForAllPeriod);
                    double persent = (((allTimeNeededToMakeAllOperations / 60.00)) / workTimeForAllPeriod) * 100.00;
                    Console.WriteLine("{0}% е релното отработено време", Math.Round(persent, 2));
                    Console.WriteLine("Това е цялата статистика за тази машина за въведеният интервал!");
                    SeeListWithAdministratorscommand();
                    command = Console.ReadLine();
                    return command;
                }
                catch (System.InvalidOperationException)
                {
                    Console.WriteLine("Няма намерено съвпадение за въведеният период!");
                    SeeListWithAdministratorscommand();
                    command = Console.ReadLine();
                    return command;
                }
            }
        }

        private static string RemoveMachine(string command)
        {
            Mode.SeeAllExistingMachines();
            int maschieToRemoveId = int.Parse(Console.ReadLine());
            using (var db = new ObjectContext())
            {
                var Machine = db.Machines.First(x => x.MachineId == maschieToRemoveId && x.IsDelited == false);
                Machine.IsDelited = true;
                db.SaveChanges();
            }
            Console.WriteLine(@"Операцията по изтриване на избрана от Вас машина е извършена успешно!");
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string SeeListWithAllPersons(string command)
        {
            Mode.SeeAllExistingPersons();
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string SeeListWithAllMachines(string command)
        {
            Mode.SeeAllExistingMachines();
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string SeeListWithAllDetails(string command)
        {
            Mode.SeeAllExistingDetails();
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string SeeMachineNotWorkLogs(string command)
        {
            Console.WriteLine(@"Моля въведете началната дата от която жеалете да започне справката във формат Year/Month/Day:");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.WriteLine(@"Моля въведете крайната дата до която жеалете да бъде направена справката във формат Year/Month/Day:");
            DateTime end = DateTime.Parse(Console.ReadLine());
            using (var db = new ObjectContext())
            {
                var logs =
                    from l in db.MachinesNotWorkLogs
                    join m in db.Machines on l.MachineId equals m.MachineId
                    where l.StartPeriod >= start && l.EndPeriod <= end 
                    select new { l.EndPeriod, l.ProblemInformation, l.StartPeriod,m.Name};
                foreach (var log in logs)
                {
                    Console.WriteLine();
                    Console.WriteLine("машината {0}:",log.Name);
                    Console.WriteLine("не е работила между {0} часа и {1} часа,", log.StartPeriod, log.EndPeriod);
                    Console.WriteLine("поради следният проблем: {0}",log.ProblemInformation);
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Това е пълният списък със справките за неработещи машини обхващащи въведеният от Вас период!");
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string SeeDetailedReferenceOfSomeObject(string command)
        {
            Console.WriteLine("Моля въведете командата която желаетe да бъде изпълнена!");
            Console.WriteLine(@"Моля натиснете 1 за да се изпълни командата ""виж подробна харектеристика за даден детайл""!");
            Console.WriteLine(@"Моля натиснете 2 за да се изпълни командата ""виж подробна харектерисктика за дадена машина""!");
            string commandObjectChoise = Console.ReadLine();
            while (commandObjectChoise != "край")
            {
                switch (commandObjectChoise)
                {
                    case "1":
                        using (var db = new ObjectContext())
                        {
                            try
                            {
                                Console.WriteLine("Моля въведете името на детайла, на който желаете да видите подробната харектеристика!");
                                string detailName = Console.ReadLine();
                                var detail = db.Details.Where(x => x.DetailName == detailName).First();
                                Console.WriteLine(detail);
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine(@"Няма намерен детайл с въведеното от Вас име, натиснете клавиша ""enter"" за да продължите!");
                                Console.ReadLine();//not take value from console, just use to continue
                            }
                        }
                        Console.WriteLine(@"Ако не желаете да видите подробната харектеристика за друг обект въведете командата ""край""!");
                        commandObjectChoise = Console.ReadLine();
                        break;
                    case "2":
                        using (var db = new ObjectContext())
                        {
                            try
                            {
                                Console.WriteLine("Моля въведете името на машината, за която желаете да видите подробна харектеристика!");
                                string machineName = Console.ReadLine();
                                var machine = db.Machines.Where(x => x.Name == machineName).First();
                                Console.WriteLine(machine);
                            }
                            catch (InvalidOperationException)
                            {
                                Console.WriteLine(@"Няма намерена машина с въведеното от Вас име, натиснете клавиша ""enter"" за да продължите!");
                                Console.ReadLine();//not take value from console, just use to continue
                            }
                        }
                        Console.WriteLine(@"Ако не желаете да видите подробната харектеристика за друг обект въведете командата ""край""!");
                        commandObjectChoise = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine(@"Въведохте невалидна команда, моля въведете желаната команда отново а ако не желаете да видите подробната харектеристика на даден обект, въведете командата ""край""!");
                        commandObjectChoise = Console.ReadLine();
                        break;
                }
            }
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string CreateNewAdminsitratorAccount(string command)
        {
            using (var db = new ObjectContext())
            {
                var administratorUsser = db.AdministartorUssers.First();
                Console.WriteLine(@"Моля въведете новият ""usser name""!");
                string newUsserName = Console.ReadLine();
                Console.WriteLine(@"Моля въведете новата парола!");
                string newPassword = Console.ReadLine();
                administratorUsser.UsserName = newUsserName;
                administratorUsser.Password = newPassword;
                db.SaveChanges();
            }
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string CreateNewOperatorAccount(string command)
        {
            using (var db = new ObjectContext())
            {
                SecurityOperator newOperator = new SecurityOperator();
                Console.WriteLine(@"Моля въведете ""usser name""!");
                string usserName = Console.ReadLine();
                Console.WriteLine(@"Моля въведете ""парола""!");
                string password = Console.ReadLine();
                newOperator.UsserName = usserName;
                newOperator.Password = password;
                db.OperatorsUssers.Add(newOperator);
                db.SaveChanges();
            }
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string DeleteOperatorAccount(string command)
        {
            Console.WriteLine(@"Това е списъкът с всички съществуващи операторски акаунта в базата данни:");
            using (var db = new ObjectContext())
            {
                var listWithAllOperatorsAccount = db.OperatorsUssers.Where(x => x.IsDelited == false).ToList();
                foreach (var usser in listWithAllOperatorsAccount)
                {
                    Console.WriteLine("{0} - {1} {2}",usser.SecurityOperatorId,usser.UsserName,usser.Password);
                }
                Console.WriteLine(@"Моля изберете ID- то на акаунта, който желаете да бъде изтрит!");
                int id = int.Parse(Console.ReadLine());
                var usserToRemove = db.OperatorsUssers.First(x => x.SecurityOperatorId == id);
                usserToRemove.IsDelited = true;
                db.SaveChanges();
            }
            SeeListWithAdministratorscommand();
            command = Console.ReadLine();
            return command;
        }

        private static string commandHelpUpdate(string commandHelp)
        {
            Console.WriteLine(@"Ако желаете да видите информацията за друга команда въведете номера на желаната команда в противен случай въведете като команда ""край""!");
            commandHelp = Console.ReadLine();
            return commandHelp;
        }

        private static void SeeListWithAdministratorscommand()
        {
            Console.WriteLine();
            Console.Write(@"Моля натиснете бутона ""enter"" за да продължите, след което въведете избраната команда от Вас от списъка с команди който ще се изпише на дисплея, след като натиснете бутона ""enter""!");
            Console.ReadLine();//just to go on
            Console.WriteLine("Това е списъка с команди:");
            Console.WriteLine(@"Натиснете 1 за командата ""добавяне на работник""!");
            Console.WriteLine(@"Натиснете 2 за командата ""променете съществуваща информация за работник""!");
            Console.WriteLine(@"Натиснете 3 за командата ""помощ""!");
            Console.WriteLine(@"Натиснете 4 за командата ""край""!");
            Console.WriteLine(@"Натиснете 5 за командата ""добавяне на детайл""!");
            Console.WriteLine(@"Натиснете 6 за командата ""промени съществуваща информация за детайл""!");
            Console.WriteLine(@"Натиснете 7 за командата ""добави машина""!");
            Console.WriteLine(@"Натиснете 8 за командата ""промени съществуваща информация за машина""!");
            Console.WriteLine(@"Натиснете 9 за командата ""добавете коментар за работник""!");
            Console.WriteLine(@"Натиснете 10 за командата ""преглеждане на коментарите за избран работник""!");
            Console.WriteLine(@"Натиснете 11 за командата ""виж статистиката за избран работник""!");
            Console.WriteLine(@"Натиснете 12 за командата ""изтрий работник""!");
            Console.WriteLine(@"Натиснете 13 за командата ""виж статистиката за избрана машина""!");
            Console.WriteLine(@"Натиснете 14 за командата ""изтрий машина""!");
            Console.WriteLine(@"Натиснете 15 за командата ""виж всички работници""!");
            Console.WriteLine(@"Натиснете 16 за командата ""виж всички машини""!");
            Console.WriteLine(@"Натиснете 17 за командата ""виж всички детайли""!");
            Console.WriteLine(@"Натиснете 18 за командата ""виж списък на възникнали проблеми и прекъсване на работният процес на машините""!");
            Console.WriteLine(@"Натиснете 19 за командата ""виж пълна харектеристика за даден обект""!");
            Console.WriteLine(@"Натиснете 20 за командата ""променете съществуващ администраторски акаунт""!");
            Console.WriteLine(@"Натиснете 21 за командата ""създаване на нов операторски акаунт""!");
            Console.WriteLine(@"Натиснете 22 за командата ""изтриване на съществуващ операторски акаунт""!");
        }

        private static string InputNewcommandForUpdatePerson(string partToUpdate)
        {
            Console.WriteLine(@"Ако желаете да промените друго свойство на избраният работник, въведете номера на избраното свойство в противен случай въведете командата ""край""");
            partToUpdate = Console.ReadLine();
            return partToUpdate;
        }

        private static string InputNewCommandForUpdateDetail(string detailUpdatecommand)
        {
            Console.WriteLine(@"Ако желаете да промените друго свойство на детайла, въведете командата за съответното свойство в противен случай въведете командата ""край""!");
            detailUpdatecommand = Console.ReadLine();
            return detailUpdatecommand;
        }
    }
}
