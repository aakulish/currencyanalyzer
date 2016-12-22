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

namespace CurrencyAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CurrencyService service = new CurrencyService();
        const int RequiredSamples = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rates = await service.GetLastNRates(textBoxCurrency.Text, RequiredSamples);
                string trend;
                if (rates[0].Rate >= rates[1].Rate && rates[1].Rate >= rates[2].Rate)
                    trend = "Increasing";
                else
                    if (rates[0].Rate <= rates[1].Rate && rates[1].Rate <= rates[2].Rate)
                        trend = "Decreasing";
                    else
                        trend = "Unstable";
                textBlockTrend.Text = trend;
            }
            catch
            {
                textBlockTrend.Text = "Error executing request";
            }
        }
    }
}
