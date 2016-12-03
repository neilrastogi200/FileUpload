using System;
using System.Diagnostics.CodeAnalysis;
using ExpressMapper;
using FileUpload.BLL.Models;

namespace FileUpload.BLL.ExpressMapper
{
    [ExcludeFromCodeCoverage]
    public class ExpressMapperAutoConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Register<Transaction,Repository.Models.Transaction>().Member(dest => dest.Amount,src => Convert.ToDecimal(src.Amount));
        }
    }
}
