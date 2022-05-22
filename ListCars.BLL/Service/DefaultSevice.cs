using System;
using System.Collections.Generic;
using System.Text;
using ListCars.DAL.Models;
using ListCars.DAL.Interfaces;
using ListCars.DAL.DB;
using System.Data;
using System.Data.SQLite;

namespace ListCars.BLL.Service
{
    public class DefaultSevice
    {
        public CarModel officemodel = new CarModel();
        public CarsTable<CarModel> tablecar = new CarsTable<CarModel>();
        public ListReports reports = new ListReports();

        public int FuncCount<M>(Icruid<M> Icrud)
        {
            return Icrud.Count();
        }

        public int FuncInsert<M>(Icruid<M> Icrud, M model)
        {
            return Icrud.Insert(model);
        }

        public bool FuncUpdate<M>(Icruid<M> Icrud, M model)
        {
            return Icrud.Update(model);
        }


        public bool FuncDelete<M>(Icruid<M> Icrud, int id)
        {
            return Icrud.Delete(id);
        }

        public DataTable FuncSelectReports<M>(Icruid<M> Icrud, string report)
        {
            return Icrud.SelectReports(report);
        }

    }
}
