using System;
using System.Collections.Generic;
using System.Text;
using ListCars.DAL.Models;

namespace ListCars.DAL.DB
{
    public class ListReports
    {
        public string GetReports(NavigateModel model)
        {
            var result = "";
            switch (model.RepId)
            {
                case 0:
                    result = GetSelect(model);
                    break;

                case 1:
                    result = GetStatReport(model);
                    break;

                case 2:
                    result = GetCarReport(model);
                    //result = GetTest(model);
                    break;
            }
            return result;
        }

        private string GetTest(NavigateModel model) => $@"SELECT * FROM CarsTable ";


        private string GetSelect(NavigateModel model) => $@"SELECT 
                                        Id,
                                        (SELECT count(*) FROM CarsTable b  WHERE a.Id >= b.Id) AS NumberRow,
                                         Number,
                                         Model,
                                         Color,
                                         Year,
                                         Active
                                         FROM CarsTable a   
                                            where NumberRow >= " + model.topmin + " and NumberRow <= " + model.topmax;


        private string GetStatReport(NavigateModel model) => $@"SELECT 
                                        count(*),
                                        min(Created),
                                        max(Created)
                                         FROM CarsTable ";


        private string GetCarReport(NavigateModel model) => $@"SELECT 
                                        Model,
                                        Created,
                                        LastFetched,
                                        Number,
                                        Color,
                                        iif(Active = 1, 'В работе', 'На ремонте')
                                         FROM CarsTable 
                                        where Model like '%" + model.Model + "%'";
    }
}

