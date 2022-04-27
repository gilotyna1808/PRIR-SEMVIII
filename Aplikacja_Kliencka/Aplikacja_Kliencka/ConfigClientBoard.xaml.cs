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
    /// Logika interakcji dla klasy ConfigClientBoard.xaml
    /// </summary>
    public partial class ConfigClientBoard : UserControl
    {
        public int ClientCount { get; set; }
        public List<string> ClientNames { get; set; }
        public bool Changes { get; set; }

        private List<TextBox> textBoxes;
        public ConfigClientBoard(ConfigClient config)
        {
            InitializeComponent();
            Changes = false;
            ClientCount = config.ClientCount;
            ClientNames = config.ClientsNames;
            LoadTxtBox();
            LoadQueueNamesTextBox();
        }

        private void LoadTxtBox()
        {
            txt_clientCount.Text = ClientCount.ToString();
        }

        private void LoadQueueNamesTextBox()
        {
            grid_clientNamesBoard.Children.Clear();
            textBoxes = new List<TextBox>();
            for (int i = 0; i < ClientCount; i++)
            {
                string tempClientName = "";
                if (ClientNames.Count > i)
                {
                    tempClientName = ClientNames[i];
                }
                var textBox = CreateQueueTxtBox(tempClientName);
                grid_clientNamesBoard.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(textBox, i);
                textBoxes.Add(textBox);
                grid_clientNamesBoard.Children.Add(textBoxes[i]);
            }
        }

        private TextBox CreateQueueTxtBox(string txt)
        {
            double width = 800;
            if (this.ActualWidth != 0) width = this.ActualWidth;
            TextBox textBox = new TextBox();
            textBox.Margin = new Thickness(5, 5, 5, 0);
            textBox.Height = 60;
            textBox.Width = (width) - 5;
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
            if (uint.TryParse(txt_clientCount.Text, out queueCount))
            {
                this.TextChanged(sender, e);
                ClientCount = (int)queueCount;
                LoadQueueNamesTextBox();
            }
        }

        public void SaveChanges(ref ConfigClient config)
        {
            if (Changes)
            {
                List<string> tempClientNames = new List<string>();
                try
                {
                    ClientCount = int.Parse(txt_clientCount.Text);
                    config.ClientCount = ClientCount;
                    for (int i = 0; i < ClientCount; i++)
                    {
                        string queueName = "";
                        if (textBoxes.Count > i) queueName = textBoxes[i].Text;
                        tempClientNames.Add(queueName);
                    }
                    config.ClientsNames = tempClientNames;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
