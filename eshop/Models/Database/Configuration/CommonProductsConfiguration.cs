using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database.Configuration
{
    public class CommonProductsConfiguration : EntityConfiguration, IEntityTypeConfiguration<CommonProduct>
    {
        public void Configure(EntityTypeBuilder<CommonProduct> builder)
        {
            base.Configure(builder);       


        }
    }
    
}
