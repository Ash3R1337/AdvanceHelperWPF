﻿using System;
using System.Windows;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Media;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Forms;
using AHlibrary;

namespace AdvanceHelperWEB
{
    /// <summary>
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start(string UserLogin)
        {
            InitializeComponent();
            if (File.Exists("save.txt"))
            {
                DirPath.Text = File.ReadAllText("save.txt");
                DirPathStr = DirPath.Text;
                PathIsCorrect = true;
                EnableDisableButtons();
                FilesAddtoListBox();
                DirectoriesAddtoListBox();
            }
            labelLogin.Content = UserLogin;
            userLogin = UserLogin;
        }

        string userLogin;

        string DirPathStr; //Переменная, хранящая путь к текущей директории
        bool PathIsCorrect = false; //Определяет, установлена ли директория

        private void ChBtn_Click(object sender, RoutedEventArgs e) //Выбор рабочей директории
        {
            DirPathStr = DirPath.Text;
            FilesAddtoListBox();
            DirectoriesAddtoListBox();
            EnableDisableButtons();
        }

        public void EnableDisableButtons() //Отключение / Включение кнопок
        {
            if (PathIsCorrect == true)
            {
                SortBtn.IsEnabled = true;
                CheckBtn.IsEnabled = true;
                RenameBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
                MakeAcheckBtn.IsEnabled = true;
                PrintBtn.IsEnabled = true;
                CreateBtn.IsEnabled = true;
                OpenDirBtn.IsEnabled = true;
            }
            else
            {
                SortBtn.IsEnabled = false;
                CheckBtn.IsEnabled = false;
                RenameBtn.IsEnabled = false;
                DeleteBtn.IsEnabled = false;
                MakeAcheckBtn.IsEnabled = false;
                PrintBtn.IsEnabled = false;
                CreateBtn.IsEnabled = false;
                OpenDirBtn.IsEnabled = false;
            }
        }

