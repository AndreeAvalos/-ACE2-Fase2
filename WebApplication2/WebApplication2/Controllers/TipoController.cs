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
    public class TipoController : ControllerBase
    {
        private String cadenaconexion = "Server=tcp:ws1.database.windows.net,1433;Initial Catalog=BD_P1;Persist Security Info=False;User ID=ANDREE;Password=1234kINGAVALOS;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Tipo
        [HttpGet]
        public string Get()
        {
            SqlDataReader reader = null;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = cadenaconexion;

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Tipo";
            cmd.Connection = conexion;
            conexion.Open();

            reader = cmd.ExecuteReader();
            List<Tipo> lstEstudiante = new List<Tipo>();

            try
            {
                while (reader.Read())
                {
                    Tipo nuevo = new Tipo(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                    lstEstudiante.Add(nuevo);
                }
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }
            conexion.Close();
            string json = JsonConvert.SerializeObject(lstEstudiante);


            return json;
        }

        // GET: api/Tipo/5
        [HttpGet("{id}", Name = "Get3")]
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
                CommandText = "SELECT * FROM Medicion where id_tipo=" + id + ";",
                Connection = conexion
            };
            conexion.Open();

            reader = cmd.ExecuteReader();
            List<Medicion> lstMediciones = new List<Medicion>();
            string json="";
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

        // POST: api/Tipo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Tipo/5
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
