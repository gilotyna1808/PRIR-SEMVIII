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

namespace Aplikacja_Kliencka
{
    /// <summary>
    /// Logika interakcji dla klasy ConfigQueueBoard.xaml
    /// </summary>
    public partial class ConfigQueueBoard : UserControl
    {
        public int QueueCount { get; set; }
        public List<string> QueueNames { get; set; }
        public bool Changes { get; set; }

        private List<TextBox> textBoxes;
        public ConfigQueueBoard(ConfigClient config)
        {
            InitializeComponent();
            Changes = false;
            QueueCount = config.RabitMQ_QueueCount;
            QueueNames = config.RabitMQ_QueueRecive;
            LoadTxtBox();
            LoadQueueNamesTextBox();
        }

        private void LoadTxtBox()
        {
            txt_queueCount.Text = QueueCount.ToString();
        }

        private void LoadQueueNamesTextBox()
        {
            grid_queueNamesBoard.Children.Clear();
            textBoxes = new List<TextBox>();
            for(int i = 0; i < QueueCount; i++)
            {
                string tempQueueName = "";
                if (QueueNames.Count > i)
                {
                    tempQueueName = QueueNames[i];
                }
                var textBox = CreateQueueTxtBox(tempQueueName);
                grid_queueNamesBoard.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(textBox, i);
                textBoxes.Add(textBox);
                grid_queueNamesBoard.Children.Add(textBoxes[i]);
            }
        }

        private TextBox CreateQueueTxtBox(string txt)
        {
            double width = 800;
            if (this.ActualWidth != 0) width = this.ActualWidth;
            TextBox textBox = new TextBox();
            textBox.Margin = new Thickness(5, 5, 5, 0);
            textBox.Height = 60;
            textBox.Width = (width)-5;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.Text = txt;
            textBox.TextChanged += TextChanged;
            return textBox;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Changes = true;
        }

        private void txt_queueCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            uint queueCount = 0;
            if (uint.TryParse(txt_queueCount.Text,out queueCount))
            {
                this.TextChanged(sender, e);
                QueueCount = (int)queueCount;
                LoadQueueNamesTextBox();
            }
        }

        public void SaveChanges(ref ConfigClient config)
        {
            if (Changes)
            {
                List<string> tempQueueNames = new List<string>();
                try
                {
                    QueueCount = int.Parse(txt_queueCount.Text);
                    config.RabitMQ_QueueCount = QueueCount;
                    for(int i=0; i < QueueCount; i++)
                    {
                        string queueName = "";
                        if (textBoxes.Count>i) queueName = textBoxes[i].Text;
                        tempQueueNames.Add(queueName);
                    }
                    config.RabitMQ_QueueRecive = tempQueueNames;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
