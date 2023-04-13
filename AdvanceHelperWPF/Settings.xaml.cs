﻿using System.IO;
using System.Windows;
using System.Windows.Forms;
using Menu = AdvanceHelperWEB.Menu;

namespace AdvanceHelperWPF
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        string DirPathStr;
        string FilesFromat;
        string dbusername;
        string dbname;
        string password;
        string userLogin;
        FileHandler fileHandler = new FileHandler();
        public Settings(string UserLogin)
        {
            InitializeComponent();
            SettingsTextBlock.Text = $"Версия программы: 1.1.0\nПользователь: {UserLogin}";
            userLogin = UserLogin;
            if (File.Exists("config.txt"))
            {
                DirPathStr = fileHandler.GetPath("config.txt", "Путь к рабочей директории = ");
                DirTextBlock.Text = $"Текущая рабочая директория: {DirPathStr}";

                FilesFromat = fileHandler.GetPath("config.txt", "Доступные форматы файлов (через запятую): ");
                TBfileFormat.Text = FilesFromat;

                dbusername = fileHandler.GetPath("config.txt", "Имя пользователя базы данных: ");
                TBdbusername.Text = dbusername;

                dbname = fileHandler.GetPath("config.txt", "Название базы данных: ");
                TBdbname.Text = dbname;

                password = fileHandler.GetPath("config.txt", "Пароль базы данных: ");
                TBpassword.Text = password;
            }
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(userLogin);
            menu.Show();
            this.Close();
        }

        private void PathViewBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirTextBlock.Text = $"Текущая рабочая директория: {folderBrowser.SelectedPath}";;
                DirPathStr = folderBrowser.SelectedPath;
            }
        }

        private void SaveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            fileHandler.FileSave("config.txt", DirPathStr, "Путь к рабочей директории = ");
            fileHandler.FileSave("config.txt", TBfileFormat.Text, "Доступные форматы файлов (через запятую): ");
            fileHandler.FileSave("config.txt", TBdbusername.Text, "Имя пользователя базы данных: ");
            fileHandler.FileSave("config.txt", TBdbname.Text, "Название базы данных: ");
            fileHandler.FileSave("config.txt", TBpassword.Text, "Пароль базы данных: ");
            System.Windows.MessageBox.Show("Настройки были успешно сохранены");
        }
    }
}