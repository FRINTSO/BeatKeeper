using BeatKeeper.ViewModels;

namespace BeatKeeper.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}
