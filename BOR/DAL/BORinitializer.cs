using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BOR.Models;

namespace BOR.DAL
{
    public class BORinitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BORcontext>
    {
    }
}