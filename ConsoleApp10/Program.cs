

using ConsoleApp10;

//using (BankClientsContext db = new BankClientsContext())
//{
//    foreach (User u in db.Users.ToList())
//        Console.WriteLine($"Id: {u.Id}\nName: {u.LastName} {u.FirstName} {u.PatronomicName}\nAge: {u.Age}\nRole: {u.Role}\nAccount: {u.Account}\n\n");
//}
bool AppIsOn = true;
int input;

while (AppIsOn)
{
    while (true)
    {
        Console.WriteLine("\n\tСделайте выбор\n" +
        "1 - Вывести список пользователей из базы\n" +
        "2 - Добавить пользователя\n" +
        "3 - Удалить пользователя\n" +
        "4 - Изменить баланс пользователя\n" +
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
                string firstName = Console.ReadLine() ?? "no first_name";

                Console.WriteLine("Введите Фамилию: ");
                string lastName = Console.ReadLine() ?? "no last_name";

                Console.WriteLine("Введите Отчество: ");
                string patronomic = Console.ReadLine() ?? "no patronomic";

                int age;
                while (true)
                {
                    Console.WriteLine("\nВведите Возраст: ");
                    if (int.TryParse(Console.ReadLine(), out age))
                        break;
                    else
                        Console.WriteLine("Ошибка ввода");
                }

                Console.WriteLine("Введите Роль пользователя (Клиент, сотрудник и т.д.): ");
                string role = Console.ReadLine() ?? "no role";

                Console.WriteLine("Введите IBAN: ");
                string account = Console.ReadLine() ?? "no patronomic";

                decimal balance;
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
            

            break;


        default:
            break;
    }

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

