using PruebaQuala.Models;
using System.Data;
using System.Data.SqlClient;

namespace PruebaQuala.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _bd;

        public ProductoRepository( IConfiguration configuration )
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Producto GetById(int id) {

            throw new NotImplementedException();
        }
        public List<Producto> GetAll()
        {
            throw new NotImplementedException();
        }
        public Producto Add(Producto producto)
        {
            throw new NotImplementedException();
        }
        public void Update(Producto producto)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
