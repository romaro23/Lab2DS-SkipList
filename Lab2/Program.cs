using Lab2;
SkipList<int> list = null;
SkipList<int> copy = null;
Console.WriteLine("Choose type of probability generation: 1 - random, 2 - 2^i");
while (list == null && copy == null)
{
    switch (int.Parse(Console.ReadLine()))
    {
        case 1:
            list = new SkipList<int>(1);
            copy = new SkipList<int>(1);
            break;
        case 2:
            list = new SkipList<int>(2);
            copy = new SkipList<int>(2);
            break;
        default:
            Console.WriteLine("Wrong number. Try again");
            break;
    }
}
Console.WriteLine("Choose type of MaxLevel: 1 - const, 2 - func. Default is inf");
switch (int.Parse(Console.ReadLine()))
{
    case 1:
        Console.WriteLine("Write MaxLevel");
        int level = int.Parse(Console.ReadLine());
        list.SetMaxLevel("const", level);
        copy.SetMaxLevel("const", level);
        break;
    case 2:
        list.SetMaxLevel("func");
        copy.SetMaxLevel("func");
        break;
}
var active = list;
Console.WriteLine("You are working with the main list");
while (true)
{
    Console.WriteLine("1 - Add, 2 - Search, 3 - Remove, 4 - Create copy, 5 - Clear, 6 - Print, 7 - Switch to copy, 8 - Switch to main list");
    int key;
    int value;
    switch (int.Parse(Console.ReadLine()))
    {            
        case 1:
            Console.WriteLine("Write key: ");
            key = int.Parse(Console.ReadLine());
            Console.WriteLine("Write value: ");
            value = int.Parse(Console.ReadLine());
            active.Add(key, value);
            break;
        case 2:
            Console.WriteLine("Write key: ");
            key = int.Parse(Console.ReadLine());
            try
            {
                value = active.Search(key);
                Console.WriteLine("Value: " + value);
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;
        case 3:
            Console.WriteLine("Write key: ");
            key = int.Parse(Console.ReadLine());
            active.Remove(key);
            break;
        case 4:
            copy = list.Clone();
            break;
        case 5:
            active.Clear();
            break;
        case 6:
            active.Print();
            break;
        case 7:
            Console.WriteLine("You are working with the copy");
            active = copy;
            break;
        case 8:
            Console.WriteLine("You are working with the main list");
            active = list;
            break;
    }
}
