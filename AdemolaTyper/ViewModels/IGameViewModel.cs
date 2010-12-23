using System.Windows.Input;

namespace AdemolaTyper.ViewModels
{
    public interface IGameViewModel
    {
        ICommand CurrentWordIsProcessed{ get; }
    }
}