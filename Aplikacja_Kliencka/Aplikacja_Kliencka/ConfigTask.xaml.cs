using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Aplikacja_Kliencka
{
    /// <summary>
    /// Logika interakcji dla klasy ConfigTask.xaml
    /// </summary>
    public partial class ConfigTaskBoard : UserControl
    {
        public int TaskCount { get; set; }

        public List<string> TaskNames { get; set; }
        public List<string> TaskPath { get; set; }
        public bool Changes { get; set; }
        private List<TextBox> textBoxesNames;
        private List<TextBox> textBoxesPath;

        public ConfigTaskBoard(ConfigClient config)
        {
            InitializeComponent();
            Changes = false;
            TaskCount = config.TaskCount;
            TaskNames = new List<String>();
            TaskPath = new List<String>();
            foreach(KeyValuePair <string,string> entry in config.Tasks)
            {
                TaskNames.Add(entry.Key);
                TaskPath.Add(entry.Value);
            }
            LoadTxtBox();
            LoadQueueNamesTextBox();
        }
        private void LoadTxtBox()
        {
            txt_taskCount.Text = 2.ToString();
        }

        private TextBox CreateTaskTxtBoxLeft(string txt)
        {
            double width = 800;
            if (this.ActualWidth != 0) width = this.ActualWidth;
            TextBox textBox = new TextBox();
            textBox.Margin = new Thickness(5, 5, 5, 0);
            textBox.Height = 60;
            textBox.Width = (width/2) - 5;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.Text = txt;
            textBox.TextChanged += TextChanged;
            return textBox;
        }

        private TextBox CreateTaskTxtBoxRight(string txt)
        {
            double width = 800;
            if (this.ActualWidth != 0) width = this.ActualWidth;
            TextBox textBox = new TextBox();
            textBox.Margin = new Thickness((width / 2) + 5, 5, 5, 0);
            textBox.Height = 60;
            textBox.Width = (width / 2) - 5;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.Text = txt;
            textBox.TextChanged += TextChanged;
            textBox.GotFocus += PathTextBoxClicked;
            return textBox;
        }

        private void LoadQueueNamesTextBox()
        {
            grid_taskNamesBoard.Children.Clear();
            textBoxesNames = new List<TextBox>();
            textBoxesPath = new List<TextBox>();
            for (int i = 0; i < TaskCount; i++)
            {
                string tempTaskName = "";
                string tempTaskPath = "";
                if (TaskNames.Count > i)
                {
                    tempTaskName = TaskNames[i];
                }
                if (TaskPath.Count > i)
                {
                    tempTaskPath = TaskPath[i];
                }
                var textBoxName = CreateTaskTxtBoxLeft(tempTaskName);
                var textBoxPath = CreateTaskTxtBoxRight(tempTaskPath);
                grid_taskNamesBoard.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(textBoxName, i);
                Grid.SetColumn(textBoxName, 0);
                Grid.SetRow(textBoxPath, i);
                Grid.SetColumn(textBoxPath, 1);
                textBoxesNames.Add(textBoxName);
                textBoxesPath.Add(textBoxPath);
                grid_taskNamesBoard.Children.Add(textBoxesNames[i]);
                grid_taskNamesBoard.Children.Add(textBoxesPath[i]);
            }
        }

        private void txt_taskCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            uint queueCount = 0;
            if (uint.TryParse(txt_taskCount.Text, out queueCount))
            {
                this.TextChanged(sender, e);
                TaskCount = (int)queueCount;
                LoadQueueNamesTextBox();
            }
        }

        private void TextChanged(object sender=null, TextChangedEventArgs e=null)
        {
            Changes = true;
        }

        private void PathTextBoxClicked(object sender, RoutedEventArgs e)
        {
            var openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "All Files (*.*)|*.*";
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty && openFileDlg.FileName!="")
            {
                string res = openFileDlg.FileName;
                var txtbox = sender as TextBox;
                txtbox.Text = res;
            }
            TextChanged();
        }

        public void SaveChanges(ref ConfigClient config)
        {
            if (Changes)
            {
                Dictionary<string, string> tempTask = new Dictionary<string, string>();
                try
                {
                    TaskCount = int.Parse(txt_taskCount.Text);
                    config.TaskCount = TaskCount;
                    for (int i = 0; i < TaskCount; i++)
                    {
                        string k = "";
                        string v = "";
                        if (textBoxesNames.Count > i) k = textBoxesNames[i].Text;
                        if (textBoxesPath.Count > i) v = textBoxesPath[i].Text;
                        tempTask.Add(k, v);
                    }
                    config.Tasks = tempTask;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
