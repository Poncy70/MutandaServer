using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderEntry.Net.Models;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.OData;
using System.Threading.Tasks;

namespace OrderEntry.Net.Service
{
    public class OrdiniDomainManager: MappedEntityDomainManager<GEST_Ordini_Teste, GEST_Ordini_Teste>
    {
        public OrdiniDomainManager(DbContext context, HttpRequestMessage request)
            : base(context, request)
        {
        }

        public override SingleResult<GEST_Ordini_Teste> Lookup(string id)
        {
            return this.LookupEntity(p => p.Id == id);
        }
        public override Task<GEST_Ordini_Teste> UpdateAsync(string id, Delta<GEST_Ordini_Teste> patch)
        {
            return this.UpdateEntityAsync(patch, id);
        }
        public override Task<bool> DeleteAsync(string id)
        {
            return this.DeleteItemAsync(id);
        }
    }
}