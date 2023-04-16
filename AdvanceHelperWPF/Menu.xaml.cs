﻿using AdvanceHelperWPF;
using System.Windows;

namespace AdvanceHelperWEB
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Menu : Window
    {
        string userLogin;
        string userStatus;
        public Menu(string UserLogin, string UserStatus)
        {
            InitializeComponent();
            labelHeaderLogin.Content = $"Добро пожаловать {UserLogin}! С чего начнем?";
            userLogin = UserLogin;
            userStatus = UserStatus;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            Start start = new Start(userLogin, userStatus);
            start.Show();
            this.Close();
        }

        private void Tables_Click(object sender, RoutedEventArgs e)
        {
            Tables tables = new Tables(userLogin, userStatus);
            tables.Show();
            this.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TeachersPortfolioBtn_Click(object sender, RoutedEventArgs e)
        {
            TeachersPortfolio teachersPortfolio = new TeachersPortfolio(userLogin, userStatus);
            teachersPortfolio.Show();
            this.Close();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings(userLogin, userStatus);
            settings.Show();
            this.Close();
        }
    }
}
