﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Caliburn.Micro;
using Kebler.Domain.Interfaces;
using Kebler.Domain.Models.Events;
using WPFLocalizeExtension.Engine;

namespace Kebler.Services
{
    public class LocalizationManager : ILocalizationManager
    {
        private List<CultureInfo> _cultureList;

        private readonly ILogger _logger = Logger.Instance;
        private readonly IConfigService _configService = ConfigService.Instance;


        private CultureInfo _currentCulture = new("en");
        private static ILocalizationManager _localizationManager;

        public static ILocalizationManager Instance => _localizationManager ??= new LocalizationManager();


        public List<CultureInfo> CultureList
        {
            get
            {
                return _cultureList ??= new List<CultureInfo>
                {
                    new("en"),
                    new("ru")
                };
            }
        }

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                try
                {
                    _currentCulture = value;
                    ConfigService.DefaultSettingsInstanse.Language = _currentCulture;
                    _configService.Save();

                    var eventAggraAggregator = IoC.Get<IEventAggregator>();

                    eventAggraAggregator.PublishOnUIThreadAsync(new LocalizationCultureChangesMessage
                    {
                        Culture = CurrentCulture
                    });

                    SetCurrentThreadCulture(CurrentCulture);
                    _logger.Info($"Lang changed {_currentCulture}");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        private void SetCurrentThreadCulture(in CultureInfo culture)
        {
            try
            {
                if (culture != null)
                {
                    Thread.CurrentThread.CurrentUICulture = culture;
                    Thread.CurrentThread.CurrentCulture =
                        CultureInfo.CreateSpecificCulture(culture.TwoLetterISOLanguageName);

                    CultureInfo.DefaultThreadCurrentCulture = culture;
                    CultureInfo.DefaultThreadCurrentUICulture = culture;

                    LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
                    LocalizeDictionary.Instance.Culture = culture;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}