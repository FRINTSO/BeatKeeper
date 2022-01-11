namespace BeatKeeper.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {

        public LayoutViewModel(ViewModelBase contentViewModel)
        {
            ContentViewModel = contentViewModel;
        }

        public ViewModelBase ContentViewModel { get; }

        public override void Dispose()
        {
            ContentViewModel.Dispose();

            base.Dispose();
        }

    }
}
