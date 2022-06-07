﻿using Caliburn.Micro;
using Kebler.Domain.Models;

namespace Kebler.ViewModels.DialogWindows
{
    public class BoxViewModel : Screen
    {
        private string _message;
        private double _minWidth;

        private bool _yesVisible,
            _noVisible,
            _cancelVisible,
            _okVisible,
            _okDefault,
            _yesDefault,
            _logoVisibility;

        public bool Result;


        public double MinWidth
        {
            get => _minWidth;
            set => Set(ref _minWidth, value);
        }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public bool LogoVisibility
        {
            get => _logoVisibility;
            set => Set(ref _logoVisibility, value);
        }

        public bool YesDefault
        {
            get => _yesDefault;
            set => Set(ref _yesDefault, value);
        }

        public bool OkDefault
        {
            get => _okDefault;
            set => Set(ref _okDefault, value);
        }

        public bool OkVisible
        {
            get => _okVisible;
            set => Set(ref _okVisible, value);
        }

        public bool YesVisible
        {
            get => _yesVisible;
            set => Set(ref _yesVisible, value);
        }

        public bool NoVisible
        {
            get => _noVisible;
            set => Set(ref _noVisible, value);
        }

        public bool CancelVisible
        {
            get => _cancelVisible;
            set => Set(ref _cancelVisible, value);
        }


        protected void ShowButtons(MessageBoxDilogButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxDilogButtons.Ok:
                {
                    OkVisible = true;
                    YesDefault = true;
                    break;
                }
                case MessageBoxDilogButtons.OkCancel:
                {
                    OkVisible = CancelVisible = true;
                    OkDefault = true;
                    break;
                }
                case MessageBoxDilogButtons.YesNo:
                {
                    YesVisible = NoVisible = true;
                    YesDefault = true;
                    break;
                }
            }
        }


        public virtual void OkYes()
        {
            Result = true;
            base.TryCloseAsync(true);
        }

        public virtual void NoCancel()
        {
            Result = false;
            base.TryCloseAsync(false);
        }
    }
}