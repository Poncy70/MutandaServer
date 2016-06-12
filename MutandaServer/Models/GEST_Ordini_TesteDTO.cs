using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderEntry.Net.Models
{
    public class GEST_Ordini_TesteDTO : EntityData
    {
        public GEST_Ordini_TesteDTO()
        {
            this.RigheOrdine = new List<GEST_Ordini_RigheDTO>();
        }

        [NotMapped]
        public virtual ICollection<GEST_Ordini_RigheDTO> RigheOrdine { get; set; }

        private string mRagioneSociale;
        public string RagioneSociale
        {
            get
            {
                if (mRagioneSociale != null)
                    return mRagioneSociale.Replace("'", "''");
                else
                    return mRagioneSociale;
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
                    return mNumeroOrdineDevice.Replace("'", "''");
                else
                    return mNumeroOrdineDevice;
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
                    return mRagSocSped.Replace("'", "''");
                else
                    return mRagSocSped;
            }

            set { mRagSocSped = value; }
        }

        private string mIndirizzoSped;
        public string IndirizzoSped
        {
            get
            {
                if (mIndirizzoSped != null)
                    return mIndirizzoSped.Replace("'", "''");
                else
                    return mIndirizzoSped;
            }
            set { mIndirizzoSped = value; }
        }

        private string mCittaSped;
        public string CittaSped
        {
            get
            {
                if (mCittaSped != null)
                    return mCittaSped.Replace("'", "''");
                else
                    return mCittaSped;
            }
            set { mCittaSped = value; }
        }

        private string mCapSped;
        public string CapSped
        {
            get
            {
                if (mCapSped != null)
                    return mCapSped.Replace("'", "''");
                else
                    return mCapSped;
            }
            set { mCapSped = value; }
        }

        private string mProvSped;
        public string ProvSped
        {
            get
            {
                if (mProvSped != null)
                    return mProvSped.Replace("'", "''");
                else
                    return mProvSped;
            }
            set { mProvSped = value; }
        }

        private string mNazioneSped;
        public string NazioneSped
        {
            get
            {
                if (mNazioneSped != null)
                    return mNazioneSped.Replace("'", "''");
                else
                    return mNazioneSped;
            }
            set { mNazioneSped = value; }
        }

        private string mDeviceMail;
        public string DeviceMail
        {
            get
            {
                if (mDeviceMail != null)
                    return mDeviceMail.Replace("'", "''");
                else
                    return mDeviceMail;
            }
            set { mDeviceMail = value; }
        }

        public string IdDevice { get; set; }

        public int NrRigheTot { get; set; }
    }
}
