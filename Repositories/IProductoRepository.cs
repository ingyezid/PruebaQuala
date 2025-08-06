using PruebaQuala.Models;

namespace PruebaQuala.Repositories
{
    public interface IProductoRepository
    {
        Producto GetById(int id);
        List<Producto> GetAll();
        Producto Add(Producto producto);
        void Update(Producto producto);
        void Delete(int id);
    }
}
