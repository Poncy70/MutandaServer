using System.Linq;
using OrderEntry.Net.Models;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server;
using System.Web.Http.Controllers;
using System.Web.Http;
using System.Web.Http.OData;
using System.Collections.Generic;

namespace OrderEntry.Net.Service
{
    public class PermissionController : BaseController<Permission>
    {
        protected OrderEntryNetContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Task initializeBase = Task.Run(async () => { await InitializeBaseAsync(controllerContext); });
            initializeBase.Wait();

            if (initializeBase.IsCompleted)
            {
                context = new OrderEntryNetContext(MakeConnectionString());
                DomainManager = new EntityDomainManager<Permission>(context, Request, enableSoftDelete: true);
            }
        }

        protected override IQueryable<Permission> Query()
        {
            IQueryable<Permission> permissionQuery = null;
            IEnumerable<Permission> permission = (from perm in context.Permission
                                                  where perm.deviceMail == mConnectionInfo.DeviceMail
                                                  select perm);

            permissionQuery = permission.AsQueryable();
            return permissionQuery;
        }

        public IQueryable<Permission> GetAllPermission()
        {
            IQueryable<Permission> i = null;

            try
            {
                Permission firstElement;
                i = Query();

                if (i.Count() > 0)
                    firstElement = i.First();

                return i;
            }
            catch (System.Exception e)
            {
                ControllerStatic.WriteErrorLog(mConnectionInfo, "PermissionController", e, i.ToString());
            }

            return null;
        }

        public SingleResult<Permission> GetPermission(string id)
        {
            return Lookup(id);
        }

        public Task<Permission> PatchPermission(string id, Delta<Permission> patch)
        {
            return UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostPermission(Permission item)
        {
            Permission current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task DeletePermission(string id)
        {
            return DeleteAsync(id);
        }
    }

}