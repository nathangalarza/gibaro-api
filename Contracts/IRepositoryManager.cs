
namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }

        IUserDeviceRepository UserDevice { get; }

        //{{Interface}}//

        Task SaveAsync();
    }
}










