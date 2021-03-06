using System.Threading.Tasks;

namespace EwalletService.Commands
{
    public interface ICommandHandler<T>  where T : ICommand
    {
         Task HandleAsync(T command);
    }
}