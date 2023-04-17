using ConsoleApp10;
using System.Linq.Expressions;

bool AppIsOn = true;
int input;
string firstName;
string lastName;
string patronomic;
int age;
string role;
string account;
decimal balance;

while (AppIsOn)
{
    while (true)
    {
        Console.WriteLine("\n\tСделайте выбор\n" +
        "1 - Вывести список пользователей из базы\n" +
        "2 - Добавить пользователя\n" +
        "3 - Удалить пользователя\n" +
        "4 - Изменить данные пользователя\n" +
        "0 - Выход");

        if (int.TryParse(Console.ReadLine(), out input) && input >= 0 && input <= 4)
            break;
        else
            Console.WriteLine("Ошибка ввода");
    }



    switch ((MainMenu)input)
    {
        case MainMenu.Exit:
            AppIsOn = false;
            break;


        case MainMenu.ExportUsers:
            using (BankClientsContext db = new BankClientsContext())
            {
                foreach (User u in db.Users.ToList())
                    Console.WriteLine(
                        $"Id: {u.Id}\nName: {u.LastName} {u.FirstName} {u.PatronomicName}\nAge: {u.Age}\nRole: {u.Role}\nAccount: {u.Account}\nBalance: {u.Balance}");
            }
            break;


        case MainMenu.AddUser:
            int add = 1;
            List<User> users = new List<User>();

            while (add != 0)
            {
                Console.WriteLine("\nВведите Имя: ");
                firstName = Console.ReadLine() ?? "no first_name";

                Console.WriteLine("Введите Фамилию: ");
                lastName = Console.ReadLine() ?? "no last_name";

                Console.WriteLine("Введите Отчество: ");
                patronomic = Console.ReadLine() ?? "no patronomic";
                                
                while (true)
                {
                    Console.WriteLine("\nВведите Возраст: ");
                    if (int.TryParse(Console.ReadLine(), out age))
                        break;
                    else
                        Console.WriteLine("Ошибка ввода");
                }

                Console.WriteLine("Введите Роль пользователя (Клиент, сотрудник и т.д.): ");
                role = Console.ReadLine() ?? "no role";

                Console.WriteLine("Введите IBAN: ");
                account = Console.ReadLine() ?? "no patronomic";
                                
                while (true)
                {
                    Console.WriteLine("\nВведите Баланс на счете: ");
                    if (decimal.TryParse(Console.ReadLine(), out balance))
                        break;
                    else
                        Console.WriteLine("Ошибка ввода");
                }

                while (true)
                {
                    Console.WriteLine("Введите 1 чтобы добавить еще одного пользователя или 0 для окончания ввода и записи в базу данных ");
                    if (int.TryParse(Console.ReadLine(), out add) && (add == 1 || add == 0))
                        break;
                    else
                        Console.WriteLine("Ошибка ввода");
                }

                users.Add(new User
                {
                    Id = GetNextId(),
                    FirstName = firstName,
                    LastName = lastName,
                    PatronomicName = patronomic,
                    Age = age,
                    Role = role,
                    Account = account,
                    Balance = balance
                });

                if (add == 0)
                {
                    if(AddToTableDB(users))
                        Console.WriteLine("Пользователи добавлены в базу данных");
                    else
                        Console.WriteLine("Ошибка при добавлении, пользователь не добавлен в базу");
                }
            }
            break;


        case MainMenu.RemoveUser:
            while (true)
            {
                Console.WriteLine("Введите Id пользователя для удаления из базы");

                if (int.TryParse(Console.ReadLine(), out input))
                    break;
                else
                    Console.WriteLine("Ошибка ввода");
            }

            if (RemoveUserFromDBTable(input))             
                Console.WriteLine("Пользователь удален");
            else
                Console.WriteLine("Ошибка при удалении, пользователь не удален");
            break;


        case MainMenu.UpdateUser:
            while (true)
            {
                Console.WriteLine("\n\tВыберите поле для изменения:\n" +
                "1 - Фамилия\n" +
                "2 - Имя\n" +
                "3 - Отчество\n" +
                "4 - Возраст\n" +
                "5 - Роль\n" +
                "6 - Номер счета\n" +
                "7 - Баланс на счете\n" +
                "0 - Выход в главное меню");

                if (int.TryParse(Console.ReadLine(), out input) && input >= 0 && input <= 7)
                    break;
                else
                    Console.WriteLine("Ошибка ввода");
            }

            int id;

            while (true)
            {
                Console.WriteLine("Введите Id пользователя, которого хотите изменить");

                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                else
                    Console.WriteLine("Ошибка ввода");
            }

            if (!IdExist(id))
            {
                Console.WriteLine($"Пользователь с Id = {id} не найден в таблице");
                input = 0;
            }

            switch ((UpdateMenu)input)
            {
                case UpdateMenu.Exit:
                    break;


                case UpdateMenu.FirstName:
                    Console.WriteLine("\nВведите новое Имя: ");
                    firstName = Console.ReadLine() ?? "no_first_name";

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if(u.Id == id)
                            {
                                u.FirstName = firstName;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }                                
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                case UpdateMenu.LastName:
                    Console.WriteLine("\nВведите новую фамилию: ");
                    lastName = Console.ReadLine() ?? "no_last_name";

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if (u.Id == id)
                            {
                                u.LastName = lastName;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                case UpdateMenu.Patronomic:
                    Console.WriteLine("\nВведите новое отчество: ");
                    patronomic = Console.ReadLine() ?? "no_patronomic";

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if (u.Id == id)
                            {
                                u.PatronomicName = patronomic;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                case UpdateMenu.Age:
                    while (true)
                    {
                        Console.WriteLine("\nВведите новый возраст: ");
                        if (int.TryParse(Console.ReadLine(), out age))
                            break;
                        else
                            Console.WriteLine("Ошибка ввода");
                    }

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if (u.Id == id)
                            {
                                u.Age = age;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                case UpdateMenu.Role:
                    Console.WriteLine("\nВведите новую Роль: ");
                    role = Console.ReadLine() ?? "no_role";

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if (u.Id == id)
                            {
                                u.Role = role;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                case UpdateMenu.Account:
                    Console.WriteLine("\nВведите новый номер счета: ");
                    account = Console.ReadLine() ?? "no_account";

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if (u.Id == id)
                            {
                                u.Account = account;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                case UpdateMenu.Balance:
                    while (true)
                    {
                        Console.WriteLine("\nВведите новый баланс на счете: ");
                        if (decimal.TryParse(Console.ReadLine(), out balance))
                            break;
                        else
                            Console.WriteLine("Ошибка ввода");
                    }

                    using (ApplicationContext db = new ApplicationContext())
                    {
                        foreach (var u in db.Users.ToList())
                        {
                            if (u.Id == id)
                            {
                                u.Balance = balance;
                                db.Users.Update(u);
                                db.SaveChanges();
                            }
                        }
                    }
                    Console.WriteLine("Данные изменены");
                    break;


                default:
                    break;
            }

            break;


        default:
            break;
    }

}

static bool IdExist(int id)
{
    bool isExist = false;

    using (BankClientsContext db = new BankClientsContext())
    {
        foreach (User u in db.Users.ToList())
            if (u.Id == id)
                isExist = true;
    }

    return isExist;
}

static bool AddToTableDB(List<User> users)
{
    if (users.Count == 0)
        return false;

    try
    {
        using (BankClientsContext db = new BankClientsContext())
        {
            foreach (var item in users)
            {
                db.Users.Add(item);
            }

            db.SaveChanges();
        }        
    }
    catch (Exception)
    {
        return false;
    }

    return true;
}

static bool RemoveUserFromDBTable(int Id)
{
    bool isUserFind = false;

    using (ApplicationContext db = new ApplicationContext())
    {        
        User? user = db.Users.FirstOrDefault(p => p.Id == Id);
        if (user != null)
        {
            db.Users.Remove(user);
            db.SaveChanges();
            isUserFind = true;
        }
    }

    return isUserFind;
}

static long GetNextId()
{
    long id = 0;

    using (BankClientsContext db = new BankClientsContext())
    {
        foreach (User u in db.Users.ToList())
            if (u.Id > id)
                id = u.Id;
    }

    return ++id;
}

enum MainMenu { Exit, ExportUsers, AddUser, RemoveUser, UpdateUser }

enum UpdateMenu { Exit, FirstName, LastName, Patronomic, Age, Role, Account, Balance }



//добавление

//using ConsoleApp29;

//using (ApplicationContext db = new ApplicationContext())
//{
//    User tom = new User { Name = "Tom", Age = 54 };
//    User alice = new User { Name = "Alice", Age =34 };


//    db.Users.Add(tom);
//    db.Users.Add(alice);
//    db.SaveChanges();
//}







//Редактирование

//using (ApplicationContext db = new ApplicationContext())
//{

//    User? user = db.Users.FirstOrDefault();
//    if(user != null)
//    {
//        user.Name = "Vasiya";
//        user.Age = 2;
//        db.Users.Update(user);
//        db.SaveChanges();
//    }

//}



//Удаление
//using ConsoleApp10;

//using (ApplicationContext db = new ApplicationContext())
//{

//    User? user = db.Users.FirstOrDefault();
//    if (user != null)
//    {
//        db.Users.Remove(user);
//        db.SaveChanges();
//    }
//}



//using (ApplicationContext db = new ApplicationContext())
//{
//    var users = db.Users.ToList();
//    Console.WriteLine(users.Count);

//    foreach (User u in users)
//    {
//        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
//    }
//}

