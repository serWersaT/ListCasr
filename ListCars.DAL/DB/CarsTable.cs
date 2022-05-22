using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ListCars.DAL.Interfaces;
using ListCars.DAL.Models;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ListCars.DAL.DB
{
    public class CarsTable<T>: Icruid<CarModel>
    {
        string baseName = Directory.GetCurrentDirectory() + "/CarsTable.db3";

        public CarsTable()
        {
#if DEBUG
            baseName = @"D:\С# примеры\СписокАвто\ListCars\ListCars.DAL\DBSQL\CarsTable.db3";
#endif

            if (!File.Exists(baseName))
            {

                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [CarsTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                [Created] char(100) NOT NULL,
                                [LastFetched] char(100) NOT NULL,
                                [Number] char(100) NOT NULL,
                                [Model] char(100) NOT NULL,
                                [Color] char(100) NOT NULL,
                                [Year] char(100) NOT NULL,
                                [Active] integer
                              );";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        public DataTable SelectReports(string where)
        {
            
            List<T> lst = new List<T>();

            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
            {
                connection.Open();
                DataTable table = new DataTable();
                using (SQLiteCommand command = new SQLiteCommand(where))
                {
                    command.Connection = connection;
                    SQLiteDataReader reader = command.ExecuteReader();
                    table.Load(reader);
                    return table;
                }
            }
        }

        public int Count()
        {
            int count = 0;
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM CarsTable"))
                {
                    cmd.Connection = connection;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    count = (reader.Read()) ? reader.GetInt32(0) : 0;
                    reader.Close();
                }
                return count;
            }
        }
        public bool Update(CarModel model)
        {
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand($@"UPDATE CarsTable 
                                                                SET 
                                                                LastFetched = '{DateTime.Now.ToString()}',
                                                                Number = '{model.NumberCar}',
                                                                Model = '{model.ModelCar}',
                                                                Color = '{model.ColorCar}',
                                                                Year = '{model.YearCar}',
                                                                Active = '{model.Active}'
                                                            WHERE Id = {model.Id}"))
                {
                    try
                    {
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

        }

        public bool Delete(int id)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM CarsTable WHERE Id = {id}"))
                    {
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public int Insert(CarModel model)
        {
            bool SelectTest = false;

            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"SELECT * FROM CarsTable WHERE Number = '{model.NumberCar}'";
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.StepCount == 0) SelectTest = true;
                }


                if (SelectTest == true)
                {
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand())
                        {
                            command.CommandText = $@"INSERT INTO CarsTable(Created, LastFetched, Number, Model, Color, Year, Active)
                                        VALUES ('{DateTime.Now.ToString()}', '{DateTime.Now.ToString()}', '{model.NumberCar}', '{model.ModelCar}', '{model.ColorCar}', '{model.YearCar}', 1)";
                            command.CommandType = System.Data.CommandType.Text;
                            command.Connection = connection;
                            command.ExecuteNonQuery();
                        }
                        return 1;

                    }
                    catch (Exception ex)
                    { return 2; }
                }
                else return 0;

            }
        }


        private List<CarModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<CarModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new CarModel();
                    wd.Id = reader.GetInt32(0);
                    wd.NumberRow = reader.GetInt32(1);
                    wd.NumberCar = reader.GetString(2);
                    wd.ModelCar = reader.GetString(3);
                    wd.ColorCar = reader.GetString(4);
                    wd.YearCar = reader.GetString(5);
                    wd.Active = reader.GetInt32(6);
                    ListWord.Add(wd);
                }
                reader.Close();
            }
            return ListWord;
        }
    }
}
