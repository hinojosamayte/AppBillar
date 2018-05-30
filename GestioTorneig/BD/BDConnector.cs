using MySql.Data.MySqlClient;
using GestioTorneig.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace GestioTorneig.BD
{
    public class BDConnector
    {
        #region props
        public MySqlConnection Connection { get; set; }

        public string ConnectionString { get; set; }

        private ObservableCollection<Soci> inscrits;
        private ObservableCollection<Modalitat> modalitats;

        //public ObservableCollection<SociList> Clients
        //{
        //    get
        //    {
        //        if (clients == null)
        //        {
        //            clients = new ObservableCollection<SociList>();
        //            // clients = getLlistaClients();
        //        }
        //        return clients;
        //    }
        //    set { clients = value; }
        //}


        //public ObservableCollection<Modalitat> Modalitats
        //{
        //    get
        //    {
        //        if (modalitats == null)
        //        {
        //            modalitats = new ObservableCollection<Modalitat>();
        //         modalitats = getLlistaModalitat();
        //        }
        //        return modalitats;
        //    }
        //    set { modalitats = value; }
        //}

        public string modalitatbyId(int idModalitat)
        {

            string desc = null;

            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);


                using (Connection = new MySqlConnection(ConnectionString))
                {



                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select desc_modalitat from modalitat  where id = @idModalitat";
                    command.Parameters.Add(new MySqlParameter("idModalitat", idModalitat));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            desc = reader.GetString("desc_modalitat");
                        }

                    }


                }
            }

            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD modalitatbyId  " + e.Message);
            }

            return desc;





        }

        public int insertTorneo(int id, string nom, DateTime data_inici, Modalitat modalitat, Object preinscripcion)
        {
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                Debug.WriteLine(data_inici.Date.ToString("yyyy-MM-dd"));
                string data = data_inici.Date.ToString("yyyy-MM-dd");

                //data_inici.Date.ToString("yyyy-MM-dd")));

                using (Connection = new MySqlConnection(ConnectionString))
                {
                   
                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "insert into torneo values(@id,@nom,data_inici,@id_modalitat, @id_inscrit,@preinscricion,@data_closed)";
                    command.Parameters.Add(new MySqlParameter("id", id));
                    command.Parameters.Add(new MySqlParameter("nom", nom));
                    command.Parameters.Add(new MySqlParameter("data_inici", data_inici));
                    command.Parameters.Add(new MySqlParameter("id_modalitat", modalitat.Id));
                    command.Parameters.Add(new MySqlParameter("id_inscrit", null));
                    command.Parameters.Add(new MySqlParameter("preinscricion", true));
                    command.Parameters.Add(new MySqlParameter("data_closed", null));
                    command.ExecuteNonQuery();

                }

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD INSERT TORNEO:  " + e.Message);
            }
            return id;
        }





        public ObservableCollection<Torneo> getLlistaTorneos()
        {

            ObservableCollection<Torneo> torneos = new ObservableCollection<Torneo>();

            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);


                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select * from torneo order by estadistica_modalitat";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            int numero = reader.GetInt16("id");
                            string nom = reader.GetString("nom");

                            DateTime dataIni = Convert.ToDateTime(reader["data_inici"]);
                            int idModalitat = reader.GetInt16("id_modalitat");
                            //idInscrit null
                            string num;
                            if (reader.IsDBNull(reader.GetInt16("id_inscrit")))
                            {
                                num = null;
                            }
                            else
                            {
                                num = reader.GetString(reader.GetOrdinal("id_inscrit"));
                                int idInscrit = Int16.Parse(num);
                            }

                            Boolean inscr = reader.GetBoolean("preinscricion");
                            Debug.WriteLine("Cognom: " + nom);

                            Modalitat modActual = new Modalitat(idModalitat, modalitatbyId(idModalitat));

                            torneos.Add(new Torneo(numero, nom, dataIni, modActual, inscr));

                        }

                    }

                }

                //foreach (Inscrit p in perits)
                //{
                //    p.Cites = getCitesByPeritId(p.Numero);


                // }

            }



            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD GetLlistaTorneo  " + e.Message);
            }



            return torneos;


        }










        #endregion props

        public BDConnector(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        //  public ObservableCollection<Soci> getLlistaSocis()
        public ObservableCollection<Socilist> getLlistaSocis()
        {
            ObservableCollection<Socilist> socislist = new ObservableCollection<Socilist>();

            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select * from soci";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            int numero = reader.GetInt32("id");
                            string nif = reader.GetString("nif");
                            string nom = reader.GetString("nom");
                            DateTime dataAlta = Convert.ToDateTime(reader["data_alta"]);
                            string cognom1 = reader.GetString("cognom1");
                            //string cognom2 = reader.GetString("cognom2");
                            //checkeja null
                            string cognom2 = reader[5] as string;

                            string passwordHash = reader.GetString("passwordHash");
                            //longblob
                            int estadistica = reader.GetInt16("estadistica_modalitat");
                            //EstadisticaModalitat em = new EstadisticaModalitat(estadistica,getEstadisticaByID(estadistica));
                            Socilist s = new Socilist(numero, nif, nom, cognom1, cognom2, estadistica);

                            socislist.Add(s);
                        }
                    }
                    return socislist;
                }

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD GetLlistaClients:  " + e.Message);
            }


            return socislist;
        }



        public int existentegrupo(int id_soci)
        {
            int numero = 0;

            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);


                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select count(id_grup) as numero from inscrit where  id_soci= @id_Soci";
                    command.Parameters.Add(new MySqlParameter("id_soci", id_soci));
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            numero = reader.GetInt16("numero");
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD existentegrup:  " + e.Message);
            }

            return numero;
        }

        public int getIdGrup()
        {
            int index = 0;
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select max(comptador) from comptadors where taula = 'GRUP'";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            index = reader.GetInt16("max(comptador)") + 1;
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR GET ID GRUP SEGUENT BD:  " + e.Message);
            }

            ActualitzarComptadorsGrup(index);

            return index;
        }



        public void AddInscrit(int numero, int grup, int torneo, DateTime dataInscrp)
        {
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "insert into inscrito values(@id_soci,@id_grup, @id_torneo,@data_inscrp)";
                    command.Parameters.Add(new MySqlParameter("id_soci", numero));
                    command.Parameters.Add(new MySqlParameter("id_torneo", torneo));
                    command.Parameters.Add(new MySqlParameter("id_grup", grup));
                    command.Parameters.Add(new MySqlParameter("data_inscrp", dataInscrp));
                    command.ExecuteNonQuery();


                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD INSERT INSCRIT:  " + e.Message);
            }
        }

        public ObservableCollection<Ttancats> getTorneosTanctats()
        {
            ObservableCollection<Ttancats> cerrados = new ObservableCollection<Ttancats>();
            {



                try
                {
                    EncodingProvider provider = CodePagesEncodingProvider.Instance;
                    Encoding.RegisterProvider(provider);
                    using (Connection = new MySqlConnection(ConnectionString))
                    {

                        Connection.Open();
                        MySqlCommand command = Connection.CreateCommand();
                        command.CommandText = "SELECT data_closed, g.descripcio, p.guanyador , p.inscrit_a, p.inscrit_b FROM partida p left JOIN grup g ON g.id_partida = p.id LEFT JOIN torneo t ON t.id = g.id_torneig where t.preinscricion = 0 group by g.id";

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                object datat = reader["data_closed"];
                                DateTime? data = (datat == null) ? (DateTime?)null : Convert.ToDateTime(datat);
                                string desc = reader.GetString("descripcio");
                                string enumeGanador = reader[3] as string;
                                //string cognom2 = reader[5] as string;


                                int iA = reader.GetInt32("inscrit_a");
                                int iB= reader.GetInt32("inscrit_b");
                               


                                Ttancats s = new Ttancats(data,  desc, Enums.GUANYADOR.A, iA, iB);

                               cerrados.Add(s);
                            }
                        }
                        
                    }

                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("ERROR BD INSERT CITA:  " + e.Message);
                }


                return cerrados;

            }
        }


            

        public ObservableCollection<TorneoActiu> getTorneosActiu()
        {
            ObservableCollection<TorneoActiu> actius = new ObservableCollection<TorneoActiu>();

            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);


                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select data_inici, count(g.id) as Qtgrup, count(p.id)  as Qtpartida FROM torneo t  LEFT JOIN grup g ON t.id = g.id_torneig LEFT JOIN partida p ON g.id_partida = p.id GROUP BY t.id, p.id limit 0,6";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            DateTime dataIni = Convert.ToDateTime(reader["data_inici"]);
                            int qtygrup = reader.GetInt16("Qtgrup");
                            int qtypartida = reader.GetInt16("Qtpartida");
                            actius.Add(new TorneoActiu(dataIni, qtygrup, qtypartida));

                        }
                    }
                }

                //foreach (Inscrit p in perits)
                //{
                //    p.Cites = getCitesByPeritId(p.Numero);
                //}
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD getTorneosActiu:  " + e.Message);
            }
            return actius;
        }


        public void updateGrup(int id, int grup)
        {
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {
                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "update grup set id_partida =@id where id=@grup";
                    command.Parameters.Add(new MySqlParameter("id", id));
                    command.Parameters.Add(new MySqlParameter("grup", grup));
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD UpdateGrup  " + e.Message);
            }
        }







        internal void addPartida(Partida p)
        {
            {
                try
                {
                    EncodingProvider provider = CodePagesEncodingProvider.Instance;
                    Encoding.RegisterProvider(provider);
                    using (Connection = new MySqlConnection(ConnectionString))
                    {
                        string a = "PENDIENTE";
                        Connection.Open();
                        MySqlCommand command = Connection.CreateCommand();
                        command.CommandText = "insert into partida values(@id,@carambolesA,@carambolesB,@data_partida,@inscrit_a,@inscrit_b,@num_entrades,@estat_partida,@guanyador,@motiu_victoria)";
                        command.Parameters.Add(new MySqlParameter("id", p.Id));
                        command.Parameters.Add(new MySqlParameter("carambolesA", p.CarambolesA));
                        command.Parameters.Add(new MySqlParameter("carambolesB", p.CarambolesB));
                        command.Parameters.Add(new MySqlParameter("data_partida", p.DataPartida));
                        command.Parameters.Add(new MySqlParameter("inscrit_a", p.InscritA.Numero));
                        command.Parameters.Add(new MySqlParameter("inscrit_b", p.InscritB.Numero));
                        command.Parameters.Add(new MySqlParameter("num_entrades", p.NumEntrades));
                        command.Parameters.Add(new MySqlParameter("estat_partida", a));
                        command.Parameters.Add(new MySqlParameter("guanyador", null));
                        command.Parameters.Add(new MySqlParameter("motiu_victoria", null));
                        command.ExecuteNonQuery();



                    }
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("ERROR BD INSERT PARTIDA:  " + e.Message);
                }
            }
        }

        internal int getIdPartida()
        {

            int index = 0;
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {
                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select max(comptador) from comptadors where taula = 'PARTIDA'";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            index = reader.GetInt16("max(comptador)") + 1;
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR GET ID PARTIDA SEGUENT BD:  " + e.Message);
            }

            ActualitzarComptadorsPartida(index);

            return index;


        }

        private void ActualitzarComptadorsPartida(int index)
        {
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "update comptadors set comptador = @index where taula = 'PARTIDA'";
                    command.Parameters.Add(new MySqlParameter("index", index));
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD actualitzarComptadorsTorneo  " + e.Message);
            }
        }


        public void AddGroup(int id, int idTorneig, string descripcio, int carambolesVistoria, int limitEntrades, int mismogrupo, int idPartida)
        {
            //  int numInst = getInsertIdInscrit();
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "insert into grup values(@id,@id_torneig, @descripcio,@caramboles_vistoria,@limit_entrades,@mismo_grupo,@id_partida )";
                    command.Parameters.Add(new MySqlParameter("id", id));
                    command.Parameters.Add(new MySqlParameter("id_torneig", idTorneig));
                    command.Parameters.Add(new MySqlParameter("descripcio", descripcio));
                    command.Parameters.Add(new MySqlParameter("caramboles_vistoria", carambolesVistoria));
                    command.Parameters.Add(new MySqlParameter("limit_entrades", limitEntrades));
                    command.Parameters.Add(new MySqlParameter("mismo_grupo", mismogrupo));
                    command.Parameters.Add(new MySqlParameter("id_partida", null));
                    command.ExecuteNonQuery();

                    //insert into grup values(14, 3, "grupo master sin participacones en otros torneos", 120, 50, null, null);
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD INSERT GRUPO:  " + e.Message);
            }

        }




        public int getInsertIdTorneo()
        {
            int index = 0;
            try
            {

                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);


                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "select max(comptador) from comptadors where taula = 'TORNEO'";
                    // command.CommandText = "select max(Id) from torneo ";


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            index = reader.GetInt16("max(comptador)") + 1;
                            // index = reader.GetInt16("max(Id)") + 1;
                        }
                    }
                } 
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR GET ID TORNEO SEGUENT BD:  " + e.Message);
            }
            ActualitzarComptadorsTorneo(index);
            return index;
        }




        private void ActualitzarComptadorsTorneo(int index)
        {
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {

                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "update comptadors set comptador = @index where taula = 'TORNEO'";
                    command.Parameters.Add(new MySqlParameter("index", index));
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD actualitzarComptadorsTorneo  " + e.Message);
            }
        }



        private void ActualitzarComptadorsGrup(int index)
        {
            try
            {
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);
                using (Connection = new MySqlConnection(ConnectionString))
                {
                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    command.CommandText = "update comptadors set comptador = @index where taula = 'GRUP'";
                    command.Parameters.Add(new MySqlParameter("index", index));
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("ERROR BD actualitzarComptadorsGrup  " + e.Message);
            }
        }
    }







    //    public void actualitzarPerit(int numPerit,string nif,string nom, string cognom1, string cognom2, DateTime dataNaix, string loggin, string password)
    //    {

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "update perit set nif = @nif,nom = @nom, cognom1 = @cognom1, cognom2 = @cognom2, data_naix = @dataNaix, login = @loggin, password = @password where numero = @numPerit";
    //                command.Parameters.Add(new MySqlParameter("numPerit", numPerit));
    //                command.Parameters.Add(new MySqlParameter("nif", nif));
    //                command.Parameters.Add(new MySqlParameter("nom", nom));
    //                command.Parameters.Add(new MySqlParameter("cognom1", cognom1));
    //                command.Parameters.Add(new MySqlParameter("cognom2", cognom2));
    //                command.Parameters.Add(new MySqlParameter("dataNaix", dataNaix));
    //                command.Parameters.Add(new MySqlParameter("loggin", loggin));
    //                command.Parameters.Add(new MySqlParameter("password", password));

    //                command.ExecuteNonQuery();

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR Actualitzant Inscrit BD:  " + e.Message);
    //        }

    //    }

    //    public void deletePerit(int numPerit, out string misstageError)
    //    {
    //        misstageError = "";
    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "delete from perit where numero = @numPerit";
    //                command.Parameters.Add(new MySqlParameter("numPerit", numPerit));

    //                command.ExecuteNonQuery();

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD deletePerit:  " + e.Message);
    //            misstageError = "No es possible esborrar aquest perit, ja que ja te sinistres assignats";
    //        }

    //    }








    //    private ObservableCollection<Reserva> getCitesByPeritId(int idPerit)
    //    {
    //        ObservableCollection<Reserva> cites = new ObservableCollection<Reserva>();

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select * from cita where num_perit = @idPerit";
    //                command.Parameters.Add(new MySqlParameter("idPerit", idPerit));

    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        int id = reader.GetInt16("id");
    //                        int numPerit = reader.GetInt16("num_perit");
    //                        DateTime diaHora = Convert.ToDateTime(reader["dia_hora"]);
    //                        int numSinistre = reader.GetInt16("num_sinistre");
    //                        int duracio = reader.GetInt16("duracio");


    //                        cites.Add(new Reserva(id,numPerit,diaHora,numSinistre,duracio));

    //                    }
    //                }
    //            }               
    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD getCitesByPeritId:  " + e.Message);
    //        }



    //        return cites;
    //    }




    //    public ObservableCollection<Partida> getLlistaSinistresSenseCita(int numPerit) {
    //        ObservableCollection<Partida> sinistres = new ObservableCollection<Partida>();

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select * from sinistre where num_perit = @numPerit and numero not in(select num_sinistre from cita)";
    //                command.Parameters.Add(new MySqlParameter("numPerit", numPerit));

    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        int numero = reader.GetInt16("numero");
    //                        object dataAssig = reader["data_assignacio"];
    //                        object dataTanc = reader["data_tancament"];
    //                        DateTime? dataAssignacio = (dataAssig == null) ? (DateTime?)null : Convert.ToDateTime(dataAssig);
    //                        DateTime dataObertura = Convert.ToDateTime(reader["data_obertura"]);
    //                        DateTime? dataTancament = (dataTanc == null) ? (DateTime?)null : Convert.ToDateTime(dataTanc);
    //                        string descripcio = reader.GetString("descripcio");
    //                        int numPolissa = reader.GetInt16("num_polissa");
    //                        int idPerit;
    //                        if (reader.IsDBNull(reader.GetOrdinal("num_perit"))) idPerit = 0;
    //                        else idPerit = reader.GetInt16(reader.GetOrdinal("num_perit"));

    //                        Enums.GUANYADOR tipusSinistre = Enums.getTipusSinistreFromString(reader.GetString("tipus_sinistre"));
    //                        Enums.MOTIU_VICTORIA estatSinistre = Enums.getMotiuVictoriaFromString(reader.GetString("estat_sinistre"));


    //                        Debug.WriteLine("Partida: " + numero);

    //                        sinistres.Add(new Partida(numero, dataAssignacio, dataObertura, dataTancament, descripcio, numPolissa, idPerit, tipusSinistre, estatSinistre));

    //                    }

    //                }

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD:  " + e.Message);
    //        }
    //        getPolicesPerId(sinistres);
    //        getPeritsPerId(sinistres);
    //        getTrucadesPerIdSinistre(sinistres);
    //        getInformesPericialsPerIdSinistre(sinistres);

    //        return sinistres;


    //    }

    //    public ObservableCollection<Partida> getLlistaSinistres()
    //    {

    //        ObservableCollection<Partida> sinistres = new ObservableCollection<Partida>();

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select * from sinistre";            

    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        int numero = reader.GetInt16("numero");
    //                        object dataAssig = reader["data_assignacio"];
    //                        object dataTanc = reader["data_tancament"];
    //                        DateTime? dataAssignacio = (dataAssig == null) ? (DateTime?)null : Convert.ToDateTime(dataAssig);
    //                        DateTime dataObertura = Convert.ToDateTime(reader["data_obertura"]);
    //                        DateTime? dataTancament = (dataTanc == null) ? (DateTime?)null : Convert.ToDateTime(dataTanc);
    //                        string descripcio = reader.GetString("descripcio");
    //                        int numPolissa = reader.GetInt16("num_polissa");
    //                        int idPerit;
    //                        if (reader.IsDBNull(reader.GetOrdinal("num_perit"))) idPerit = 0;
    //                        else idPerit = reader.GetInt16(reader.GetOrdinal("num_perit"));

    //                        Enums.GUANYADOR tipusSinistre = Enums.getTipusSinistreFromString(reader.GetString("tipus_sinistre"));
    //                        Enums.MOTIU_VICTORIA estatSinistre = Enums.getMotiuVictoriaFromString(reader.GetString("estat_sinistre"));


    //                        Debug.WriteLine("Partida: " + numero);

    //                        sinistres.Add(new Partida(numero, dataAssignacio, dataObertura, dataTancament, descripcio, numPolissa, idPerit, tipusSinistre, estatSinistre));

    //                    }

    //                }

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD:  " + e.Message);
    //        }
    //        getPolicesPerId(sinistres);
    //        getPeritsPerId(sinistres);
    //        getTrucadesPerIdSinistre(sinistres);
    //        getInformesPericialsPerIdSinistre(sinistres);

    //        return sinistres;
    //    }



    //    public ObservableCollection<Partida> getSinistresFiltrats(DateTime? dataObertura, Enums.MOTIU_VICTORIA estatSinistre, int clientCod, int peritCod) {

    //        ObservableCollection<Partida> sinistresFiltrats = new ObservableCollection<Partida>();

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                if (clientCod > 0)
    //                {
    //                    command.CommandText = "select * from sinistre s join polissa p on(s.num_polissa = p.numero) where 1=1 and p.num_client = @clientCod ";
    //                    command.Parameters.Add(new MySqlParameter("clientCod", clientCod));
    //                }
    //                else {
    //                    command.CommandText = "select * from sinistre where 1=1 ";
    //                }

    //                if (dataObertura != null)
    //                {
    //                    command.CommandText += "and data_obertura = @dataSinistre ";
    //                    command.Parameters.Add(new MySqlParameter("dataSinistre", dataObertura));
    //                }
    //                if (estatSinistre != Enums.MOTIU_VICTORIA.NOTHING)
    //                {
    //                    command.CommandText += "and estat_sinistre = @estatSinistre ";
    //                    command.Parameters.Add(new MySqlParameter("estatSinistre", estatSinistre.ToString()));
    //                }
    //                if (peritCod != 0)
    //                {
    //                    command.CommandText += "and num_perit = @peritCod";
    //                    command.Parameters.Add(new MySqlParameter("peritCod", peritCod));
    //                }

    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        int numero = reader.GetInt16("numero");
    //                        object dataAssig = reader["data_assignacio"];
    //                        object dataTanc = reader["data_tancament"];
    //                        //object dataOb = reader["data_obertura"];
    //                        DateTime? dataAssignacio = (dataAssig == null) ? (DateTime?)null : Convert.ToDateTime(dataAssig);
    //                        DateTime dObertura = Convert.ToDateTime(reader["data_obertura"]);
    //                        DateTime? dataTancament = (dataTanc == null) ? (DateTime?)null : Convert.ToDateTime(dataTanc);
    //                        string descripcio = reader.GetString("descripcio");
    //                        int numPolissa = reader.GetInt16("num_polissa");
    //                        int idPerit;                            
    //                        if (reader.IsDBNull(reader.GetOrdinal("num_perit"))) idPerit = 0;
    //                        else idPerit = reader.GetInt16(reader.GetOrdinal("num_perit"));


    //                        Enums.GUANYADOR tipusSinistre = Enums.getTipusSinistreFromString(reader.GetString("tipus_sinistre"));
    //                        Enums.MOTIU_VICTORIA eSinistre = Enums.getMotiuVictoriaFromString(reader.GetString("estat_sinistre"));




    //                        sinistresFiltrats.Add(new Partida(numero,dataAssignacio,dObertura,dataTancament,descripcio,numPolissa,idPerit,tipusSinistre,eSinistre));

    //                    }

    //                }
    //            }



    //        }catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD GetSinistresFiltrats:  " + e.Message);
    //        }

    //        getPolicesPerId(sinistresFiltrats);
    //        getPeritsPerId(sinistresFiltrats);
    //        getTrucadesPerIdSinistre(sinistresFiltrats);
    //        getInformesPericialsPerIdSinistre(sinistresFiltrats);

    //        return sinistresFiltrats;
    //    }     



    //    private void getPolicesPerId(ObservableCollection<Partida> sinistresFiltrats)
    //    {
    //        foreach (Partida s in sinistresFiltrats) {


    //            int idPolissa = s.NumPolissa;

    //            try
    //            {
    //                EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //                Encoding.RegisterProvider(provider);


    //                using (Connection = new MySqlConnection(ConnectionString))
    //                {

    //                    Connection.Open();
    //                    MySqlCommand command = Connection.CreateCommand();
    //                    command.CommandText = "select * from polissa where numero = @idPolissa";
    //                    command.Parameters.Add(new MySqlParameter("idPolissa", idPolissa));

    //                    using (MySqlDataReader reader = command.ExecuteReader())
    //                    {

    //                        while (reader.Read())
    //                        {

    //                            int numero = reader.GetInt16("numero");
    //                            DateTime dataInici = Convert.ToDateTime(reader["data_inici"]);
    //                            DateTime dataFi = Convert.ToDateTime(reader["data_fi"]);
    //                            Decimal import = reader.GetDecimal("import_polissa");
    //                            Decimal importContinent = reader.GetDecimal("import_continent");
    //                            Decimal importContingut = reader.GetDecimal("import_contingut");
    //                            int numClient = reader.GetInt16("num_client");
    //                            string poblacio = reader.GetString("poblacio");
    //                            string liniaAdreca = reader.GetString("linia_adreca");
    //                            Enums.TIPUS_HABITATGE tipusHabitatge = Enums.getTipusHabitatgeFromString(reader.GetString("tipus_habitatge"));

    //                            SociList c = getClientById(numClient);

    //                            s.Polissa = new Torneo(numero, dataInici, dataFi, import, importContinent, importContingut, c, poblacio, liniaAdreca, tipusHabitatge);

    //                        }

    //                    }

    //                }

    //            }
    //            catch (MySqlException e)
    //            {
    //                Debug.WriteLine("ERROR BD getPolicesPerId:  " + e.Message);
    //            }

    //        }
    //    }



    //private void getPeritsPerId(ObservableCollection<Partida> sinistresFiltrats)
    //{
    //    foreach (Partida s in sinistresFiltrats) {

    //        int idPerit = s.IdPerit;

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select * from perit where numero = @idPerit";
    //                command.Parameters.Add(new MySqlParameter("idPerit", idPerit));
    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        int numero = reader.GetInt16("numero");
    //                        string nif = reader.GetString("nif");
    //                        string nom = reader.GetString("nom");
    //                        string cognom1 = reader.GetString("cognom1");
    //                        //checkeja null
    //                        string cognom2 = reader[5] as string;
    //                        DateTime dataNaix = Convert.ToDateTime(reader["data_naix"]);
    //                        string loggin = reader.GetString("login");
    //                        string password = reader.GetString("password");

    //                        s.Perit = new Inscrit(numero, nif, nom, cognom1, cognom2, dataNaix, loggin, password);

    //                    }

    //                }

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD getPeritsPerId:  " + e.Message);
    //        }


    //        //TODO fer el get cites per num Partida.
    //        if (s.Perit != null)
    //        {
    //            s.Perit.Cites = getCitesByPeritId(s.Perit.Numero);
    //        }
    //    }
    //}



    //    private void getTrucadesPerIdSinistre(ObservableCollection<Partida> sinistresFiltrats)
    //    {
    //        foreach (Partida s in sinistresFiltrats) {

    //            int idSinistre = s.Numero;

    //            try
    //            {
    //                EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //                Encoding.RegisterProvider(provider);


    //                using (Connection = new MySqlConnection(ConnectionString))
    //                {

    //                    Connection.Open();                        
    //                    MySqlCommand command = Connection.CreateCommand();
    //                    command.CommandText = "select * from trucada where num_sinistre = @idSinistre";
    //                    command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));
    //                    using (MySqlDataReader reader = command.ExecuteReader())
    //                    {
    //                        ObservableCollection<Modalitat> trucadesSinistre = new ObservableCollection<Modalitat>();
    //                        while (reader.Read())
    //                        {

    //                            int numero = reader.GetInt16("num_sinistre");
    //                            DateTime dataHora = Convert.ToDateTime(reader["data_hora"]);
    //                            string descripcio = reader.GetString("descripcio");
    //                            string personaContacte = reader.GetString("persona_contacte");


    //                            trucadesSinistre.Add(new Modalitat(numero, dataHora, descripcio, personaContacte));

    //                        }

    //                        s.Trucades = trucadesSinistre;

    //                    }

    //                }

    //            }
    //            catch (MySqlException e)
    //            {
    //                Debug.WriteLine("ERROR BD getTrucadesPerIdSinistre:  " + e.Message);
    //            }

    //        }
    //    }

    //    private void getInformesPericialsPerIdSinistre(ObservableCollection<Partida> sinistresFiltrats)
    //    {

    //        foreach (Partida s in sinistresFiltrats)
    //        {

    //            int idSinistre = s.Numero;

    //            try
    //            {
    //                EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //                Encoding.RegisterProvider(provider);


    //                using (Connection = new MySqlConnection(ConnectionString))
    //                {

    //                    Connection.Open();
    //                    MySqlCommand command = Connection.CreateCommand();
    //                    command.CommandText = "select * from informe_pericial where num_sinistre = @idSinistre";
    //                    command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));
    //                    using (MySqlDataReader reader = command.ExecuteReader())
    //                    {

    //                        while (reader.Read())
    //                        {

    //                            int numero = reader.GetInt16("num_sinistre");
    //                            DateTime dataEmisio = Convert.ToDateTime(reader["data_emisio"]);
    //                            Decimal importCobert = reader.GetDecimal("Import_cobert");
    //                            string informe = reader.GetString("informe");
    //                            Enums.ESTAT_PARTIDA estatInforme = Enums.getEstatPartidaFromString(reader.GetString("estat_informe"));
    //                            Enums.RESULTAT_PERITATGE resultatPeritatge = Enums.getResultatPeritatgeFromString(reader.GetString("resultat_peritatge"));

    //                            s.Informe = new Taula(numero, dataEmisio, importCobert, informe, s.Perit, estatInforme, resultatPeritatge);

    //                        }
    //                        if (s.Informe != null)
    //                        {
    //                            s.Informe.Entrades = getEntradesById(idSinistre);
    //                        }

    //                    }

    //                }

    //            }
    //            catch (MySqlException e)
    //            {
    //                Debug.WriteLine("ERROR BD GetInformesPericialsPerIdSinistre:  " + e.Message);
    //            }
    //        }
    //    }

    //    private ObservableCollection<Grup> getEntradesById(int idSinistre)
    //    {
    //        ObservableCollection<Grup> entrades = new ObservableCollection<Grup>();

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select * from entrada_informe where numero = @idSinistre";
    //                command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));
    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        int numero = reader.GetInt16("numero");
    //                        DateTime dataInforme = Convert.ToDateTime(reader["data_informe"]);
    //                        string descripcio = reader.GetString("descripcio");
    //                        bool postReparacio = reader.GetInt16("despres_reparacio") == 1;
    //                        Byte[] imatge = (byte[])reader["foto"];
    //                        Debug.WriteLine("DATA ENTRADA = " + dataInforme);



    //                        entrades.Add(new Grup(numero, dataInforme, descripcio, postReparacio, imatge));                            
    //                    }



    //                }

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD getEntradesById:  " + e.Message);
    //        }

    //        return entrades;
    //    }

    //    private List<Int64> getMidaFotos(int idSinistre)
    //    {
    //        List<Int64> midesFotos = new List<Int64>();

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select OCTET_LENGTH(foto) from entrada_informe where numero = @idSinistre order by ordre";
    //                command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));
    //                using (MySqlDataReader reader = command.ExecuteReader())
    //                {

    //                    while (reader.Read())
    //                    {

    //                        Int64 midaFoto = reader.GetInt64("OCTET_LENGTH(foto)");
    //                        midesFotos.Add(midaFoto);
    //                    }


    //                }

    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD getMidaFotos:  " + e.Message);
    //        }

    //        return midesFotos;
    //    }

    //    private SociList getClientById(int numClient) {

    //        foreach (SociList c in Clients) {
    //            if (c.Numero == numClient) return c;
    //        }

    //        return null;
    //    }

    //    private Int64 countInformeFromSinistre(int idSinistre) {
    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);
    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {
    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();
    //                command.CommandText = "select count(num_sinistre) from informe_pericial where num_sinistre = @idSinistre";
    //                command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));
    //              return (Int64)command.ExecuteScalar();
    //            }
    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD countInformeFromSinistre:  " + e.Message);
    //        }
    //        return 0;
    //    }

    //    public bool ActualitzarPeritSinistre(int idSinistre, int idPerit,out string missatgeError) {

    //        missatgeError = "";
    //        if (countInformeFromSinistre(idSinistre) == 0)
    //        {

    //            try
    //            {
    //                EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //                Encoding.RegisterProvider(provider);


    //                using (Connection = new MySqlConnection(ConnectionString))
    //                {

    //                    Connection.Open();
    //                    MySqlCommand command = Connection.CreateCommand();
    //                    command.CommandText = "update sinistre set num_perit = @idPerit where numero = @idSinistre";
    //                    command.Parameters.Add(new MySqlParameter("idPerit", idPerit));
    //                    command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));    

    //                    command.ExecuteNonQuery();

    //                }




    //            }
    //            catch (MySqlException e)
    //            {
    //                Debug.WriteLine("ERROR BD ActualitzarPeritSinistre:  " + e.Message);
    //            }



    //        }
    //        missatgeError = "El Inscrit ja ha començat a treballar en aquest sinistre, no es pot realitzar el canvi.";
    //        return false;

    //    }

    //    public bool ActualitzarEstatSinistre(int idSinistre, string estatSinistre) {

    //        string comanda = "";
    //        DateTime ara = DateTime.Now;

    //        try
    //        {
    //            EncodingProvider provider = CodePagesEncodingProvider.Instance;
    //            Encoding.RegisterProvider(provider);


    //            using (Connection = new MySqlConnection(ConnectionString))
    //            {

    //                Connection.Open();
    //                MySqlCommand command = Connection.CreateCommand();

    //                if (estatSinistre.Equals("TANCAT"))
    //                {
    //                    comanda = "update sinistre set estat_sinistre = @estatSinistre, data_tancament = @ara where numero = @idSinistre";
    //                    command.Parameters.Add(new MySqlParameter("ara", ara));
    //                }
    //                else if (estatSinistre.Equals("ASSIGNAT")) {
    //                    comanda = "update sinistre set estat_sinistre = @estatSinistre, data_assignacio = @ara where numero = @idSinistre";
    //                    command.Parameters.Add(new MySqlParameter("ara", ara));
    //                }

    //                command.CommandText = comanda;
    //                command.Parameters.Add(new MySqlParameter("estatSinistre", estatSinistre));
    //                command.Parameters.Add(new MySqlParameter("idSinistre", idSinistre));

    //                command.ExecuteNonQuery();
    //                Connection.Close();
    //                return true;
    //            }

    //        }
    //        catch (MySqlException e)
    //        {
    //            Debug.WriteLine("ERROR BD actualitzarEstatSinistre:  " + e.Message);
    //        }
    //        return false;

    //    }







}





















































