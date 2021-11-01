﻿namespace Kebler
{
#if RELEASE
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using Caliburn.Micro;
    using Kebler.Const;
    using Kebler.Models;
    using Kebler.Resources;
    using Kebler.Services;
    using Kebler.Update.Core;
    using Kebler.ViewModels;
#endif

    internal static class Updater
    {
#if RELEASE

        private static readonly Kebler.Services.Interfaces.ILog Log = Kebler.Services.Log.Instance;

        public static async Task CheckUpdates()
        {

     Log.Info("Check updates");
            try
            {
                var current = Assembly.GetExecutingAssembly().GetName().Version;

                var result = await UpdaterApi.Check(ConstStrings.GITHUB_USER, nameof(Kebler), current, ConfigService.Instanse.AllowPreRelease);

                Log.Info($"Current {current} Serv {result.Item2.name}");

                App.Instance.IsUpdateReady = result.Item1;
                if (result.Item1)
                {
                    var mgr = new WindowManager();
                    var lt = LocalizationProvider.GetLocalizedValue(nameof(Strings.NewUpdate)); 
                    var dialogres = await mgr.ShowDialogAsync(new MessageBoxViewModel(lt.Replace("%d", result.Item2.tag_name), string.Empty,
                        Enums.MessageBoxDilogButtons.YesNo, true));
                    if (dialogres == true) 
                        InstallUpdates();
                }
            }
            catch (Exception ex)
            {
                App.Instance.IsUpdateReady = false;
                Log.Error(ex);
                Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);
            }

    }

    public static void InstallUpdates()
        {
            Directory.CreateDirectory(ConstStrings.TempInstallerFolder);
            File.Copy(ConstStrings.InstallerExePath, ConstStrings.TempInstallerExePath, true);

            using (Process process = new Process())
            {
                var info = new ProcessStartInfo
                {
                    FileName = ConstStrings.TempInstallerExePath,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false
                };

                if (ConfigService.Instanse.AllowPreRelease)
                    info.Arguments = $"{ConstStrings.Args.Beta}";

                process.StartInfo = info;
                process.EnableRaisingEvents = false;
                process.Start();
            }

            Application.Current.Shutdown();
        }
#endif
    }

}