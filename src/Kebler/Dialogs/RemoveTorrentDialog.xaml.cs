﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Kebler.Models;
using Kebler.TransmissionCore;

namespace Kebler.Dialogs
{
    /// <summary>
    ///     Interaction logic for RemoveTorrentDialog.xaml
    /// </summary>
    public partial class RemoveTorrentDialog
    {
        private static readonly Kebler.Services.Interfaces.ILog Log = Kebler.Services.Log.Instance;
        private readonly CancellationToken _cancellationToken;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly uint[] _toRemove;
        private readonly TransmissionClient _transmissionClient;
        public Enums.RemoveResult Result;


        public RemoveTorrentDialog(IEnumerable<string> names, uint[] toRm, ref TransmissionClient? transmissionClient,
            bool witData = false)
        {
            InitializeComponent();
            Container.ItemsSource = names;
            RemoveWithDataCheckBox.IsChecked = witData;

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            RemoveWithDataCheckBox.IsChecked = witData;
            _toRemove = toRm;
            _transmissionClient = transmissionClient;
            DataContext = this;
        }

        public bool WithData => RemoveWithDataCheckBox.IsChecked != null && (bool) RemoveWithDataCheckBox.IsChecked;
        public bool IsWorking { get; set; }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            IsWorking = false;
            DialogResult = false;
            _cancellationTokenSource.Cancel();
            Close();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            IsWorking = false;
        }

        public async void Add(object sender, RoutedEventArgs e)
        {
            IsWorking = true;
            var with = WithData;
            await Task.Factory.StartNew(async () =>
            {
                Log.Info("Start removing: " + string.Join(", ", _toRemove));
                while (true)
                {
                    if (Dispatcher.HasShutdownStarted)
                        return;
                    Result = await _transmissionClient.TorrentRemoveAsync(_toRemove, _cancellationToken, with);
                    Log.Info("RM response: " + Result);
                    if (Result == Enums.RemoveResult.Ok)
                    {
                        DialogResult = true;
                        break;
                    }

                    await Task.Delay(500, _cancellationToken);
                }
            }, _cancellationToken);
            Close();
        }

        public new void Show()
        {
            throw new Exception("Use ShowDialog instead of Show()");
        }
    }
}