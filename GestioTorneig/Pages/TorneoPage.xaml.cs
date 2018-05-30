using GestioTorneig.BD;
using GestioTorneig.Controls;
using GestioTorneig.Dialogs;
using GestioTorneig.Model;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using static GestioTorneig.Controls.ControlAgenda;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace GestioTorneig.Pages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class TorneoPage : Page

    {
        static Torneo torneoActual;
        static Grup GActual = null;
        static int grupActual = 0;
        static int mismogrupo = 0;
        static bool primero = true;


        static public ObservableCollection<Soci> socis { get; set; } = new ObservableCollection<Soci>();
        static public ObservableCollection<Socilist> socislist { get; set; } = new ObservableCollection<Socilist>();
        public ObservableCollection<Partida> partidas { get; set; } = new ObservableCollection<Partida>();
        public ObservableCollection<TorneoActiu> torneosActius { get; set; } = new ObservableCollection<TorneoActiu>();
        public ObservableCollection<Ttancats> torneosTancats { get; set; } = new ObservableCollection<Ttancats>();

        public ObservableCollection<Torneo> torneos { get; set; } = new ObservableCollection<Torneo>();
        public ObservableCollection<Grup> grups { get; set; } = new ObservableCollection<Grup>();

        /// private List<string> horesDia = new List<string>() { "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00" };

        // public List<Enums.MOTIU_VICTORIA> EstatsSinistre { get; set; } = new List<Enums.MOTIU_VICTORIA>();
        public List<String> Modalitats { get; set; } = new List<String>();
        private BDConnector con;
        public TorneoPage()
        {
            this.InitializeComponent();
            Loaded += TorneoPage_Loaded;

        }

        private void TorneoPage_Loaded(object sender, RoutedEventArgs e)
        {

            prepararLlistaInscrit();
            prepararLlistaTorneos(lvSTorneos);
            prepararLlistaTorneosTancat(lvSTorneos);
            //con = new BDConnector("server=127.0.0.1;uid=root;pwd=root;database=projecte;SslMode=None;");
            con = new BDConnector("server=92.222.27.83;uid=m2-mhinojosa;pwd=46776946Y;database=m2_mhinojosa;SslMode=None;");
            getModalitat();
            cboxModalitat.ItemsSource = Modalitats;
            socislist = con.getLlistaSocis();   
            ObservableCollection<object> socisObject = new ObservableCollection<object>(socislist);  
            lvSInscrits.ItemsSource = socisObject;
     
        }


        //    lvSinistres.onDouble_tap += LvSinistres_onDouble_tap;
        //    Clients = con.getLlistaClients();
        //    cboxClient.ItemsSource = Clients;
        //    cboxClient.DisplayMemberPath = "FullName";
        //    getEstatsSinistre();
        //    Sinistres = con.getLlistaSinistres();
        //    ObservableCollection<object> sinistresObject = new ObservableCollection<object>(Sinistres);
        //    lvSinistres.ItemsSource = sinistresObject;
        //    lvSinistresSenseCita.onSelected_changed += LvSinistresSenseCita_onSelected_changed;
        //    cboxHoresDia.ItemsSource = horesDia;
        //    cboxHoresDia.SelectedIndex = 0;        //}
        //private void LvSinistresSenseCita_onSelected_changed(object sender, EventArgs e)        //{
        //    btnAssignaCita.IsEnabled = true;        //}

        private void getModalitat()
        {
            Modalitats.Add("Lliure");
            Modalitats.Add("a 1 Banda");
            Modalitats.Add("a 3 Bandes");

        }


        private void ControlCalendari_DateChanged(object sender, EventArgs e)
        {
            ControlCalendar cc = (ControlCalendar)sender;
            Debug.WriteLine(cc.MCurrentDate.Date);
        }



        //private async void btnNou_editar_Click(object sender, RoutedEventArgs e)
        //{
        //    Button btnPulsat = (Button)sender;

        //    if (btnPulsat.Tag.ToString().Equals("Editar"))
        //    {
        //        DialogNouPerit dialog = new DialogNouPerit(con, false, (Inscrit)lvPerits.ObjecteSeleccionat);
        //        ContentDialogResult res = await dialog.ShowAsync();
        //        lvPerits.ItemsSource = new ObservableCollection<object>(lvPerits.ItemsSource);
        //    }
        //    else
        //    {
        //        Inscrit p = new Inscrit();
        //        DialogNouPerit dialog = new DialogNouPerit(con, true, p);
        //        ContentDialogResult res = await dialog.ShowAsync();
        //        if (dialog.BotoClicat == Enums.BOTO.DESAR)
        //        {
        //            lvPerits.ItemsSource.Add(dialog.PeritActual);
        //        }
        //    }
        //}

        //private void btnEsborrar_Click(object sender, RoutedEventArgs e)
        //{
        //    string missatgeError = "";
        //    con.deletePerit(((Inscrit)lvPerits.ObjecteSeleccionat).Numero, out missatgeError);
        //    tbMissatgeError.Text = missatgeError;
        //    Debug.WriteLine("MISSATGE ? :" + missatgeError);
        //    if (missatgeError.Equals(""))
        //    {
        //        lvPerits.ItemsSource.Remove((Inscrit)lvPerits.ObjecteSeleccionat);
        //    }
        //    espera();

        //}

        //private async void espera()
        //{
        //    await System.Threading.Tasks.Task.Delay(2000);
        //    tbMissatgeError.Text = "";
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnFiltreOn_Click(object sender, RoutedEventArgs e)
        //{
        //    var date = dpDataSinistre.Date;
        //    DateTime? dataSinistre = null;
        //    if (date != null)
        //    {
        //        dataSinistre = date.Value.DateTime;
        //    }
        //    Enums.MOTIU_VICTORIA estatSinistre;
        //    if (cboxEstatSinistre.SelectedIndex != -1)
        //    {
        //         estatSinistre = (Enums.MOTIU_VICTORIA)cboxEstatSinistre.SelectedItem;
        //    }
        //    else {
        //         estatSinistre = Enums.MOTIU_VICTORIA.NOTHING;
        //    }
        //    SociList client = (SociList)cboxClient.SelectedItem;

        //    lvSinistres.ItemsSource = new ObservableCollection<object>(con.getSinistresFiltrats(dataSinistre, estatSinistre,
        //                                client == null ? 0 : client.Numero, ((Inscrit)lvPerits.ObjecteSeleccionat).Numero));

        //}

        //private void btnFiltreOff_Click(object sender, RoutedEventArgs e)
        //{
        //    desactivarFiltre();

        //}

        //private void desactivarFiltre()
        //{
        //    dpDataSinistre.Date = null;
        //    cboxEstatSinistre.SelectedIndex = 0;
        //    cboxClient.SelectedIndex = -1;

        //    lvSinistres.ItemsSource = new ObservableCollection<object>(con.getLlistaSinistres());
        //    lvSinistresSenseCita.ItemsSource = new ObservableCollection<object>();

        //    lvSinistres.IndexItemSeleccionat = -1;
        //}

        private void btnAddTorneo_Click(object sender, RoutedEventArgs e)
        {

            btnAddTorneo.IsEnabled = false;
            // string avui = DateTime.Today.Date.ToString();
            // avui = avui.Substring(0, avui.Length - 7) + cboxHoresDia.SelectedItem.ToString();
            var date = dpDataini.Date;
            //si esta vacio coje la fecha actual
            DateTime dataIni = DateTime.Today.Date;
            if (date != null)
            {
                dataIni = date.Value.DateTime;
            }
            // string missatgeError = "";
            // controlAgenda.comprovaAgenda(c, out missatgeError);
            btnCloseTorneo.IsEnabled = false;

            int idTorneoActual = con.getInsertIdTorneo();

            Modalitat m = new Modalitat(cboxModalitat.SelectedIndex, cboxModalitat.SelectedItem.ToString());
            torneoActual = new Torneo(idTorneoActual, tbTitol.Text, dataIni, m, true);
            Debug.WriteLine(dataIni.Date);

            con.insertTorneo(idTorneoActual, torneoActual.Nom, dataIni.Date, torneoActual.Modalitat, torneoActual.Preinscripcion);
            torneos.Add(torneoActual);


            //        lvSinistresSenseCita.ItemsSource.Remove(s);
            //    }
            //    canviTextBlock();


            //  }
        }

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            int caramboles = 0;
            int entrades = 0;
            string nom = "";
           // CloseGroup.IsEnabled = false;     

            try
            {

                if (!(String.IsNullOrEmpty(txtnom.Text.ToString())))
                {
                    nom = txtnom.Text.ToString();
                }
                if (!(String.IsNullOrEmpty(txtcCaram.ToString())))
                {
                    caramboles = Int32.Parse(txtcCaram.Text.ToString());
                }
                if (!(String.IsNullOrEmpty(txtEntrades.Text.ToString())))
                {
                    entrades = Int32.Parse(txtEntrades.Text.ToString());
                }

                if (lvSInscrits.IndexItemSeleccionat != -1)

                {

                    Socilist i = (Socilist)lvSInscrits.ObjecteSeleccionat;

                    grupActual = con.getIdGrup();
                    if (primero == true)
                    {
                        {
                            mismogrupo = grupActual;
                            primero = false;
                            int idPartida = 0;
                            Grup gp = new Grup(grupActual, torneoActual, nom, caramboles, entrades, mismogrupo);
                            torneoActual.Grups.Add(gp);
                            con.AddGroup(gp.Id, gp.Torneig.Id, gp.Descripcio, gp.CaramVictoria, gp.LimitEntrades, mismogrupo, idPartida);
                            GActual = gp;
                            lvSInscrits.ItemsSource.Remove(i);
                        }
                    }
                    lvSInscrits.ItemsSource.Remove(i);
                    DateTime d = DateTime.Today.Date;
                    Inscrit ins = new Inscrit(i.Id, GActual, torneoActual, d);

                    con.AddInscrit(ins.Numero, mismogrupo, ins.Torneo.Id, ins.DataInscrp);
                   // gp.Inscrits.Add(ins);                
                    GActual.addInscrit(ins);                  
                    if (GActual.Inscrits.Count() > 1)
                    {
                       btnCloseTorneo.IsEnabled = true;
                   }
                }
            }
            catch
            {
                //mensaje eerrooo

            }

            //private async void canviTextBlock()
            //{
            //    await System.Threading.Tasks.Task.Delay(2000);
            //    tbErrorCita.Text = "";
            //}

        }

        public void CloseGroup_Click(object sender, RoutedEventArgs e)
        {
            primero = false;
            // con.getIdGrup();
            grupActual = con.getIdGrup();
            txtnom.Text = "";
            txtcCaram.Text = "";
            txtEntrades.Text = "";
            controlBotones();

        }

      

        private void btnCloseTorneo_Click(object sender, RoutedEventArgs e)
        {

           // torneosActius = con.getTorneosActiu();
          //  ObservableCollection <object> torneosObject = new ObservableCollection<object>(torneosActius);
           // lvSTorneos.ItemsSource = torneosObject;
            torneosTancats = con.getTorneosTanctats();
            ObservableCollection<object> tancatsObject = new ObservableCollection<object>(torneosTancats);
            lvSTorneos.ItemsSource = tancatsObject;

            controlBotones();  
        }

        private void controlBotones()
        {
            foreach (Inscrit index in GActual.Inscrits)
            {
                for (int n = 0; n < GActual.Inscrits.Count -1; n++)
                {
                    if (index.Equals(GActual.Inscrits[n])){
                        n++;
                    }
                    int id_partida = con.getIdPartida();
                    Partida p = new Partida(id_partida, 0, 0, null, index, GActual.Inscrits[n ], 0, Enums.ESTAT_PARTIDA.PENDENT);
                    partidas.Add(p);
                    con.addPartida(p);
                    con.updateGrup(p.Id,GActual.Id);
                }
            }
            
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    if (e.Parameter is string)
        //    {

        //    }
        //    else
        //    {
        //        con = new BDConnector("server=127.0.0.1;uid=root;pwd=root;database=projecte;SslMode=None;");
        //        string missatgeError;
        //        //  Partida param = ((Partida)e.Parameter);
        //        con.ActualitzarEstatSinistre(((Partida)e.Parameter).Numero, ((Partida)e.Parameter).EstatSininistre.ToString());
        //        con.ActualitzarPeritSinistre(((Partida)e.Parameter).Numero, ((Partida)e.Parameter).Perit.Numero, out missatgeError);

        //        int i = Sinistres.IndexOf((Partida)e.Parameter);
        //    }
        //}

        private void prepararLlistaInscrit()
        {
            Dictionary<string, string> headerCamps = new Dictionary<string, string>();
            Dictionary<string, int> ordreCamps = new Dictionary<string, int>();
            Dictionary<string, bool> campsVisibles = new Dictionary<string, bool>();
            double[] midesColumnes = { 30, 30, 30, 30,30 };

            campsVisibles["Id"] = true;
            campsVisibles["Nom"] = true;
            campsVisibles["Cognom1"] = true;
            campsVisibles["Cognom2"] = true;
            campsVisibles["Nif"] = true;

            headerCamps["Id"] = "Id";
            headerCamps["Nom"] = "Nom";
            headerCamps["Cognom1"] = "Cognom1";
            headerCamps["Cognom2"] = "Cognom2";
            headerCamps["Nif"] = "NIF";

            ordreCamps["Id"] = 0;
            ordreCamps["Nom"] = 1;
            ordreCamps["Cognom1"] = 2;
            ordreCamps["Cognom2"] = 3;
            ordreCamps["Nif"] = 4;


            lvSInscrits.CampsVisibles = campsVisibles;
            lvSInscrits.Capsaleres = headerCamps;
            lvSInscrits.OrdreCamps = ordreCamps;
            lvSInscrits.MidaColumnes = midesColumnes;

           // lvSInscrits.onSelected_changed += lvSInscrits_onSelected_changed;

        }

        public void prepararLlistaTorneos(Control_ListViewDBCreator lvTorneos)
        {
            
     
            Dictionary<string, string> headerCamps = new Dictionary<string, string>();
            Dictionary<string, int> ordreCamps = new Dictionary<string, int>();
            Dictionary<string, bool> campsVisibles = new Dictionary<string, bool>();
            double[] midesColumnes = { 20, 20, 20 };

            campsVisibles["DataInici"] = true;
            campsVisibles["Qtgrup"] = true;
            campsVisibles["Qtpartida"] = true;
            campsVisibles["DataAssignacio"] = true;
           // campsVisibles["inscritA"] = true;


            headerCamps["DataInici"] = "Data Inici";
            headerCamps["Qtgrup"] = "qtat grup";
            headerCamps["Qtpartida"] = "qtat partida";
           // headerCamps["inscritA"] = "inscritA";

            ordreCamps["DataInici"] = 0;
            ordreCamps["Qtgrup"] = 1;
            ordreCamps["Qtpartida"] = 2;
            // ordreCamps["inscritA"] = 3;


            lvTorneos.CampsVisibles = campsVisibles;
            lvTorneos.Capsaleres = headerCamps;
            lvTorneos.OrdreCamps = ordreCamps;
            lvTorneos.MidaColumnes = midesColumnes;
        }

        public void prepararLlistaTorneosTancat(Control_ListViewDBCreator lvTorneos)
        {


            Dictionary<string, string> headerCamps = new Dictionary<string, string>();
            Dictionary<string, int> ordreCamps = new Dictionary<string, int>();
            Dictionary<string, bool> campsVisibles = new Dictionary<string, bool>();
            double[] midesColumnes = { 20, 20, 20 ,20};

            campsVisibles["DataClosed"] = true;
            campsVisibles["Desc"] = true;
            campsVisibles["Ganador"] = true;
            campsVisibles["DataAssignacio"] = true;
             campsVisibles["inscritA"] = true;


            headerCamps["DataClosed"] = "Data  closed";
            headerCamps["Desc"] = "Descripcion";
            headerCamps["Ganador"] = "qtat partida";
             headerCamps["inscritA"] = "inscritA";

            ordreCamps["DataClosed"] = 0;
            ordreCamps["Desc"] = 1;
            ordreCamps["Ganador"] = 2;
            ordreCamps["inscritA"] = 3;


            lvTorneos.CampsVisibles = campsVisibles;
            lvTorneos.Capsaleres = headerCamps;
            lvTorneos.OrdreCamps = ordreCamps;
            lvTorneos.MidaColumnes = midesColumnes;
        }









        //private void LvPerits_onSelected_changed(object sender, EventArgs e)
        //{
        //    Control_ListViewDBCreator lvPerits = (Control_ListViewDBCreator)sender;
        //    Inscrit p = (Inscrit)lvPerits.ObjecteSeleccionat;
        //    if (p != null)
        //    {
        //        lvSinistres.ItemsSource = new ObservableCollection<object>(con.getSinistresFiltrats(null, Enums.MOTIU_VICTORIA.NOTHING, 0, p.Numero));
        //        btnFiltreOn.IsEnabled = lvSinistres.ItemsSource.Count > 0;


        //        lvSinistresSenseCita.ItemsSource = getSinistresSenseCita(p.Numero);

        //        btnEditar.IsEnabled = true;
        //        btnEsborrar.IsEnabled = true;

        //        controlAgenda.CitesPerit = p.Cites;

        //    }





        //}

        //private ObservableCollection<object> getSinistresSenseCita(int numero)
        //{
        //    prepararLlistaSinistres(lvSinistresSenseCita);
        //    ObservableCollection<object> sinistres = new ObservableCollection<object>();
        //    foreach (object s in lvSinistres.ItemsSource)
        //    {
        //        bool trobat = false;
        //        if (((Partida)s).Perit.Cites.Count != 0)
        //        {
        //            foreach (Reserva c in ((Partida)s).Perit.Cites)
        //            {
        //                if (c.NumSinistre == ((Partida)s).Numero)
        //                {
        //                    trobat = true;
        //                    break;
        //                }
        //            }
        //            if (!trobat)
        //            {
        //                sinistres.Add(s);
        //            }
        //        }
        //    }

        //    return sinistres;
        //}

        //private void LvSinistres_onDouble_tap(object sender, EventArgs e)
        //{
        //    Control_ListViewDBCreator lv = (Control_ListViewDBCreator)sender;
        //    Partida s = (Partida)lv.ObjecteSeleccionat;
        //    EstadisticaModalitat sd = new EstadisticaModalitat();

        //    sd.Sinistre = (Partida)lv.ObjecteSeleccionat;
        //    sd.Perits = Perits;

        //    this.Frame.Navigate(typeof(SinistreDetallPage), sd);

        //}








    }





}

