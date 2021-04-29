using System.Collections.Generic;
using System.Reflection;

namespace uBeac.Core.Web.Controllers
{
    public class EnumController : BaseController
    {
        [Get]
        public IEnumerable<EnumModel> GetAll()
        {
            return Assembly.GetEntryAssembly().GetReferencedAssemblies().GetEnums();
        }
    }
}
