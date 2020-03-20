using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using ShinyPush.Services;

namespace ShinyPush.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(BaseServices baseServices) : base(baseServices)
        {
        }
    }
}
