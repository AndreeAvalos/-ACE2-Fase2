using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicionController : ControllerBase
    {
        private String cadenaconexion = "Server=tcp:ws1.database.windows.net,1433;Initial Catalog=BD_P1;Persist Security Info=False;User ID=ANDREE;Password=1234kINGAVALOS;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Medicion
        [HttpGet]
        public string Get()
        {
            SqlDataReader reader = null;
            SqlConnection conexion = new SqlConnection
            {
                ConnectionString = cadenaconexion
            };

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "SELECT * FROM Medicion",
                Connection = conexion
            };
            conexion.Open();

            reader = cmd.ExecuteReader();
            List<Medicion> lstMediciones = new List<Medicion>();
            string json;
            try
            {
                while (reader.Read())
                {
                    Medicion nuevo = new Medicion(
                         Convert.ToInt32(reader.GetValue(0)),
                         Convert.ToInt32(reader.GetValue(1)),
                         Convert.ToInt32(reader.GetValue(2)),
                         Convert.ToInt32(reader.GetValue(7)),
                         reader.GetValue(3).ToString(),
                         reader.GetValue(4).ToString(),
                         reader.GetValue(5).ToString(),
                         reader.GetValue(6).ToString(),
                         reader.GetValue(8).ToString()
                     );
                    lstMediciones.Add(nuevo);
                }
                json = JsonConvert.SerializeObject(lstMediciones);
            }
            catch (Exception)
            {
                json = "No existen datos";
            }

            conexion.Close();
            return json;
        }

        // GET: api/Medicion/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            SqlDataReader reader = null;
            SqlConnection conexion = new SqlConnection
            {
                ConnectionString = cadenaconexion
            };

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "SELECT * FROM Medicion where id_medicion=" + id + ";",
                Connection = conexion
            };
            conexion.Open();

            reader = cmd.ExecuteReader();
            string json = "";
           
            if (reader.Read())
            {
                Medicion nuevo = new Medicion(
                    Convert.ToInt32(reader.GetValue(0).ToString()),
                    Convert.ToInt32(reader.GetValue(1).ToString()),
                    Convert.ToInt32(reader.GetValue(2).ToString()),
                    Convert.ToInt32(reader.GetValue(7).ToString()),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetValue(6).ToString(),
                    reader.GetValue(8).ToString()
                );
                return JsonConvert.SerializeObject(nuevo);
            }
            else
            {
                json = "No existen datos";
            }

            conexion.Close();
            return json;
        }

        // POST: api/Medicion
        [HttpPost]
        public bool Post(@in entrada)
        {
            bool salida = false;
            DateTime currentTime = DateTime.Now;
            String hora = (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, TimeZoneInfo.Local.Id, "Central Standard Time")).ToString("HH:mm:ss");
            string fecha_actual = (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, TimeZoneInfo.Local.Id, "Central Standard Time")).ToString("dd-MM-yyyy");
            SqlConnection conexion = new SqlConnection
            {
                ConnectionString = cadenaconexion
            };
            conexion.Open();
           try
            {

                @in nuevo = entrada;
                SqlCommand cmd = new SqlCommand("insert_Medicion", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("tipo", nuevo.Tipo);
                cmd.Parameters.AddWithValue("carnet", nuevo.Carnet);
                cmd.Parameters.AddWithValue("fecha", fecha_actual);
                cmd.Parameters.AddWithValue("hora", hora);
                cmd.Parameters.AddWithValue("lat", nuevo.Latitud);
                cmd.Parameters.AddWithValue("long", nuevo.Longitud);
                cmd.Parameters.AddWithValue("valor", nuevo.Valor_medicion);
                cmd.Parameters.AddWithValue("unidad", nuevo.Unidad);

                //Medicion nue = new Medicion(1, nuevo.Tipo, nuevo.Carnet, nuevo.Valor_medicion, fecha_actual, hora, nuevo.Latitud, nuevo.Longitud, nuevo.Unidad);
                cmd.ExecuteNonQuery();
                salida = true;

           }
            catch (Exception)
            {
            }
            conexion.Close();


            return salida;
        }

        // PUT: api/Medicion/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
