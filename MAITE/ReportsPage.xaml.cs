using GestioDeTornejos.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioDeTornejos.view
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            this.InitializeComponent();
        }

        private Payload payload;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e != null)
            {
                payload = (Payload)e.Parameter;
            }
            if (payload == null || payload.OcTornejos == null || payload.OcTornejos.Count <= 0 || payload.IndexTorneig < 0)
            {
                payload.Canvis = false;
                btReportGrups.IsEnabled = false;
            } else
            {
                btReportGrups.IsEnabled = true;
            }
        }

        private void btReportGrups_Click(object sender, RoutedEventArgs e)
        {
            String url = String.Format("http://92.222.27.83:8080/jasperserver/rest_v2/reports/m2-sgonzalez/PROJECTE/ClassificacionsReport.html?j_username=m2-sgonzalez&j_password=47119178K&torneigId={0}",payload.OcTornejos.ElementAt(payload.IndexTorneig).Id);
            wvReportResult.Navigate(new Uri(url));
            tbCarregant.Visibility = Visibility.Visible;
        }

        private void btReportSocis_Click(object sender, RoutedEventArgs e)
        {
            String url = "http://92.222.27.83:8080/jasperserver/rest_v2/reports/m2-sgonzalez/PROJECTE/SocisReport.html?j_username=m2-sgonzalez&j_password=47119178K";
            wvReportResult.Navigate(new Uri(url));
            tbCarregant.Visibility = Visibility.Visible;
        }

        private void wvReportResult_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            tbCarregant.Visibility = Visibility.Collapsed;
        }
    }
}
