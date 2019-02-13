﻿using System;
using System.Windows.Input;

namespace Pomo_Shiny
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IApplicationAccessor _applicationAccessor;
        private readonly ICountdownTimer _countdownTimer;
        private TimeSpan _remainingTime;

        public MainWindowViewModel(IApplicationAccessor applicationAccessor, ICountdownTimer countdownTimer)
        {
            _applicationAccessor = applicationAccessor;
            _countdownTimer = countdownTimer;
            _countdownTimer.Callback = val => RemainingTime = val;

            ExitCommand = new DelegateCommand(Exit);
            StartTimerCommand = new DelegateCommand(StartTimer);
            StartBreakTimerCommand = new DelegateCommand(StartBreakTimer);
        }

        public ICommand ExitCommand { get; }
        public ICommand StartTimerCommand { get; }
        public ICommand StartBreakTimerCommand { get; }

        public TimeSpan RemainingTime
        {
            get => _remainingTime;
            private set
            {
                _remainingTime = value;
                RaisePropertyChangedEvent(nameof(RemainingTime));
            }
        }

        private void StartTimer()
        {
            _countdownTimer.StartCountdown(25);
        }

        private void StartBreakTimer()
        {
            _countdownTimer.StartCountdown(5);
        }

        private void Exit()
        {
            _applicationAccessor.Shutdown();
        }
    }
}