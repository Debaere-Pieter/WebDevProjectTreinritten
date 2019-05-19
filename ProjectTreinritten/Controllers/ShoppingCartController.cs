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
                Zetels zetels; 
                
                BoekingService boekingService = new BoekingService();
                TrajectService trajectService = new TrajectService();
                RitService ritService = new RitService();
                ZetelService zetelService = new ZetelService();

                foreach (CartVM cart in carts.Cart)
                {
                    boeking = new Boeking();
                    zetels = new Zetels();                    

                    Traject t = trajectService.Get(cart.TrajectNr);
                    var boekingen = boekingService.GetAllByDate(DateTime.Parse(cart.Vertrekdatum));
                    int ZetelID = 0;

                    if(t.Rit1Id != 0)
                    {
                        TreinType TypeRit1 = ritService.GetTreinTypeRit(t.Rit1Id);

                        int MaxAantalPersonen = 0;
                        int HuidigAantalPersonen = 0;
                        if (cart.Klasse.Equals("Economic"))
                        {
                            MaxAantalPersonen = TypeRit1.CapaciteitEconomic;

                        }
                        else if (cart.Klasse.Equals("Business"))
                        {
                            MaxAantalPersonen = TypeRit1.CapaciteitBusiness;
                        }
                                                
                        foreach (Boeking b in boekingen)
                        {
                            Traject TeTestenTraject = trajectService.Get(b.TrajectId);
                            List<int> ritIDs = new List<int>
                        {
                            TeTestenTraject.Rit1Id,
                            (int)TeTestenTraject.Rit2Id, //indien null zullen ze worden omgezet naar 0
                            (int)TeTestenTraject.Rit3Id
                        };
                            if (ritIDs.Contains(t.Rit1Id))
                            {
                                HuidigAantalPersonen++;
                            }
                        }
                        if(HuidigAantalPersonen + 1 <= MaxAantalPersonen)
                        {
                            zetels.Rit1Zetel = HuidigAantalPersonen + 1;
                        }
                    }                    
                    
                    if(t.Rit2Id != 0 && t.Rit2Id != null)
                    {
                        TreinType TypeRit2 = ritService.GetTreinTypeRit((int)t.Rit2Id);

                        int MaxAantalPersonen = 0;
                        int HuidigAantalPersonen = 0;
                        if (cart.Klasse.Equals("Economic"))
                        {
                            MaxAantalPersonen = TypeRit2.CapaciteitEconomic;

                        }
                        else if (cart.Klasse.Equals("Business"))
                        {
                            MaxAantalPersonen = TypeRit2.CapaciteitBusiness;
                        }

                        foreach (Boeking b in boekingen)
                        {
                            Traject TeTestenTraject = trajectService.Get(b.TrajectId);
                            List<int> ritIDs = new List<int>
                        {
                            TeTestenTraject.Rit1Id,
                            (int)TeTestenTraject.Rit2Id, //indien null zullen ze worden omgezet naar 0
                            (int)TeTestenTraject.Rit3Id
                        };
                            if (ritIDs.Contains((int)t.Rit2Id))
                            {
                                HuidigAantalPersonen++;
                            }
                        }
                        if (HuidigAantalPersonen + 1 <= MaxAantalPersonen)
                        {
                            zetels.Rit2Zetel = HuidigAantalPersonen + 1;
                        }
                    }

                    if (t.Rit3Id != 0 && t.Rit3Id != null)
                    {
                        TreinType TypeRit3 = ritService.GetTreinTypeRit((int)t.Rit3Id);

                        int MaxAantalPersonen = 0;
                        int HuidigAantalPersonen = 0;
                        if (cart.Klasse.Equals("Economic"))
                        {
                            MaxAantalPersonen = TypeRit3.CapaciteitEconomic;

                        }
                        else if (cart.Klasse.Equals("Business"))
                        {
                            MaxAantalPersonen = TypeRit3.CapaciteitBusiness;
                        }
                        
                        foreach (Boeking b in boekingen)
                        {
                            Traject TeTestenTraject = trajectService.Get(b.TrajectId);
                            List<int> ritIDs = new List<int>
                        {
                            TeTestenTraject.Rit1Id,
                            (int)TeTestenTraject.Rit2Id, //indien null zullen ze worden omgezet naar 0
                            (int)TeTestenTraject.Rit3Id
                        };
                            if (ritIDs.Contains((int)t.Rit3Id))
                            {
                                HuidigAantalPersonen++;
                            }
                        }
                        if (HuidigAantalPersonen + 1 <= MaxAantalPersonen)
                        {
                            zetels.Rit3Zetel = HuidigAantalPersonen + 1;
                        }
                    }

                    try
                    {
                        zetelService.Create(zetels);
                        ZetelID = zetels.ZetelId;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }

                    boeking.BoekingsDatum = DateTime.UtcNow;
                    boeking.VertrekDatum = DateTime.Parse(cart.Vertrekdatum);
                    boeking.Naam = cart.Naam;
                    boeking.Voornaam = cart.Voornaam;
                    boeking.HotelId = cart.HotelId;
                    boeking.TrajectId = cart.TrajectNr;
                    boeking.LoginId = userID;
                    boeking.Klasse = cart.Klasse;
                    boeking.ZetelId = ZetelID;

                    try
                    {
                        boekingService.Create(boeking);                        
                    }
                    catch (Exception ex)
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