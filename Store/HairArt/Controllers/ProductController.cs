using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Services;


namespace HairArt.Controllers;

public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }
    public IActionResult Index() //DI Cercevesi
    {
       
        var model= _manager.ProductService.GetAllProducts(false);
        return View(model);
    }

     public IActionResult Get([FromRoute]int id)
    {
        var model=_manager.ProductService.GetOneProduct(id,false);
        return View(model);
    }

}
