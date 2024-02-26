using ConsoleApp41.Models;
using ConsoleApp41.Data;

EventDao eventDao = new();
SpeakerDao speakerDao = new();
string? option;
do {
    ShowMenu();
    Console.Write("Please choose an option: ");
    option = Console.ReadLine();
    switch (option) {
        case "1":
            Console.WriteLine("Insert Event");
            Event newEvent = CreateEvent();
            Console.Write("Enter speaker count: ");
            int speakerCount = int.Parse(Console.ReadLine());
            int[] speakers = new int[speakerCount];
            for (int i = 0; i < speakerCount; i++) {
                Console.Write("Enter Speaker Id: ");
                speakers[i] = int.Parse(Console.ReadLine());
            }
            eventDao.Insert(newEvent, speakers);
            break;
        case "2":
            Console.WriteLine("Insert Speaker");
            Speaker newSpeaker = CreateSpeaker();
            speakerDao.Insert(newSpeaker);
            break;
        case "3":
            Console.WriteLine("Get Event");
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());
            Event readEvent = eventDao.GetById(id);
            Console.WriteLine(readEvent);
            break;
        case "4":
            Console.WriteLine("Get Speaker");
            Console.Write("Id: ");
            id = int.Parse(Console.ReadLine());
            Speaker readSpeaker = speakerDao.GetById(id);
            Console.WriteLine(readSpeaker);
            break;
        case "5":
            Console.WriteLine("Get All Events");
            List<Event> events = eventDao.GetAll();
            foreach (Event e in events) {
                Console.WriteLine(e);
            }
            break;
        case "6":
            Console.WriteLine("Get All Speakers");
            List<Speaker> speakersList = speakerDao.GetAll();
            foreach (Speaker s in speakersList) {
                Console.WriteLine(s);
            }
            break;
        case "7":
            Console.WriteLine("Update Speaker");
            Console.WriteLine("Rows affected: " + speakerDao.Update(CreateSpeaker()));
            break;
        case "8":
            Console.WriteLine("Delete Event");
            Console.WriteLine("Rows affected: " + eventDao.Delete(int.Parse(Console.ReadLine())));
            break;
        case "9":
            Console.WriteLine("Delete Speaker");
            Console.WriteLine("Rows affected: " + speakerDao.Delete(int.Parse(Console.ReadLine())));
            break;
        case "10":
            Console.WriteLine("Add Speaker to Event");
            Console.WriteLine("Rows affected: " + eventDao.AddSpeaker(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            break;
        case "11":
            Console.WriteLine("Remove Speaker from Event");
            Console.WriteLine("Rows affected: " + eventDao.RemoveSpeaker(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            break;
        case "0":
            Console.WriteLine("Exit");
            break;
        default:
            Console.WriteLine("Invalid Option");
            break;
    }
} while (option != "0");

static void ShowMenu() {
    Console.WriteLine("1. Insert Event");
    Console.WriteLine("2. Insert Speaker");
    Console.WriteLine("3. Get Evemt");
    Console.WriteLine("4. Get Speaker");
    Console.WriteLine("5. Get All Events");
    Console.WriteLine("6. Get All Speakers");
    Console.WriteLine("7. Update Speaker");
    Console.WriteLine("8. Delete Event");
    Console.WriteLine("9. Delete Speaker");
    Console.WriteLine("10. Add Speaker to Event");
    Console.WriteLine("11. Remove Speaker from Event");
    Console.WriteLine("0. Exit");
}

static Event CreateEvent() {
    Event newEvent = new();
    Console.Write("Name: ");
    newEvent.Name = Console.ReadLine();
    Console.Write("Description: ");
    newEvent.Description = Console.ReadLine();
    Console.Write("Address: ");
    newEvent.Address = Console.ReadLine();
    Console.Write("Start Date: ");
    newEvent.StartDate = DateTime.Parse(Console.ReadLine());
    Console.Write("Start Time: ");
    newEvent.StartTime = DateTime.Parse(Console.ReadLine());
    Console.Write("End Time: ");
    newEvent.EndTime = DateTime.Parse(Console.ReadLine());
    return newEvent;
}

static Speaker CreateSpeaker() {
    Speaker newSpeaker = new();
    Console.Write("Full Name: ");
    newSpeaker.FullName = Console.ReadLine();
    Console.Write("Position: ");
    newSpeaker.Position = Console.ReadLine();
    Console.Write("Company: ");
    newSpeaker.Company = Console.ReadLine();
    Console.Write("Image URL: ");
    newSpeaker.ImageUrl = Console.ReadLine();
    return newSpeaker;
}
