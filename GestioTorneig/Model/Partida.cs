 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioTorneig.Model
{
    public class Partida
    {
        #region Props  



        public int Id { get; set; }

        public int CarambolesB { get; set; }

        public int CarambolesA { get; set; }


        public DateTime? DataPartida { get; set; }

       
        public int NumEntrades { get; set; }





        public Inscrit InscritA{ get; set; }

        public Inscrit InscritB { get; set; }

        public Enums.MOTIU_VICTORIA MotiuVictoria { get; set; }

        public Enums.GUANYADOR ganador { get; set; }



        public Enums.ESTAT_PARTIDA EstatPartida { get; private set; }




        public Partida(int id, int carambolesA, int carambolesB ,DateTime? dataPartida, Inscrit inscritA,Inscrit inscritB,
             int numEntrades,Enums.ESTAT_PARTIDA estatPartida )
        {
           

 




            Id= id;
            CarambolesA = carambolesA;
            CarambolesB=carambolesB;
            DataPartida = dataPartida;
            InscritA = inscritA;
            InscritB = inscritB;
            NumEntrades=numEntrades;
            EstatPartida = estatPartida;






        }

        //public Torneo Polissa
        //{
        //    get { return mPolissa; }
        //    set
        //    {
        //        mPolissa = value;
        //        if (!value.Sinistres.Contains(this))
        //        {
        //            value.addSinistre(this);
        //        }
        //    }
                   
        //}

       
     

        #endregion Props




    }
}
