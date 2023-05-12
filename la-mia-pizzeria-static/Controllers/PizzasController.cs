using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        public IActionResult AllPizzas(string? name)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                List<Pizza> pizzas = new List<Pizza>();
                if(name == null)
                 pizzas = ctx.Pizzas.ToList();
                else 
                 pizzas = ctx.Pizzas.Where(p => p.Name == name).ToList();
                return Ok(pizzas);

                
                
            }
        }
        [HttpPut("{id}")]
        public IActionResult PizzaId(int id)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                var pizza = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                return Ok(pizza);
            }
        }
    }
}
