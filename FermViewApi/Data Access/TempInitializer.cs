using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using FermViewApi.Models;

namespace FermViewApi.Data_Access
{
    public class TempInitializer : DropCreateDatabaseIfModelChanges<DbContext>
    {
        
    }
}
