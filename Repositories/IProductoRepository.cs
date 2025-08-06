using PruebaQuala.Models;

namespace PruebaQuala.Repositories
{
    public interface IProductoRepository
    {
        /// <summary>
        /// Mostrar un producto por su Id
        /// </summary>
        /// <param name="id">Codigo Producto</param>
        /// <returns> Producto </returns>
        Producto GetById(int id);

        /// <summary>
        /// Mostrar todos los productos
        /// </summary>
        /// <returns> List<Producto> </returns>
        List<Producto> GetAll();

        /// <summary>
        /// Crear un nuevo producto
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns> Producto </returns>
        Producto Create(Producto producto);

        /// <summary>
        /// Actualizar un producto
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns> Producto </returns>
        Producto Update(Producto producto);

        /// <summary>
        /// Eliminar un producto por su Id
        /// </summary>
        /// <param name="id"> Codigo Producto </param>
        void Delete(int id);
    }
}
