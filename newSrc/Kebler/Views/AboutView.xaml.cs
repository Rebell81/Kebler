﻿using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace Kebler.Views
{
    /// <summary>
    ///     Interaction logic for About.xaml
    /// </summary>
    public partial class AboutView
    {
        public AboutView() 
        {
            InitializeComponent();
            //DataContext = this;
            //Vers = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        //public string Vers { get; set; }


        //private void Open(object sender, RequestNavigateEventArgs e)
        //{
        //    Process.Start(new ProcessStartInfo("cmd", $"/c start {e.Uri.AbsoluteUri}") {CreateNoWindow = true});
        //    e.Handled = true;
        //}

        private void Open(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {e.Uri.AbsoluteUri}") { CreateNoWindow = true });
            e.Handled = true;
        }
    }
}