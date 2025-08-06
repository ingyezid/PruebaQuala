using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PruebaQuala.Models;
using PruebaQuala.Repositories;

namespace PruebaQuala.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_productoRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//public class TuControlador : Controller
//{
//    public IActionResult Create()
//    {
//        return View();
//    }

//    [HttpPost]
//    public IActionResult Create(TuModelo modelo)
//    {
//        if (ModelState.IsValid)
//        {
//            // Procesa la creación
//            return RedirectToAction("Index");
//        }
//        return View(modelo);
//    }
//}