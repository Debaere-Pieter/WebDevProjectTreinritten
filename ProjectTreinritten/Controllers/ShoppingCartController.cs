using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjectTreinritten.Extensions;
using ProjectTreinritten.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectTreinritten.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            //code om het gebruik van generic classes uit te leggen
            //we hebben een extention methode geschreven zie de map extentions
            //"ShoppingCart" is de naam van mijn brandkast
            //<ShoppingCartVM> vervangt de T
            ShoppingCartVM cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            //dit is puur extra (geeft u de session id), dit is tech niet nodig
            var SessionId = HttpContext.Session.Id;


            return View(cartList);
        }

        public ActionResult Delete(int? trajectNr)
        {
            if (trajectNr == null)
            {
                return NotFound();
            }

            ShoppingCartVM cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            var itemToRemove = cartList.Cart.FirstOrDefault(r => r.TrajectNr == trajectNr);

            if (itemToRemove != null)
            {
                cartList.Cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }
            return View("index", cartList);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Payment(List<CartVM> cart)
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                //call method to save
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}