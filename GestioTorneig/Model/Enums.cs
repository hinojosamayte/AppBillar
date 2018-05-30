using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioTorneig.Model
{
    public class Enums
    {

        public enum MOTIU_VICTORIA
        {
            PER_CARAMBOLES,
            ENTRADES_ASSOLIDES,
            ABANDONAMENT
           
        }

        public static MOTIU_VICTORIA getMotiuVictoriaFromString(String estatSinistre)
        {
            switch (estatSinistre)
            {
                case "PER_CARAMBOLES":
                    return MOTIU_VICTORIA.PER_CARAMBOLES;
                case "ENTRADES_ASSOLIDES":
                    return MOTIU_VICTORIA.ENTRADES_ASSOLIDES;
                case "ABANDONAMENT":
                    return MOTIU_VICTORIA.ABANDONAMENT;
                default: return MOTIU_VICTORIA.PER_CARAMBOLES
                        ;
            }
        }


        public enum GUANYADOR
        {
           A,
           B
        }

        

        public enum ESTAT_PARTIDA
        {
            PENDENT,
            JUGADA
        }

        public static ESTAT_PARTIDA getEstatPartidaFromString(String estatPartida)
        {
            switch (estatPartida)
            {
                case "PENDENT":
                    return ESTAT_PARTIDA.PENDENT;
                case "JUGADA":
                    return ESTAT_PARTIDA.JUGADA;                 
                default: return ESTAT_PARTIDA.PENDENT;

            }
        }

        //public enum RESULTAT_PERITATGE
        //{
        //    COBERT_TOTAL,
        //    COBERT_PARCIAL,
        //    SENSE_COBERTURA,
        //    REPARAT
        //}

        //public static RESULTAT_PERITATGE getResultatPeritatgeFromString(String resultatPeritatge)
        //{
        //    switch (resultatPeritatge)
        //    {
        //        case "COBERT_TOTAL":
        //            return RESULTAT_PERITATGE.COBERT_TOTAL;
        //        case "COBERT_PARCIAL":
        //            return RESULTAT_PERITATGE.COBERT_PARCIAL;
        //        case "SENSE_COBERTURA":
        //            return RESULTAT_PERITATGE.SENSE_COBERTURA;
        //        case "REPARAT":
        //            return RESULTAT_PERITATGE.REPARAT;
        //        default: return RESULTAT_PERITATGE.COBERT_TOTAL;

        //    }
        //}


        //public enum TIPUS_HABITATGE
        //{
        //    TRASTER,
        //    PIS,
        //    LOCAL,
        //    CASA_ADOSADA,
        //    CASA_INDIV,
        //    PARKING
        //}

        //public static TIPUS_HABITATGE getTipusHabitatgeFromString(String tipusHabitatge)
        //{
        //    switch (tipusHabitatge)
        //    {
        //        case "TRASTER": return TIPUS_HABITATGE.TRASTER;
        //        case "PIS": return TIPUS_HABITATGE.PIS;
        //        case "LOCAL": return TIPUS_HABITATGE.LOCAL;
        //        case "CASA_ADOSADA": return TIPUS_HABITATGE.CASA_ADOSADA;
        //        case "CASA_INDIV": return TIPUS_HABITATGE.CASA_INDIV;
        //        case "PARKING": return TIPUS_HABITATGE.PARKING;
        //        default: return TIPUS_HABITATGE.TRASTER;
        //    }
        //}

        //public enum BOTO {

        //    CANCELAR,
        //    DESAR

        //}


    }
    }
