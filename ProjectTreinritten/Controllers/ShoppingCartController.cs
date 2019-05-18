using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjectTreinritten.Extensions;
using ProjectTreinritten.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Service;

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
        public ActionResult Payment(ShoppingCartVM carts)
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //altijd in een try catch stoppen
            try
            {
                Boeking boeking;
                BoekingService boekingService = new BoekingService();
                foreach (CartVM cart in carts.Cart)
                {
                    boeking = new Boeking();
                    
                    boeking.BoekingsDatum = DateTime.UtcNow;
                    boeking.VertrekDatum = DateTime.Parse(cart.Vertrekdatum);
                    boeking.Naam = cart.Naam;
                    boeking.Voornaam = cart.Voornaam;
                    boeking.HotelId = cart.HotelId;
                    boeking.TrajectId = cart.TrajectNr;
                    boeking.LoginId = userID;
                    boeking.Klasse = cart.Klasse;
                    try
                    {
                        boekingService.Create(boeking);
                    } catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return View();
        }
    }
}