using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using AutoMapper;
using WebGrease.Css.Extensions;

namespace ERP.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Reset();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(x => x.FullName.StartsWith("UCAS"));
            Mapper.Initialize(x => x.AddProfiles(assemblies));
        }
    }
}