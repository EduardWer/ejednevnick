using System.Globalization;

namespace PracticalWork4;


internal class Program
{
    private static readonly List<zametka> zametki = new();
    private static DateTime Data_Time = DateTime.Now;
    private static int cursorPosition = 1;//sds

    private static DateTime Izmena_Data(int change)
    {
        var newDate = Data_Time.AddDays(change);
        if (newDate.Date >= DateTime.Today) return newDate;
        return Data_Time;
    }

    private static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Текущая дата: {Data_Time.ToString("dd.MM.yyyy")}\t\tF1 для добавления заметки");

            var i = 1;
            foreach (var zametka in zametki)
                if (zametka.Date.Date == Data_Time.Date)
                {
                    Console.Write("  " + i + ". ");
                    Console.WriteLine(zametka.Name);
                    i++;
                }

            Console.SetCursorPosition(0, cursorPosition);
            Console.Write("->");

            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.F1:
                    Addzametki();
                    break;
                case ConsoleKey.Enter:
                    if (zametki.Count > 0)
                    {
                        var selectedNote = GetSelectedNote();
                        if (selectedNote != null)
                        {
                            DisplayNoteInfo(selectedNote);
                            Console.ReadKey();
                        }
                    }

                    break;
                case ConsoleKey.UpArrow:
                    if (cursorPosition > 1)
                        cursorPosition--;
                    break;
                case ConsoleKey.DownArrow:
                    if (cursorPosition < i - 1)
                        cursorPosition++;
                    break;
                case ConsoleKey.LeftArrow:
                    Data_Time = Izmena_Data(-1);
                    cursorPosition = 1;
                    break;
                case ConsoleKey.RightArrow:
                    Data_Time = Izmena_Data(1);
                    cursorPosition = 1;
                    break;
                default:
                    Console.WriteLine("Ошибка ввода");
                    break;
            }
        }
    }

    private static void Addzametki()
    {
        Console.Clear();
        var zametka_disc = new zametka();
        Console.WriteLine("Введите название");
        zametka_disc.Name = Console.ReadLine();
        Console.WriteLine("Введите описание");
        zametka_disc.Description = Console.ReadLine();
        Console.WriteLine("Введите дату (в формате день.месяц.год)");
        var dateString = Console.ReadLine();
        DateTime date;
        if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
        {
            zametka_disc.Date = date;
        }
        else
        {
            Console.WriteLine("Неверный формат даты");
            return;
        }

        zametki.Add(zametka_disc);
    }

    private static void DisplayNoteInfo(zametka Ime)
    {
        Console.Clear();
        Console.WriteLine("Название: " + Ime.Name);
        Console.WriteLine("Описание: " + Ime.Description);
        Console.WriteLine("Дата: " + Ime.Date.ToString("dd.MM.yyyy"));
    }

    private static zametka GetSelectedNote()
    {
        var i = 1;
        foreach (var note in zametki)
            if (note.Date.Date == Data_Time.Date)
            {
                if (i == cursorPosition) return note;
                i++;
            }

        return null;
    }

    public class zametka
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}