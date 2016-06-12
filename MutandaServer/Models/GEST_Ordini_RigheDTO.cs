using Microsoft.Azure.Mobile.Server;
using System;

namespace OrderEntry.Net.Models
{
    public class GEST_Ordini_RigheDTO : EntityData
    {
        public GEST_Ordini_RigheDTO()
        {

        }

        public string IdSlave { get; set; }
        public int IdRiga { get; set; }
        public short TipoRiga { get; set; }

        private string mCodArt;
        public string CodArt
        {
            get
            {
                if (mCodArt != null)
                    return mCodArt.Replace("'", "''");
                else
                    return mCodArt;
            }

            set { mCodArt = value; }
        }

        private string mDescrizione;
        public string Descrizione
        {
            get
            {
                if (mDescrizione != null)
                    return mDescrizione.Replace("'", "''");
                else
                    return mDescrizione;
            }

            set { mDescrizione = value; }
        }

        public decimal Qta { get; set; }

        private string mCodUnMis;
        public string CodUnMis
        {
            get
            {
                if (mCodUnMis != null)
                    return mCodUnMis.Replace("'", "''");
                else
                    return mCodUnMis;
            }

            set { mCodUnMis = value; }
        }

        private string mCodIva;
        public string CodIva 
        {
            get
            {
                if (mCodIva != null)
                    return mCodIva.Replace("'", "''");
                else
                    return mCodIva;
            }
            set { mCodIva = value; }
        }

        public decimal ValUnit { get; set; }
        public decimal Sc1 { get; set; }
        public decimal Sc2 { get; set; }
        public decimal Sc3 { get; set; }
        public decimal Sc4 { get; set; }
        public decimal ImportoSconto { get; set; }
        public DateTime? DataPresuntaConsegna { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Imposta { get; set; }
        public decimal Totale { get; set; }
        public decimal NCP_QtaScontoMerce { get; set; }
        public short CloudState { get; set; }
    }
}
