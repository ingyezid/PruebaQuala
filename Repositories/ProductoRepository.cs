using Dapper;
using PruebaQuala.Models;
using System.Data;
using System.Data.SqlClient;

namespace PruebaQuala.Repositories
{
    public class ProductoRepository : IProductoRepository
    {

        /// <summary>
        /// variable de conexión a la base de datos
        /// </summary>
        private readonly IDbConnection _bd;

        /// <summary>
        /// Constructor de la clase ProductoRepository
        /// </summary>
        /// <param name="configuration"></param>
        public ProductoRepository( IConfiguration configuration )
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        /// <summary>
        /// Mostrar un producto por su Id
        /// </summary>
        /// <param name="id">Codigo Producto</param>
        /// <returns> Producto </returns>
        public Producto GetById(int id) {

            var sqlOne = " SELECT * FROM Producto WHERE CodigoProducto = @CodigoProducto ";

            var producto = _bd.Query<Producto>(sqlOne, new { @CodigoProducto = id } ).Single();

            return producto;
        }

        /// <summary>
        /// Mostrar todos los productos
        /// </summary>
        /// <returns> List<Producto> </returns>
        public List<Producto> GetAll()
        {
            var sqlList = " SELECT * FROM Producto";

            var listProducto = _bd.Query<Producto>(sqlList).ToList();

            return listProducto;
        }

        /// <summary>
        /// Crear un nuevo producto
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns> Producto </returns>
        public Producto Create(Producto producto)
        {

            // opcion 1
           //var sqlCreate = " INSERT INTO Producto (Nombre, Descripcion, ReferenciaInterna, PrecioUnitario, Estado, UnidadMedida, FechaCreacion) " +
           //           " VALUES (@Nombre, @Descripcion, @ReferenciaInterna, @PrecioUnitario, @Estado, @UnidadMedida, @FechaCreacion); " +
           //           " SELECT CAST(SCOPE_IDENTITY() as int) ";
           //var id = _bd.Query<int>(sqlCreate, new {
           //     producto.Nombre,
           //     producto.Descripcion,
           //     producto.ReferenciaInterna,
           //     producto.PrecioUnitario,
           //     producto.Estado,
           //     producto.UnidadMedida,
           //     producto.FechaCreacion
           //}).Single();

           // producto.CodigoProducto = id;
           // return producto;


            // opcion 2: otra forma de hacerlo

            var sqlCreate = " INSERT INTO Producto (Nombre, Descripcion, ReferenciaInterna, PrecioUnitario, Estado, UnidadMedida, FechaCreacion) " +
                      " VALUES (@Nombre, @Descripcion, @ReferenciaInterna, @PrecioUnitario, @Estado, @UnidadMedida, @FechaCreacion); " +
                      " SELECT CAST(SCOPE_IDENTITY() as int) ";
            var id = _bd.Query<int>(sqlCreate, producto).Single();

            producto.CodigoProducto = id;
            return producto;

        }

        /// <summary>
        /// Actualizar un producto
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns> Producto </returns>
        public Producto Update(Producto producto)
        {
            var sqlUpdate = " UPDATE Producto SET " +
                            " Nombre = @Nombre, " +
                            " Descripcion = @Descripcion, " +
                            " ReferenciaInterna = @ReferenciaInterna, " +
                            " PrecioUnitario = @PrecioUnitario, " +
                            " Estado = @Estado, " +
                            " UnidadMedida = @UnidadMedida, " +
                            " FechaCreacion = @FechaCreacion " +
                            " WHERE CodigoProducto = @CodigoProducto ";

            _bd.Execute(sqlUpdate, producto);

            return producto;

        }

        /// <summary>
        /// Eliminar un producto por su Id
        /// </summary>
        /// <param name="id"> Codigo Producto </param>
        public void Delete(int id)
        {
            var sqlDelete = " DELETE FROM Producto WHERE CodigoProducto = @CodigoProducto ";

            _bd.Execute(sqlDelete, new { @CodigoProducto = id });
        }

    }
}
