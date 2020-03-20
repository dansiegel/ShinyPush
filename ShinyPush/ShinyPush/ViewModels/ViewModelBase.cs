using ReactiveUI;
using Prism.Navigation;
using System.Reactive.Disposables;
using ShinyPush.Services;
using Prism.Services.Dialogs;
using Prism.Services;
using Prism.Events;
using Prism.Logging;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace ShinyPush.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, INavigationAware, IDestructible
    {
        protected CompositeDisposable Disposables { get; private set; }

        private BaseServices BaseServices;

        private INavigationService NavigationService => BaseServices.NavigationService;

        protected IDialogService DialogService => BaseServices.DialogService;

        protected IPageDialogService PageDialogService => BaseServices.PageDialogService;

        protected IEventAggregator EventAggregator => BaseServices.EventAggregator;

        protected ILogger Logger => BaseServices.Logger;

        protected ViewModelBase(BaseServices baseServices)
        {
            Disposables = new CompositeDisposable();
            BaseServices = baseServices;
        }

        protected Task GoBack(INavigationParameters parameters) => NavigationService.GoBackAsync(parameters);

        protected Task HandleNavigation(string uri, params(string Key, object Value)[] parameters)
        {
            var p = new NavigationParameters();
            parameters.ForEach(x => p.Add(x.Key, x.Value));
            return HandleNavigation(uri, p);
        }

        protected async Task HandleNavigation(string uri, INavigationParameters parameters)
        {
            var result = await NavigationService.NavigateAsync(uri, parameters);

            if(!result.Success)
            {
                Logger.Report(result.Exception);
                System.Diagnostics.Debugger.Break();
            }
        }

        protected virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        protected virtual void OnNavigatedTo(INavigationParameters parameters) { }

        protected virtual void Destroy() { }

        void IDestructible.Destroy()
        {
            if(Disposables != null && !Disposables.IsDisposed)
            {
                Disposables.Dispose();
                Disposables = null;
            }

            Destroy();

            BaseServices = null;
        }

        void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters)
        {
            OnNavigatedFrom(parameters);
        }

        void INavigatedAware.OnNavigatedTo(INavigationParameters parameters)
        {
            OnNavigatedTo(parameters);
        }
    }
}
