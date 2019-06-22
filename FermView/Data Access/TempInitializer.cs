using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using FermView.Models;

namespace FermView.Data_Access
{
    public class TempInitializer : DropCreateDatabaseIfModelChanges<DbContext>
    {
        
    }
}
