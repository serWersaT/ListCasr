using System;
using System.Collections.Generic;
using System.Text;
using ListCars.DAL.Models;
using ListCars.DAL.DB;
using System.Data;
using System.Data.SQLite;

namespace ListCars.BLL.Service
{
    public class Service: DefaultSevice
    {
        public int InsertCar(CarModel model)
        {
            return FuncInsert<CarModel>(tablecar, model);
        }

        public int CountCar(CarModel model)
        {
            return FuncCount<CarModel>(tablecar);
        }

        public bool UpdateCar(CarModel model)
        {
            return FuncUpdate<CarModel>(tablecar, model);
        }

        public List<CarModel> SelectCar(NavigateModel model)
        {
            return GetAllCar(FuncSelectReports<CarModel>(tablecar, reports.GetReports(model)));
        }

        public bool DeleteCar(int id)
        {
            return FuncDelete<CarModel>(tablecar, id);
        }

        public List<RepGeneralStatModel> SelectGeneralReports(NavigateModel model)
        {
            return RepGeneralStat(FuncSelectReports<CarModel>(tablecar, reports.GetReports(model)));
        }

        public List<RepCarModel> SelectCarReports(NavigateModel model)
        {
            return GetCarRep(FuncSelectReports<CarModel>(tablecar, reports.GetReports(model)));
        }



        private List<RepCarModel> GetCarRep(DataTable reader)
        {
            var lst = new List<RepCarModel>();

            foreach (DataRow row in reader.Rows)
            {
                var cells = row.ItemArray;
                RepCarModel stat = new RepCarModel();
                stat.Model = (string)row[0];
                stat.Created = (string)row[1];
                stat.LastFetched = (string)row[2];
                stat.Number = (string)row[3];
                stat.Color = (string)row[4];
                stat.InWork = (string)row[5];

                lst.Add(stat);
            }

            return lst;
        }

        private List<CarModel> GetAllCar(DataTable reader)
        {
            var lst = new List<CarModel>();

            foreach (DataRow row in reader.Rows)
            {
                var cells = row.ItemArray;
                CarModel stat = new CarModel();
                stat.Id = (System.Int64)row[0];
                stat.NumberRow = (System.Int64)row[1];
                stat.NumberCar = (string)row[2];
                stat.ModelCar = (string)row[3];
                stat.ColorCar = (string)row[4];
                stat.YearCar = (string)row[5];
                stat.Active = (System.Int64)row[6];

                lst.Add(stat);
            }

            return lst;
        }


        private List<RepGeneralStatModel> RepGeneralStat(DataTable reader)
        {
            var lst = new List<RepGeneralStatModel>();

            foreach (DataRow row in reader.Rows)
            {
                var cells = row.ItemArray;
                RepGeneralStatModel stat = new RepGeneralStatModel();
                stat.Cnt = (System.Int64)row[0];
                stat.EndDate = (string)row[1];
                stat.StartDate = (string)row[2];

                lst.Add(stat);
            }

            return lst;
        }
    }
}
