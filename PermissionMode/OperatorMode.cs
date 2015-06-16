using System;
using System.Linq;
using Aplication.Data.Data;
using Aplication.Model;

namespace PermissionMode
{
    public class OperatorMode:Mode
    {
        public static void StrartOperatorMode()
        {
            ListWithOperatorCommands();
            string command = Console.ReadLine();
            while (command != "край")
            {
                switch (command)
                {
                    case "1":
                        command = InputData(command);
                        break;
                    case "2":
                        SeeAllExistingPersons();
                        ListWithOperatorCommands();
                        command = Console.ReadLine();
                        break;
                    case "3":
                        SeeAllExistingDetails();
                        ListWithOperatorCommands();
                        command = Console.ReadLine();
                        break;
                    case "4":
                        SeeAllExistingMachines();
                        ListWithOperatorCommands();
                        command = Console.ReadLine();
                        break;
                    case "5":
                        command = UpdateInputCard(command);
                        break;
                    case "6":
                        command = Help(command);
                        break;
                    case "7":
                        command = InputMachineNotWorkLog(command);
                        break;
                    default:
                        Console.WriteLine("Вие въведохте невалидна команда, моля въведете командата отново!");
                        command = Console.ReadLine();
                        break;
                }
            }
        }

        private static string InputData(string command)
        {
            Console.WriteLine(@"Моля въведете информация за ""работната карта""!");
            Console.WriteLine(@"Моля натиснете бутона ""enter"" за да продължите!");
            Console.ReadLine();//just for contunie, not use input data from console
            Console.WriteLine("Моля въведете броят на извършените операции върху даден детайл!");
            int detailPiece = int.Parse(Console.ReadLine());

            Console.WriteLine("Моля въведете името на произведеният детайл!");
            string detailName = Console.ReadLine();

            Console.WriteLine("Моля въведете ID-то на машината на която са произведени детайлите");
            SeeAllExistingMachines();
            int machineId = int.Parse(Console.ReadLine());

            Console.WriteLine("Моля въведете ID-то има на работника изработил детайлите!");
            SeeAllExistingPersons();
            int workerID = int.Parse(Console.ReadLine());

            Console.WriteLine("Моля въведете работната смяна, през която са произведени детайлите");
            Console.WriteLine(@"За ""Първа смяна"" натисенете ""1""!");
            Console.WriteLine(@"За ""Втора смяна"" натиснете ""2""!");
            Console.WriteLine(@"За ""Трета смяна"" натиснете ""3""!");
            Console.WriteLine(@"За ""Извънредна смяна "" натиснете ""4""!");
            int change = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine(@"Моля въведете датата на която са извършени операциите във формат ""Year/Month/Day""!");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(@"Моля въведете типът на извършените операции!");
            Console.WriteLine(@"За операция ""струговане на първата страна"" натиснете ""1""!");
            Console.WriteLine(@"За операция ""струговане на втората страна"" натиснете ""2""!");
            Console.WriteLine(@"За операция ""обработване на проходен отвор"" натиснете ""3""!");
            Console.WriteLine(@"За операция ""обработване на глух отвор"" натиснете ""4""!");
            Console.WriteLine(@"За операция ""обработване на глух отвор по оста"" натиснете ""5""!");
            Console.WriteLine(@"За операция ""фрезоване"" натиснете ""6""!");
            Console.WriteLine(@"За операция ""други операции"" натиснете ""7""!");
            int typeOffOperation = int.Parse(Console.ReadLine()) - 1;

            using (var db = new ObjectContext())
            {
                var person = db.Persons.First(x => x.PersonId == workerID && x.IsDelited == false);
                int detailId = db.Details.Where(n => n.DetailName == detailName).Select(x => x.DetailId).First();
                CardForSelfControl card = new CardForSelfControl(detailPiece, detailId, machineId, person.PersonId, (Change)change, date, (TypeOffOperation)typeOffOperation);
                db.CardsForSelfControl.Add(card);
                db.SaveChanges();
                Console.WriteLine(@"""Работна карта"" е въведена успешно в базата данни!");
                ListWithOperatorCommands();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string UpdateInputCard(string command)
        {

            Console.WriteLine("Моля въведете датата във формат Year/Month/Dey на която е въведена работната карта в базата данни!");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            DateTime endDate = startDate.AddHours(24.00);

            using (var db = new ObjectContext())
            {
                try
                {
                    var allCardWhoAreEnteredAnThisDate = db.CardsForSelfControl.Where(x => x.TimeOffCreatingCard >= startDate && x.TimeOffCreatingCard <= endDate).ToList();
                   
                    if (allCardWhoAreEnteredAnThisDate.Count <= 0)
                    {
                        throw new ArgumentNullException("noWorkCardsAnThisDate", "Няма въведени работни карти на тази дата!");
                    }
                    
                    Console.WriteLine("Това са картите които са въведени на тази дата:");
                    
                    foreach (var cards in allCardWhoAreEnteredAnThisDate)
                    {
                        Console.WriteLine("{0} {1} информацията  от картата е, че са извършени операции върху детайл с име {2}!", cards.CardForSelfControlId, cards.TimeOffCreatingCard, cards.DetailName.DetailName);
                    }
                   
                    Console.WriteLine("Моля въведете ID-то на работната карта, върху която желаете да направите промяна на съществуващата информация!");
                    
                    int cardId = int.Parse(Console.ReadLine());
                    var card = db.CardsForSelfControl.First(x => x.CardForSelfControlId == cardId);
                    
                    Console.WriteLine("Това е списъка с команди за промяна, моля изберете номера на командата, която желаете да се изпълни!");
                   
                    ListWithCardForSelfControlProperty();
                   
                    string propertyForChange = Console.ReadLine();
                    
                    while (propertyForChange != "край")
                    {
                        switch (propertyForChange)
                        {
                            case "1":
                                Console.WriteLine("Моля въведете новата стойност!");
                                card.NumberOfMadeDetail = int.Parse(Console.ReadLine());
                                break;
                            case "2":
                                Console.WriteLine("Моля въведете новата стойност!");
                                string detailName = Console.ReadLine();
                                int detailIdNew = db.Details.Where(n => n.DetailName == detailName).Select(i => i.DetailId).First();
                                card.DetailId = detailIdNew;
                                break;
                            case "3":
                                Console.WriteLine("Моля въведете новата стойност!");
                                card.MachineId = int.Parse(Console.ReadLine());
                                break;
                            case "4":
                                Console.WriteLine("Моля въведете новата стойност!");
                                card.PersonId = int.Parse(Console.ReadLine());
                                break;
                            case "5":
                                Console.WriteLine(@"Натиснете 1 за първа смяна!");
                                Console.WriteLine(@"Натиснете 2 за втора смяна!");
                                Console.WriteLine(@"Натиснете 3 за трета смяна!");
                                Console.WriteLine(@"Натиснете 4 за извънредна смяна!");
                                int changeIndex = int.Parse(Console.ReadLine()) - 1;
                                card.Change = (Change)changeIndex;
                                break;
                            case "6":
                                Console.WriteLine("Моля въведете новата дата на работната карта във формат Year/Month/Day/");
                                card.StartOperation = DateTime.Parse(Console.ReadLine());
                                break;
                            case "7":
                                Console.WriteLine(@"Моля въведете типът операция:");
                                Console.WriteLine(@"За типът операция ""струговане на първата страна"" натиснете ""1""!");
                                Console.WriteLine(@"За типът операция ""струговане на втората страна"" натиснете ""2""!");
                                Console.WriteLine(@"За типът операция ""обработка на проходен отвор"" натиснете ""3""!");
                                Console.WriteLine(@"За типът операция ""обработка на глух отвор"" натиснете ""4""!");
                                Console.WriteLine(@"За типът операция ""обработка на глух отвор по оста"" натиснете ""5""!");
                                Console.WriteLine(@"За типът операция ""фрезоване"" натиснете ""6""!");
                                Console.WriteLine(@"За типът операция ""други операции"" натиснете ""7""!");
                               
                                int typeOffOperation = int.Parse(Console.ReadLine()) - 1;
                                card.TypeOffOperations = (TypeOffOperation)typeOffOperation;
                                
                                break;
                            default:
                                Console.WriteLine("Вие въведохте невалидна команда!");
                                break;
                        }
                       
                        db.SaveChanges();
                        Console.WriteLine(@"Ако сте приключили с промените по работната карта въведете командата ""край"" в противен случай въведете номера на желаната команда!");
                        ListWithCardForSelfControlProperty();
                        propertyForChange = Console.ReadLine();
                    }
                }
                catch (ArgumentNullException a)
                {
                    Console.WriteLine("{0} - {1}",a.ParamName,a.Message);
                    Console.WriteLine(@"Моля натиснете бутона ""enter"" след което затворете програмата!");
                    Console.ReadLine();
                    throw new Exception();
                }
            }
           
            Console.WriteLine(@"""Работната карта е актуализирана успешно!");
            ListWithOperatorCommands();
            command = Console.ReadLine();
            return command;
        }

        private static string InputMachineNotWorkLog(string command)
        {
            Console.WriteLine("Моля въведете информация за проблема!");
            string information = Console.ReadLine();
           
            Console.WriteLine("Моля въведете датата и началният час на проблема във формат Year/Month/Day Hour:Minute:Second!");
            DateTime start = DateTime.Parse(Console.ReadLine());
            
            Console.WriteLine("Моля въведете датата и крайният час на проблема във формат Year/Month/Day Hour:Minute:Second!");
            DateTime end = DateTime.Parse(Console.ReadLine());
            
            Console.WriteLine(@"Моля въведете ID- то на машината, която е имала проблем в работният процес!");
            Mode.SeeAllExistingMachines();
            int id = int.Parse(Console.ReadLine());
            MachinesNotWorkLog machineNotWorkLog = new MachinesNotWorkLog(information, start, end, id);
           
            using (var db = new ObjectContext())
            {
                db.MachinesNotWorkLogs.Add(machineNotWorkLog);
                db.SaveChanges();
                Console.WriteLine(@"""бележка относно проблем в работният процес на дадена машина"" е създадена успешно!");
                ListWithOperatorCommands();
                command = Console.ReadLine();
            }
            return command;
        }

        private static string Help(string command)
        {
            Console.WriteLine("Моля въведете командата за която имате нужда да видите допълнителна информация!");
            ListWithOperatorCommands();
            string commandHelp = Console.ReadLine();
           
            while (commandHelp != "край")
            {
                switch (commandHelp)
                {
                    case "1":
                        Console.WriteLine(@"""въвеждане на нова работна карта"" командата създава нова работна карта в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "2":
                        Console.WriteLine(@"""за да видите всички съществуващи работници в базата данни"" команда ви позволява да видите наличната информация за всички съществуващи работници в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "3":
                        Console.WriteLine(@"""за да видите всички съществуващи детайли в базата данни"" команда ви позволява да видите наличната информация за всички съществуващи детайли в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "4":
                        Console.WriteLine(@"""за да видите всички съществуващи машини в базата данни"" команда ви позволява да видите наличната информация за всички съществуващи машини в базата данни!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "5":
                        Console.WriteLine(@"""за да направите промяна в съществуваща работна карта"" команда ви позволява да промените данните в съществуваща работна карта!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "6":
                        Console.WriteLine(@"""помощ"" команда ви позволява да видите допълнителна информация за всички команди!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    case "7":
                        Console.WriteLine(@"""за да въведете бележка относно проблем в работният процес на дадена машина"" команда ви позволява да направите бележка, която обяснява принудителното спиране или неучастване на дадена машина в работният процес!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                    default:
                        Console.WriteLine("Въвели сте невалидна команда, моля въведете командата която желаете да се изпълни отново!");
                        commandHelp = commandHelpUpdate(commandHelp);
                        break;
                }
            }
            
            ListWithOperatorCommands();
            Console.WriteLine("Моля въведете вашата команда:");
            command = Console.ReadLine();
            return command;
        }

        private static string commandHelpUpdate(string commandHelp)
        {
            Console.WriteLine(@"Ако желаете да видите информация за друга команда, моля въведете нейният номер в противен случай въведтете командата ""край""!");
            commandHelp = Console.ReadLine();
            return commandHelp;
        }

        private static void ListWithOperatorCommands()
        {
            Console.WriteLine();
            Console.Write(@"Моля натиснете бутона ""enter"" за да продължите, след което въведете избраната команда от Вас от списъка с команди който ще се изпише на дисплея, след като натиснете бутона ""enter""!");
            Console.ReadLine();//just to continue
            Console.WriteLine("Това е списъка с команди:");
            Console.WriteLine("Моля въведете командата, която желаете да бъде изпълнена:");
            Console.WriteLine(@"Натиснете ""край"" за изход");
            Console.WriteLine(@"Натиснете 1  за въвеждане на нова ""работна карта""!");
            Console.WriteLine(@"Натиснете 2 за да видите списък с всички работници въведени в базата данни!");
            Console.WriteLine(@"Натиснете 3 за да видите списък с всички детайли въведени в базата данни!");
            Console.WriteLine(@"Натиснете 4 за да видите списък с всички машини въведени в базата данни!");
            Console.WriteLine(@"Натиснете 5 за да направите промяна в съществуваща ""работна карта""!");
            Console.WriteLine(@"Натиснете 6 за да използвате командата ""помощ""!");
            Console.WriteLine(@"Натиснете 7 за да създадете бележка за престой на машината!");
        }
    }
}
