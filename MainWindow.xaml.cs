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

namespace Warshamov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            bool IsSymbol = true;
            string Error = "";
            try
            {
                Convert.ToInt32(tbDmin.Text);
                foreach (char item in tbInput.Text)
                {
                    if ((item != 48) && (item != 49))
                    {
                        Error += "Incorrect the input Data. ";
                        IsSymbol = false;
                        break;
                    }
                }
                if (Convert.ToInt32(tbDmin.Text) < 2)
                {
                    Error += "D min is too small. ";
                    IsSymbol = false;
                }
                if ((Convert.ToInt32(tbDmin.Text) - 1) / 2 > tbInput.Text.Length)
                {
                    Error += "D min is too big for this Data";
                    IsSymbol = false;
                }
            }
            catch
            {
                IsSymbol = false;
                Error += "The input is incorrect. ";
            }
            lstMattrix.Items.Clear();
            if (IsSymbol)
            {
                WarshamovEncode warsh = new WarshamovEncode();
                int[,] Mattrix = warsh.BuildGmatrix(warsh.Cmatrix(tbInput.Text.Length, Convert.ToInt32(tbDmin.Text)));
                lstMattrix.Items.Clear();
                for (int i = 0; i < tbInput.Text.Length; i++)
                {
                    string s = "";
                    for (int j = 0; j < (Mattrix.Length / tbInput.Text.Length); j++)
                    {
                        s += Mattrix[i, j].ToString() + " ";
                    }
                    lstMattrix.Items.Add(s);
                }
                int[] result = warsh.GetEncode(Mattrix, tbInput.Text);
                tbRes.Text = "";
                foreach (int item in result)
                {
                    tbRes.Text += item.ToString();
                }
            }
            else
            {
                tbRes.Text = Error;
            }
        }
    }
}
