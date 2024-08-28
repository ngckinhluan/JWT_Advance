using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Context;

namespace DAOs
{
    public class UserDetailDao(AppDbContext context)
    {
        private AppDbContext Context => context;
    }
}
