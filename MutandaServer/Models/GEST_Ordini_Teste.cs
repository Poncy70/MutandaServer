using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderEntry.Net.Models
{
    public class GEST_Ordini_Teste : EntityData
    {
        public GEST_Ordini_Teste()
        {
        }

        private string mRagioneSociale;
        public string RagioneSociale
        {
            get
            {
                if (mRagioneSociale != null)
                    return mRagioneSociale;
                else
                    return string.Empty;
            }
        
            set { mRagioneSociale = value; }
        }

        public string PartitaIva { get; set; }
        public string CodiceFiscale { get; set; }
        public DateTime DataDocumento { get; set; }

        private string mNumeroOrdineDevice;
        public string NumeroOrdineDevice 
        {
            get
            {
                if (mNumeroOrdineDevice != null)
                    return mNumeroOrdineDevice;
                else
                    return string.Empty;
            }
            set { mNumeroOrdineDevice = value; }
        }

        public string CodPagamento { get; set; }
        public string CodListino { get; set; }
        public decimal TotaleDocumento { get; set; }
        public short CloudState { get; set; }
        public DateTime? DataConsegna { get; set; }
        public decimal TotaleConsegna { get; set; }
        public int IdAgente { get; set; }
        public int IdAnagrafica { get; set; }
        public string Note { get; set; }
        public int IdIndSpedMerce { get; set; }

        private string mRagSocSped;
        public string RagSocSped 
        {
            get
            {
                if (mRagSocSped != null)
                    return mRagSocSped;
                else
                    return string.Empty;
            }

            set { mRagSocSped = value; }
        }

        private string mIndirizzoSped;
        public string IndirizzoSped
        {
            get
            {
                if (mIndirizzoSped != null)
                    return mIndirizzoSped;
                else
                    return string.Empty;
            }
            set { mIndirizzoSped = value; }
        }

        private string mCittaSped;
        public string CittaSped
        {
            get
            {
                if (mCittaSped != null)
                    return mCittaSped;
                else
                    return string.Empty;
            }
            set { mCittaSped = value; }
        }

        private string mCapSped;
        public string CapSped
        {
            get
            {
                if (mCapSped != null)
                    return mCapSped;
                else
                    return string.Empty;
            }
            set { mCapSped = value; }
        }

        private string mProvSped;
        public string ProvSped
        {
            get
            {
                if (mProvSped != null)
                    return mProvSped;
                else
                    return string.Empty;
            }
            set { mProvSped = value; }
        }

        private string mNazioneSped;
        public string NazioneSped
        {
            get
            {
                if (mNazioneSped != null)
                    return mNazioneSped;
                else
                    return string.Empty;
            }
            set { mNazioneSped = value; }
        }

        private string mDeviceMail;
        public string DeviceMail
        {
            get
            {
                if (mDeviceMail != null)
                    return mDeviceMail;
                else
                    return string.Empty;
            }
            set { mDeviceMail = value; }
        }

        public string IdDevice { get; set; }

        public int NrRigheTot { get; set; }

        public int NumeroOrdineGenerale { get; set; }

        public string IdAnagraficaDevice { get; set; } //CORRISPONDE ALL'ID DEL BASE MODEL DI GEST_Clienti_Anagrafica la utilizzeremo per associare i clienti e i nuovi ordini

    }
}
