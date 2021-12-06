using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source = LAPTOP-EI7QCVUG;Initial Catalog=WCFTest;Persist Security Info=True; User ID=fa; Password=123";
        SqlConnection connection;
        SqlCommand com;

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from sbo.Pemesanan1 where ID_Reservasi = '" + IDPemesanan + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "Sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

       

        public string editPemesanan(string IDPemesanan, string NamaCustomer ,string NoTelpon)
        {
            string a = "gagal";
            try
            {
                string sql = "update into dbo.Pemesanan1 set Nama_Customer '" + NamaCustomer + "', NoTelpon = '" + NoTelpon + "'" + " where ID_Reservasi = '" + IDPemesanan + "'";

                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }


        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {

      
            
            
                string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCustomer + "','" + NoTelpon + "', " + JumlahPemesanan + ", '" + IDLokasi + "')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.lokasi set kuota = kuota - " + JumlahPemesanan + "where ID_lokasi = '" + IDLokasi + "' ";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "success";

            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }
            public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = " select ID_Reservasi, Nama_Customer, No_telpon," + " Jumlah_Pemesanan, Nama_Lokasi from dbo.Lokasi1 from dbo.Pemesanan1 p join dbo.Lokasi1 l on p.ID_Lokasi = l.ID_Lokasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);

                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "select id_lokasi, nama_lokasi, deskripsi_full, kuota from dbo.Lokasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();

                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;

        }

        public string editPemesanan(string IDPemesanann, string NamaCustomer)
        {
            throw new NotImplementedException();
        }
    }

    


}
