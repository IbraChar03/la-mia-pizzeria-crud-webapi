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
        [HttpPost]
        public IActionResult CreatePizza([FromBody] Pizza data)
        {
            using (PizzaContext context = new PizzaContext())
            {

                Pizza pz = new Pizza { Name = data.Name, Description = data.Description, Image = data.Image, Price = data.Price, CategoryId = data.CategoryId };
           
                context.Pizzas.Add(pz);
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpPut ("{id}")]
        public IActionResult EditPizza(int id, [FromBody] Pizza data)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                Pizza pizza = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                if (pizza != null)
                {
                    pizza.Image = data.Image;
                    pizza.Name = data.Name;
                    pizza.Description = data.Description;
                    pizza.Price = data.Price;
                    pizza.CategoryId = data.CategoryId;
                    pizza.Ingredients.Clear();
       
                    ctx.Pizzas.Update(pizza);
                    ctx.SaveChanges();
                    return Ok();
                }
                else
                {

                    return NotFound();
                }
            }
           
        
        }
        [HttpPut("{id}")]
        public IActionResult DeletePizza(int id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                var pizza = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                ctx.Pizzas.Remove(pizza);
                ctx.SaveChanges();
                return Ok();
            }
        }

    }
}
