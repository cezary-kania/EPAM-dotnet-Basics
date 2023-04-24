using Task2_GreetingLibrary;

IGreetingService service = new GreetingService();

var username = args.Length > 0 ? args[0] : string.Empty;
Console.WriteLine(service.GetMessage(username));
