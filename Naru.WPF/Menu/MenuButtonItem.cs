﻿using Naru.RX;
using Naru.WPF.Command;
using Naru.WPF.Scheduler;
using Naru.WPF.ViewModel;

namespace Naru.WPF.Menu
{
    public class MenuButtonItem : ViewModel.ViewModel, IMenuItem
    {
        public string DisplayName { get; set; }

        #region IsVisible

        private readonly ObservableProperty<bool> _isVisible = new ObservableProperty<bool>();

        public bool IsVisible
        {
            get { return _isVisible.Value; }
            set { this.RaiseAndSetIfChanged(_isVisible, value); }
        }

        #endregion

        public DelegateCommand Command { get; set; }

        public string ImageName { get; set; }

        public MenuButtonItem(ISchedulerProvider scheduler)
        {
            _isVisible.ConnectINPCProperty(this, () => IsVisible, scheduler).AddDisposable(Disposables);

            IsVisible = true;
        }
    }
}