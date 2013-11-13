﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Naru.TPL;
using Naru.WPF.MVVM;
using Naru.WPF.Scheduler;

namespace Naru.WPF.Dialog
{
    public class DialogBuilder<T> : IDialogBuilder<T>
    {
        private readonly ISchedulerProvider _scheduler;
        private readonly IViewService _viewService;
        private readonly DialogViewModel<T> _dialogViewModel;
        private readonly List<T> _answers = new List<T>();

        private DialogType _dialogType;
        private string _title;
        private string _message;

        public DialogBuilder(ISchedulerProvider scheduler, IViewService viewService, DialogViewModel<T> dialogViewModel)
        {
            _scheduler = scheduler;
            _viewService = viewService;
            _dialogViewModel = dialogViewModel;
        }

        public IDialogBuilder<T> WithDialogType(DialogType dialogType)
        {
            _dialogType = dialogType;

            return this;
        }

        public IDialogBuilder<T> WithAnswers(params T[] answers)
        {
            foreach (var answer in answers)
            {
                _answers.Add(answer);
            }

            return this;
        }

        public IDialogBuilder<T> WithTitle(string title)
        {
            _title = title;

            return this;
        }

        public IDialogBuilder<T> WithMessage(string message)
        {
            _message = message;

            return this;
        }

        public DialogViewModel<T> Build()
        {
            var viewModel = _dialogViewModel;
            viewModel.Initialise(_dialogType, _answers, _title, _message);
            return viewModel;
        }

        public T Show()
        {
            var viewModel = Build();
            _viewService.ShowModal(viewModel);

            return viewModel.SelectedAnswer;
        }

        public Task<T> ShowAsync()
        {
            var viewModel = Build();
            return _viewService.ShowModalAsync(viewModel).Select(() => viewModel.SelectedAnswer, _scheduler.Dispatcher.TPL);
        }
    }
}