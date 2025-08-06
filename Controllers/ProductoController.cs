using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PruebaQuala.Models;
using PruebaQuala.Repositories;

namespace PruebaQuala.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoRepository _productoRepository;

        /// <summary>
        /// Constructor de ProductoController
        /// </summary>
        /// <param name="productoRepository"></param>
        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View(_productoRepository.GetAll());
        }

        /// <summary>
        /// Create - Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create - Post
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Procesa la creación               
                _productoRepository.Create(producto);

                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }


        /// <summary>
        /// Update - Get
        /// </summary>
        /// <param name="id"> Codigo Producto</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int? id)
        {
            var producto = _productoRepository.GetById(id.GetValueOrDefault());

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        /// <summary>
        /// Update - Post
        /// </summary>
        /// <param name="producto"> Producto </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Producto producto)
        {
            if (id != producto.CodigoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Procesa la actualización               
                _productoRepository.Update(producto);

                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }



        /// <summary>
        /// Delete - Get
        /// </summary>
        /// <param name="id"> Codigo Producto</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            _productoRepository.Delete(id.GetValueOrDefault());
            
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
