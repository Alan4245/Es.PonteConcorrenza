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
using System.Threading;

namespace Es.PonteConcorrenza
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int counterSX;
        public static int counterDX;
        
        public MainWindow()
        {
            InitializeComponent();
            MainWindow.counterSX = 0;
            MainWindow.counterDX = 0;
            Thread t1 = new Thread(new ThreadStart(AggiornaSemafori));
            t1.Start();
        }

        private void btnAggiungiAutoSX_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                counterSX += int.Parse(txtBoxInputSX.Text);
                txtBoxInputSX.Clear();
            }
            catch(Exception ex)
            {

                MessageBox.Show("Errore durante l'aggiunta di auto: " + ex.Message);

            }

        }

        private void btnAggiungiAutoDX_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                counterDX += int.Parse(txtBoxInputDX.Text);
                txtBoxInputDX.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Errore durante l'aggiunta di auto: " + ex.Message);

            }

        }

        public void AggiornaSemafori()
        {
            Uri semaforoROSSOuri = new Uri("semaforoROSSO.png", UriKind.Relative);
            Uri semaforoGIALLOuri = new Uri("semaforoGIALLO.png", UriKind.Relative);
            Uri semaforoVERDEuri = new Uri("semaforoVERDE.png", UriKind.Relative);
            ImageSource semaforoROSSO = new BitmapImage(semaforoROSSOuri);
            ImageSource semaforoGIALLO = new BitmapImage(semaforoGIALLOuri);
            ImageSource semaforoVERDE = new BitmapImage(semaforoVERDEuri);
            semaforoROSSO.Freeze();
            semaforoGIALLO.Freeze();
            semaforoVERDE.Freeze();

            while (true)
            {

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoSX.Source = semaforoROSSO;
                    imgSemaforoDX.Source = semaforoVERDE;                   
                }));

                Thread.Sleep(30000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoDX.Source = semaforoGIALLO;
                }));
                
                Thread.Sleep(4000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoSX.Source = semaforoVERDE;
                    imgSemaforoDX.Source = semaforoROSSO;
                }));

                Thread.Sleep(30000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoSX.Source = semaforoGIALLO;
                }));
                
                Thread.Sleep(4000);
            }
        }
    }
}
