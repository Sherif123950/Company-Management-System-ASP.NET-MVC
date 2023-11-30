using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configurations
{
    internal class DepartementConfigurations : IEntityTypeConfiguration<Departement>
    {
        public void Configure(EntityTypeBuilder<Departement> builder)
        {
            builder.Property(D=>D.Id).UseIdentityColumn(10,1);
            builder.Property(D => D.Name).IsRequired(true);
            builder.Property(D => D.Code).IsRequired(true);

        }
    }
}
