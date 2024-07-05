using MajorNotification.Core.Major;
using MajorNotification.Observer;

class ConsoleObserver : IMajorObserver
{
    public void Update(MajorCommand majorCommand)
    {
        if (majorCommand != MajorCommand.Unknown)
        {
            Console.WriteLine(majorCommand);
        }
    }
}