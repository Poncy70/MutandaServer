using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using OrderEntry.Net.Models;

namespace OrderEntry.Net.Service
{
    public class OrderEntryNetContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public OrderEntryNetContext() : base(connectionStringName)
        {
        }

        public OrderEntryNetContext(string connectionString) : base(connectionString)
        {
        }

        #region DB DataSet
        public DbSet<GEST_Clienti_Anagrafica_Indirizzi> GEST_Anagrafica_Indirizzi { get; set; }
        public DbSet<GEST_Articoli_Anagrafica> GEST_Articoli_Anagrafica { get; set; }
        public DbSet<GEST_Articoli_BarCode> GEST_Articoli_BarCode { get; set; }
        public DbSet<GEST_Articoli_Famiglie> GEST_Articoli_Famiglie { get; set; }
        public DbSet<GEST_Articoli_Immagini> GEST_Articoli_Immagini { get; set; }
        public DbSet<GEST_Articoli_Listini> GEST_Articoli_Listini { get; set; }
        public DbSet<GEST_Articoli_Nature> GEST_Articoli_Nature { get; set; }
        public DbSet<GEST_CategorieClienti> GEST_CategorieClienti { get; set; }
        public DbSet<GEST_Condizione_Pagamento> GEST_Condizione_Pagamento { get; set; }
        public DbSet<GEST_Documenti_Righe> GEST_Documenti_Righe { get; set; }
        public DbSet<GEST_Documenti_Teste> GEST_Documenti_Teste { get; set; }
        public DbSet<GEST_Ordini_Righe> GEST_Ordini_Righe { get; set; }
        public DbSet<GEST_Ordini_Teste> GEST_Ordini_Teste { get; set; }
        public DbSet<GEST_Iva> GEST_Iva { get; set; }
        public DbSet<GEST_Listini> GEST_Listini { get; set; }
        public DbSet<DEVICE_ParametriDevice> GEST_ParametriDevice { get; set; }
        public DbSet<GEST_Scala_Sconti> GEST_Scala_Sconti { get; set; }
        public DbSet<GEST_UnitaMisura> GEST_UnitaMisura { get; set; }
        public DbSet<GEST_Update_Immagini> GEST_Update_Immagini { get; set; }
        public DbSet<GEST_Articoli_Disponibilita> GEST_Articoli_Disponibilita { get; set; }        
        public DbSet<GEST_Articoli_Classi> GEST_Articoli_Classi { get; set; }
        public DbSet<GEST_Clienti_Anagrafica> GEST_Clienti_Anagrafica { get; set; }
        public DbSet<GEST_Porto> GEST_Porto { get; set; }
        public DbSet<Permission> Permission { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

    }

}
