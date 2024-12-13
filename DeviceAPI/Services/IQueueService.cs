using DeviceAPI.Models;

namespace DeviceAPI.Services;

public interface IQueueService
{
    void SendToQueue(Device device, string action);
}
