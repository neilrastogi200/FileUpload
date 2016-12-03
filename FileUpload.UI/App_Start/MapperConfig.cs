using System.Diagnostics.CodeAnalysis;
using ExpressMapper;
using FileUpload.BLL.ExpressMapper;

namespace FileUpload.UI
{
    [ExcludeFromCodeCoverage]
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            ExpressMapperAutoConfig.RegisterMappings();
            Mapper.Compile();
        }
    }
}