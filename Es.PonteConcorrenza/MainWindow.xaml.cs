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
        public static Uri semaforoROSSOuri = new Uri("semaforoROSSO.png", UriKind.Relative);
        public static Uri semaforoGIALLOuri = new Uri("semaforoGIALLO.png", UriKind.Relative);
        public static Uri semaforoVERDEuri = new Uri("semaforoVERDE.png", UriKind.Relative);
        public static ImageSource semaforoROSSO = new BitmapImage(semaforoROSSOuri);
        public static ImageSource semaforoGIALLO = new BitmapImage(semaforoGIALLOuri);
        public static ImageSource semaforoVERDE = new BitmapImage(semaforoVERDEuri);
        public static object x = new object();
        public static bool procediSX = false;
        public static bool procediDX = false;

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.counterSX = 0;
            MainWindow.counterDX = 0;
            semaforoROSSO.Freeze();
            semaforoGIALLO.Freeze();
            semaforoVERDE.Freeze();
            Thread t1 = new Thread(new ThreadStart(AggiornaSemafori));
            Thread t2 = new Thread(new ThreadStart(AggiornaAutoInLista));
            Thread t3 = new Thread(new ThreadStart(MovimentoAutoVersoDestra));
            Thread t4 = new Thread(new ThreadStart(MovimentoAutoVersoSinistra));
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
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
            while (true)
            {

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoSX.Source = semaforoROSSO;
                    imgSemaforoDX.Source = semaforoVERDE;
                    procediDX = true;
                    procediSX = false;
                }));

                Thread.Sleep(30000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoDX.Source = semaforoGIALLO;
                    procediDX = false;
                }));
                
                Thread.Sleep(4000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoSX.Source = semaforoVERDE;
                    imgSemaforoDX.Source = semaforoROSSO;                   
                    procediSX = true;
                }));

                Thread.Sleep(30000);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    imgSemaforoSX.Source = semaforoGIALLO;
                    procediSX = false;
                }));
                
                Thread.Sleep(4000);
            }
        }

        public void AggiornaAutoInLista()
        {
            while (true)
            {

                Thread.Sleep(500);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    lblCounterSX.Content = counterSX;
                }));
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    lblCounterDX.Content = counterDX;
                }));
            }
        }

        public void MovimentoAutoVersoDestra()
        {           
            while (true)
            {
                if(MainWindow.counterSX > 0)
                {                 
                    if(procediSX)
                    {
                        lock (x)
                        {
                           
                            int posIniziale = 100;
                            do
                            {

                                posIniziale++;
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    imgSX.Margin = new Thickness(posIniziale, 125, 0, 0);
                                }));
                                Thread.Sleep(8);

                            } while (posIniziale < 470);
                            
                            posIniziale = 100;
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                imgSX.Margin = new Thickness(posIniziale, 125, 0, 0);
                            }));

                            counterSX--;
                        }
                    }
                }
            }
        }
        public void MovimentoAutoVersoSinistra()
        {
            while (true)
            {
                if (MainWindow.counterDX > 0)
                {
                    if (procediDX)
                    {
                        lock (x)
                        {
                            int posIniziale = 510;
                            do
                            {

                                posIniziale--;
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    imgDX.Margin = new Thickness(posIniziale, 90, 0, 0);
                                }));
                                Thread.Sleep(8);

                            } while (posIniziale > 140);

                            posIniziale = 510;
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                imgDX.Margin = new Thickness(posIniziale, 90, 0, 0);
                            }));

                            counterDX--;
                        }
                    }
                }
            }
        }
    }
}
