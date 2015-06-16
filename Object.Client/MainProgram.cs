using System;
using System.Linq;
using System.Text;
using Aplication.Data.Data;
using PermissionMode;

namespace Aplication.Client
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            StartProgram();
         }
        public static void StartProgram()
        {
            Console.WriteLine("Здравейте Вие стартирахте WSE manager!");
            Console.WriteLine("Моля изберете Вашите права за работа със системата!");
            Console.WriteLine("Натиснете 1 за режим на администратор!");
            Console.WriteLine("Натиснете 2 за режим на оператор!");
            int permissions = int.Parse(Console.ReadLine());
            switch (permissions)
            {
                case 1:
                    Console.WriteLine(@"Вие избрахте режим ""администратор""!");
                    string usserName = string.Empty;
                    string password = string.Empty;
                    using (var db = new ObjectContext())
                    {
                        var usserAdministrartor = db.AdministartorUssers.First();
                        usserName = usserAdministrartor.UsserName;
                        password = usserAdministrartor.Password;
                        UsserValidation(usserName, password);
                        AdministratorMode.StartAdmistratorMode();
                    }
                    break;
                case 2:
                    Console.WriteLine(@"Вие избрахте режим ""оператор""!");
                    Console.WriteLine(@"Моля въведете вашият ""usser name""!");
                    string inputUsserName = Console.ReadLine();
                    Console.WriteLine(@"Моля въведете вашата ""парола""!");
                    string inputPassword = Console.ReadLine();
                    using (var db = new ObjectContext())
                    {
                        try
                        {
                            var inputOperator = db.OperatorsUssers.Where(n => n.UsserName == inputUsserName && n.Password == inputPassword && n.IsDelited == false).First();
                            Console.WriteLine(@"Въведеното потребителско име и парола са коректни!");
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine(@"Не съществува оператор на програмата с въведените от Вас ""usser name"" и ""парола"", моля рестартирайте програмата отново и въведете коректни данни!");
                            Console.WriteLine(@"Натиснете бутона ""enter"", след което можете да затворите програмата!");
                            Console.ReadLine();//just to go out of program, not used input value from console
                            throw new Exception();
                        }
                    }
                    OperatorMode.StrartOperatorMode();
                    break;
                default:
                   throw new ArgumentException("Вие въведахте невалидна команда, моля РЕСТАРТИРАЙТЕ програмата!");
            }

        }

        public static void UsserValidation(string realUsserName, string realPassword)
        {
            Console.Write(@"Моля въведете Вашето ""потребителско име"":");
            string userName = Console.ReadLine();
            while (userName.ToUpper() != realUsserName.ToUpper())
            {
                Console.WriteLine(@"Вие въведохте невалидно ""потребителско име"", моля въведете валидно ""потребителско име""!");
                userName = Console.ReadLine();
            }
            Console.WriteLine(@"""потребителското име"" е въведено коректно!");
            Console.Write(@"Моля въведете Вашият ""Password"":");
            string password = Console.ReadLine();
            while (password.ToUpper() != realPassword.ToUpper())
            {
                Console.WriteLine(@"Вие въведохте невалиден ""Password"", моля въведете коректен ""Password""!");
                password = Console.ReadLine();
            }
            Console.WriteLine(@"Въведеният ""Password"" е коректен!");
        }
    }
}
