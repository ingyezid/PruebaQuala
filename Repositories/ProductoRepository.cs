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
        public ProductoRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }


        #region Consulta Directa SQL

        /// <summary>
        /// Mostrar un producto por su Id
        /// </summary>
        /// <param name="id">Codigo Producto</param>
        /// <returns> Producto </returns>
        public Producto GetById(int id)
        {

            var sqlOne = " SELECT * FROM ygm_Producto WHERE CodigoProducto = @CodigoProducto ";

            var producto = _bd.Query<Producto>(sqlOne, new { @CodigoProducto = id }).Single();

            return producto;
        }

        /// <summary>
        /// Mostrar todos los productos
        /// </summary>
        /// <returns> List<Producto> </returns>
        public List<Producto> GetAll()
        {
            var sqlList = " SELECT * FROM ygm_Producto";

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
            //var sqlCreate = " INSERT INTO ygm_Producto (Nombre, Descripcion, ReferenciaInterna, PrecioUnitario, Estado, UnidadMedida, FechaCreacion) " +
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

            var sqlCreate = " INSERT INTO ygm_Producto (Nombre, Descripcion, ReferenciaInterna, PrecioUnitario, Estado, UnidadMedida, FechaCreacion) " +
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
            var sqlUpdate = " UPDATE ygm_Producto SET " +
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
            var sqlDelete = " DELETE FROM ygm_Producto WHERE CodigoProducto = @CodigoProducto ";

            _bd.Execute(sqlDelete, new { @CodigoProducto = id });
        }

        #endregion

        #region Procedimientos Almacenados

        /// <summary>
        /// Mostrar un producto por su Id - Procedimiento Almacenado
        /// </summary>
        /// <param name="id">Codigo Producto</param>
        /// <returns> Producto </returns>
        public Producto SP_GetById(int id)
        {

            var SPOne = "ygm_sp_Listar_Producto_X_Codigo";

            var producto = _bd.Query<Producto>(SPOne, new { @CodigoProducto = id }, commandType: CommandType.StoredProcedure).Single();

            return producto;
        }

        /// <summary>
        /// Mostrar todos los productos - Procedimiento Almacenado
        /// </summary>
        /// <returns> List<Producto> </returns>
        public List<Producto> SP_GetAll()
        {
            var SPList = "ygm_sp_Listar_Producto";

            var listProducto = _bd.Query<Producto>(SPList, commandType: CommandType.StoredProcedure).ToList();


            return listProducto;
        }

        /// <summary>
        /// Crear un nuevo producto - Procedimiento Almacenado
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns> Producto </returns>
        public Producto SP_Create(Producto producto)
        {

            var SPCreate = "ygm_sp_Crear_Producto";

            var id = _bd.Query<int>(SPCreate,
                new
                {
                    @Nombre = producto.Nombre,
                    @Descripcion = producto.Descripcion,
                    @ReferenciaInterna = producto.ReferenciaInterna,
                    @PrecioUnitario = producto.PrecioUnitario,
                    @Estado = producto.Estado,
                    @UnidadMedida = producto.UnidadMedida,
                    @FechaCreacion = producto.FechaCreacion
                },
                commandType: CommandType.StoredProcedure).Single();

            producto.CodigoProducto = id;
            return producto;

        }

        /// <summary>
        /// Actualizar un producto - Procedimiento Almacenado
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns> Producto </returns>
        public Producto SP_Update(Producto producto)
        {
            var SPUpdate = "ygm_sp_Actualizar_Producto";

            _bd.Execute(SPUpdate,
                new
                {
                    @CodigoProducto = producto.CodigoProducto,
                    @Nombre = producto.Nombre,
                    @Descripcion = producto.Descripcion,
                    @ReferenciaInterna = producto.ReferenciaInterna,
                    @PrecioUnitario = producto.PrecioUnitario,
                    @Estado = producto.Estado,
                    @UnidadMedida = producto.UnidadMedida,
                    @FechaCreacion = producto.FechaCreacion
                },
                commandType: CommandType.StoredProcedure);

            return producto;

        }

        /// <summary> 
        /// Eliminar un producto por su Id - Procedimiento Almacenado
        /// </summary>
        /// <param name="id"> Codigo Producto </param>
        public void SP_Delete(int id)
        {
            var SPDelete = "ygm_sp_Eliminar_Producto_X_Codigo";

            _bd.Execute(SPDelete, new { @CodigoProducto = id }, commandType: CommandType.StoredProcedure);
        }

        #endregion
    }
}
