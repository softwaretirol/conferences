using Interfaces;
using Prism.Events;
using Prism.Mvvm;

namespace PRISMDWX
{
    public class SplashScreenViewModel : BindableBase
    {
        private string currentProgress;

        public SplashScreenViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<PubSubEvent<ISplashScreenProgressMessage>>()
                .Subscribe(OnMessage);
        }

        public string CurrentProgress
        {
            get => currentProgress;
            set => SetProperty(ref currentProgress, value);
        }

        private void OnMessage(ISplashScreenProgressMessage obj)
        {
            CurrentProgress = obj.Message;
        }
    }
}