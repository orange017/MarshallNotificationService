using MajorNotification.Core.Major;

namespace MajorNotification.Observer;

public interface IMajorObserver
{
    void Update(MajorCommand majorCommand);
}