        private void SurBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirPath.Text = folderBrowser.SelectedPath;
                DirPathStr = DirPath.Text;
            }
        }

        [Obsolete("Необходимо добавить возможность переключения тем", false)]
        private void ThemeChangeToBlue()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            var colorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF56E5F3"));
            var BlueStyle = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0463A4"));
            var TopPanel = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF02F4FF"));
            gradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF1B77D3"), 0));
            gradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF2291BB"), 1));
            this.Resources.Add("ScreenGradientBrush", gradientBrush);
            this.Resources.Add("ButtonsBrush", colorBrush);
            this.Resources.Add("PanelBrush", BlueStyle);
            this.Resources.Add("TopPanelBrush", TopPanel);
            SortBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            CheckBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            RenameBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            CreateBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            DeleteBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            MakeAcheckBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            OpenDirBtn.Background = (Brush)this.TryFindResource("ButtonsBrush");
            grid.Background = (Brush)this.TryFindResource("ScreenGradientBrush");
            //panel.Fill = (Brush)this.TryFindResource("PanelBrush");
            topPanel.Fill = (Brush)this.TryFindResource("TopPanelBrush");
        }

        private void EnterBtn_Copy2_Click(object sender, RoutedEventArgs e)
        {
            ThemeChangeToBlue();
        }

        private void EnterBtn_Copy7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SortBtn_Click(object sender, RoutedEventArgs e) //Распределение файлов по директориям
        {
            SortFiles(DirPathStr);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e) //Удаление выбранного файла
        {
            FileDelete();
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e) //Просмотр файла
        {
            OpenFile();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e) //Сохранение выбранной директории в файл
        {
            FileSave("save.txt", DirPath.Text);
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(userLogin);
            menu.Show();
            this.Close();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e) //Создание нового каталога
        {
            CreateCatalog(DirPathStr);
        }

        private void RenameBtn_Click(object sender, RoutedEventArgs e)
        {
            FileRename(DirPathStr);
        }

        private void OpenDirBtn_Click(object sender, RoutedEventArgs e)
        {
            DirOpen();
        }

        private void SheetGenerate_Click(object sender, RoutedEventArgs e)
        {
            Excel excel = new Excel();
            excel.ExcelCreateDocument("Ведомость");
        }

        public void DirOpen()
        {
            try
            {
                if (DirPath.Text != "")
                {
                    int index = CatalogsList.SelectedIndex;
                    Process.Start(ListBoxFolders[index]);
                }
                else MessageBox.Show("Введите путь к директории.");
            }
            catch (Win32Exception) { MessageBox.Show("Выбранная папка не найдена."); DirectoriesAddtoListBox(); }
            catch (IndexOutOfRangeException) { MessageBox.Show("Выберите папку, которую нужно просмотреть."); }
        }

        public void OpenFile() //Просмотр файла
        {
            try
            {
                if (DirPath.Text != "")
                {
                    int index = FilesList.SelectedIndex;
                    Process.Start(ListBoxFiles[index]);
                }
                else MessageBox.Show("Введите путь к директории.");
            }
            catch (Win32Exception) { MessageBox.Show("Выбранный файл не найден."); FilesAddtoListBox(); }
            catch (IndexOutOfRangeException) { MessageBox.Show("Выберите файл, который нужно просмотреть."); }
        }

        public void FileRename(string DirPath) //Переименование файла
        {
            try
            {
                FileRenameWindow fileRenameWindow = new FileRenameWindow(FilesList.SelectedItem.ToString());
                if (fileRenameWindow.ShowDialog() == true)
                {
                    try
                    {
                        int index = FilesList.SelectedIndex;
                        string sourceFileName = ListBoxFiles[index];
                        string destFileName = DirPath + "\\" + fileRenameWindow.FileNameStr;
                        File.Move(sourceFileName, destFileName);
                        MessageBox.Show("Файл был успешно переименован");
                    }
                    catch (FileNotFoundException) { MessageBox.Show("Выбранного файла не существует"); }
                    catch (IndexOutOfRangeException) { MessageBox.Show("Выберите файл, который нужно переименовать"); }
                    FilesAddtoListBox();
                }
            }
            catch (NullReferenceException) { MessageBox.Show("Выберите файл, который нужно переименовать"); }
        }

        public void FileDelete() //Удаление файла
        {
            try
            {
                int index = FilesList.SelectedIndex;
                string path = ListBoxFiles[index];
                File.Delete(path);
                MessageBox.Show("Файл был успешно удален");
            }
            catch (FileNotFoundException) { MessageBox.Show("Выбранного файла не существует"); }
            catch (IndexOutOfRangeException) { MessageBox.Show("Выберите файл, который нужно удалить"); }
            FilesAddtoListBox();
        }

        public void FileSave(string file, string DirPath) //Сохранение файла
        {
            FileStream fileStream = null;
            if (!File.Exists(file))
                fileStream = File.Create(file);
            else
                fileStream = File.OpenWrite(file);

            StreamWriter output = new StreamWriter(fileStream);
            output.Close();
            File.WriteAllText(file, DirPath);
            MessageBox.Show("Путь был успешно сохранен.");
        }

        public bool CreateCatalog(string DirPath) //Создание папки
        {
            CatalogWindow catalogWindow = new CatalogWindow();
            if (catalogWindow.ShowDialog() == true)
            {
                if (Directory.Exists(DirPath + "\\" + catalogWindow.DirNameStr))
                {
                    MessageBox.Show($"Каталог {catalogWindow.DirNameStr} уже существует");
                    return false;
                }
                else
                {
                    Directory.CreateDirectory(DirPath + "\\" + catalogWindow.DirNameStr);
                    MessageBox.Show($"Каталог {catalogWindow.DirNameStr} успешно создан");
                    DirectoriesAddtoListBox();
                    return true;
                }
            }
            return false;
        }

        string[] ListBoxFiles; //Массив с расположением всех файлов
        string[] ListBoxFolders; //Массив с расположением всех папок

        public void FilesAddtoListBox() //Добавление файлов в FilesList
        {
            try
            {
                FilesList.Items.Clear();
                string[] AllFiles = Directory.GetFiles(DirPathStr, "*.docx", SearchOption.TopDirectoryOnly);
                Array.Copy(AllFiles, ListBoxFiles = new string[AllFiles.Length], AllFiles.Length);
                foreach (string filename in AllFiles)
                {
                    FilesList.Items.Add(System.IO.Path.GetFileName(filename));
                }
                PathIsCorrect = true;
            }
            catch (DirectoryNotFoundException) {/*MessageBox.Show("Выбранная директория не найдена.");*/}
            catch (ArgumentException)
            {
                /*MessageBox.Show("Путь не выбран.");
                FilesList.Items.Clear();*/
            }
        }
        public void DirectoriesAddtoListBox() //Добавление папок в CatalogsList
        {
            try
            {
                CatalogsList.Items.Clear();
                string[] AllFolders = Directory.GetDirectories(DirPathStr);
                Array.Copy(AllFolders, ListBoxFolders = new string[AllFolders.Length], AllFolders.Length);
                foreach (string foldername in AllFolders)
                {
                    string directory = new DirectoryInfo(foldername).Name;
                    CatalogsList.Items.Add(directory);
                }
                PathIsCorrect = true;
            }
            catch (DirectoryNotFoundException) { MessageBox.Show("Выбранная директория не найдена."); PathIsCorrect = false; }
            catch (ArgumentException)
            {
                MessageBox.Show("Путь не выбран.");
                FilesList.Items.Clear();
                PathIsCorrect = false;
            }
        }

        public void SortFiles(string DirPath) //Распределение файлов по папкам
        {
            try
            {
                string[] AllFolders = Directory.GetDirectories(DirPath);
                string[] AllFiles = Directory.GetFiles(DirPath, "*.docx", SearchOption.TopDirectoryOnly);
                int count = 0;
                foreach (string folder in AllFolders)
                    foreach (string filename in AllFiles)
                    {
                        try
                        {
                            string Name = new DirectoryInfo(folder).Name;
                            Regex FolderName = new Regex(Name);
                            MatchCollection match = FolderName.Matches(Path.GetFileName(filename));
                            if (match.Count > 0)
                            {
                                string file = Path.GetFileName(filename);
                                string NewPath = Path.Combine(folder, file);
                                File.Move(filename, NewPath);
                                count++;
                                FilesAddtoListBox();
                            }
                        }
                        catch (IOException) { MessageBox.Show($"Файл {Path.GetFileName(filename)} уже есть в одной из папок"); }
                    }
                if (count > 0) MessageBox.Show($"Было распределено {count} файлов.");
                else MessageBox.Show("Файлы для распределения не были найдены");
            }
            catch (DirectoryNotFoundException) { MessageBox.Show("Путь не выбран."); }
            catch (ArgumentException) { MessageBox.Show("Путь не выбран."); FilesList.Items.Clear(); }
        }
    }
}