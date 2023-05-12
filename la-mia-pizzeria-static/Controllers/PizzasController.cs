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
        public IActionResult CreatePizza()
        {
            using (PizzaContext context = new PizzaContext())
            {

                Pizza pz = new Pizza { Name = "Prova", Description = "d d d d d d ", Image = "https://media-assets.lacucinaitaliana.it/photos/63c0401ffb4d383e74f344dd/1:1/w_4992,h_4992,c_limit/migliore%20pizza%20milano%20pizzeria.jpg", Price = 5, CategoryId = 1 };
           
                context.Pizzas.Add(pz);
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpPut ("{id}")]
        public IActionResult EditPizza(int id, [FromBody] PizzaFormModel data)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                Pizza pizza = ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                if (pizza != null)
                {
                    pizza.Image = data.Pizza.Image;
                    pizza.Name = data.Pizza.Name;
                    pizza.Description = data.Pizza.Description;
                    pizza.Price = data.Pizza.Price;
                    pizza.CategoryId = data.Pizza.CategoryId;
                    pizza.Ingredients.Clear();
                    foreach (string idingredient in data.SelectedIngredients)
                    {
                        int intidIngredient = int.Parse(idingredient);
                        Ingredient ing = ctx.Ingredients.Where(i => i.Id == intidIngredient).FirstOrDefault();
                        pizza.Ingredients.Add(ing);

                    }

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
