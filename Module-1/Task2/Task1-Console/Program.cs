using Task2_GreetingLibrary;

IConcatenationService service = new ConcatenationService();

var username = args.Length > 0 ? args[0] : "";
Console.WriteLine(service.GetMessage(DateTime.Now, username));
