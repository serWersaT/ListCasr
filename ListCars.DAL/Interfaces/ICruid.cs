using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace ListCars.DAL.Interfaces
{
    public interface Icruid<M>
    {
        int Count();
        bool Update(M model);
        bool Delete(int id);
        int Insert(M model);
        DataTable SelectReports(string where);
    }
}
