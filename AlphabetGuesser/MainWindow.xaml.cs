using Neurolib.SimpleBackProp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlphabetGuesser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackPropNeuralNetwork network = null;
        private CheckBox[] checkBoxes = new CheckBox[15];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (network == null)
                return;

            var input = checkBoxes.Select(c => c.IsChecked.Value ? 1.0 : 0.0).ToArray();

            var result = network.Run(input);

            double error;
            if (double.TryParse(txtError.Text, out error) == false)
            {
                error = 0.0005;
                txtError.Text = error.ToString();
            }

            char letter = GetMappedCharacter(result, error);
            lblLetter.Text = letter.ToString();
            lblRawValue.Text = result[0].ToString();
            Sample smpl = AlphabetData.Samples.FirstOrDefault(s=>s.Letter == letter);
            lblDiff.Text = (smpl != null ? 2*smpl.Output - result[0] : double.NaN).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".nnetwork";
                dlg.Filter = "Neural Network Configuration (*.nnetwork)|*.nnetwork";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == true)
                {
                    string fileName = dlg.FileName;
                    using (Stream stream = File.OpenRead(fileName))
                    {
                        BinaryFormatter formetter = new BinaryFormatter();
                        network = (BackPropNeuralNetwork)formetter.Deserialize(stream);
                    }
                }
            }
            finally
            {
                btn.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtError.Text = (0.005).ToString();

            int index = 0;
            for (int ri = 0; ri < 5; ri++)
            {
                for (int ci = 0; ci < 3; ci++)
                {
                    var chk = new CheckBox()
                    {
                        IsChecked = false,
                        Margin = new Thickness(3, 3, 3, 3)
                    };
                    Grid.SetColumn(chk, ci);
                    Grid.SetRow(chk, ri);
                    chk.Checked += CheckBox_CheckChanged;
                    chk.Unchecked += CheckBox_CheckChanged;
                    gridPattern.Children.Add(chk);

                    checkBoxes[index] = chk;
                    index++;
                }
            }
        }

        private static char GetMappedCharacter(double[] raw, double error)
        {
            double v = raw[0];

            var c = (from e in AlphabetData.Samples
                     let min = 2*e.Output - error
                     let max = 2*e.Output + error
                     where v >= min && v <= max
                     select e).FirstOrDefault();

            if (c == null)
                return '?';

            return c.Letter;
        }
    }
}
