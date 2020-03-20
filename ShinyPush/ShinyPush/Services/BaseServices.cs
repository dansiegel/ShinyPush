using System;
using System.Collections.Generic;
using System.Text;
using Prism.Logging;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;

namespace ShinyPush.Services
{
    public sealed class BaseServices
    {
        public INavigationService NavigationService { get; }

        public IDialogService DialogService { get; }

        public IPageDialogService PageDialogService { get; }

        public IEventAggregator EventAggregator { get; }

        public ILogger Logger { get; }

        public BaseServices(INavigationService navigationService, IDialogService dialogService, IPageDialogService pageDialogService, IEventAggregator eventAggregator, ILogger logger)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            PageDialogService = pageDialogService;
            EventAggregator = eventAggregator;
            Logger = logger;
        }
    }
}
