using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using OrderEntry.Net.Models;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server.Config;

namespace OrderEntry.Net.Service
{
    [MobileAppController]
    public class DEVICE_ParametriDeviceController : BaseController<DEVICE_ParametriDevice>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<DEVICE_ParametriDevice>(context, Request);
            }
        }

        protected override IQueryable<DEVICE_ParametriDevice> Query()
        {
            IQueryable<DEVICE_ParametriDevice> deviceQuery = null;
            IEnumerable<DEVICE_ParametriDevice> device = (from parametri in context.GEST_ParametriDevice
                                                          where parametri.DeviceMail == mConnectionInfo.DeviceMail
                                                          select parametri);


            deviceQuery = device.AsQueryable();
            return deviceQuery;
        }

        public IQueryable<DEVICE_ParametriDevice> GetAllGEST_ParametriDevice()
        {
            IQueryable<DEVICE_ParametriDevice> i = null;

            try
            {
                DEVICE_ParametriDevice firstElement;
                i = Query();
                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "GEST_ParametriDeviceController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<DEVICE_ParametriDevice> GetGEST_ParametriDevice(string id)
        {
            return Lookup(id);
        }

        public Task<DEVICE_ParametriDevice> PatchGEST_ParametriDevice(string id, Delta<DEVICE_ParametriDevice> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostGEST_ParametriDevice(DEVICE_ParametriDevice item)
        {
            DEVICE_ParametriDevice current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeleteGEST_ParametriDevice(string id)
        {
            return DeleteAsync(id);
        }
    }
}