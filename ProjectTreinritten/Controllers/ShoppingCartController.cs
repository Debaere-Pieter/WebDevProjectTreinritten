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
using ProjectTreinritten.ViewModels;
using ProjectTreinritten.Models;
using ProjectTreinritten.Services;

namespace ProjectTreinritten.Controllers
{
    public class ShoppingCartController : Controller

    {
        private BoekingService boekingService;
        private HotelService hotelService;
        private RitService ritService;
        private StationService stationService;
        private TrajectService trajectService;
        private TreinTypeService treinTypeService;
        private VakantieDagenService vakantieDagenService;

        public ShoppingCartController()
        {
            boekingService = new BoekingService();
            hotelService = new HotelService();
            ritService = new RitService();
            stationService = new StationService();
            trajectService = new TrajectService();
            treinTypeService = new TreinTypeService();
            vakantieDagenService = new VakantieDagenService();
        }
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
        public async Task<ActionResult> Payment(ShoppingCartVM carts)
        {
            if (carts == null)
            {
                return NotFound();
            }
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
                StationService stationService = new StationService();

                foreach (CartVM cart in carts.Cart)
                {                    
                    boeking = new Boeking();
                    zetels = new Zetels();
                    
                    Traject t = trajectService.Get(cart.TrajectNr);
                    //alle boekingen ophalen van de boekingsdatum
                    var boekingen = boekingService.GetAllByDate(DateTime.Parse(cart.Vertrekdatum));

                    int ZetelID = 0;


                    //het komene blok code dient om de boekingen die niet mogelijk zijn wegens plaatsgebrek te blokkeren.
                    if(t.Rit1Id != 0)
                    {
                        //type trein opvragne van de eerste rit
                        TreinType TypeRit1 = ritService.GetTreinTypeRit(t.Rit1Id);

                        int MaxAantalPersonen = 0;
                        int HuidigAantalPersonen = 0;

                        //maxAantalPersonen instellen aan de hand van de klasse
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
                            //trjact ophalen dat we willen controleren
                            Traject TeTestenTraject = trajectService.Get(b.TrajectId);
                            //lijst om alle ritId's in te steken.
                            List<int> ritIDs = new List<int>();
                            ritIDs.Add(TeTestenTraject.Rit1Id);
                            if (TeTestenTraject.Rit2Id != null)
                            {
                                ritIDs.Add((int)TeTestenTraject.Rit2Id);
                            }
                            if (TeTestenTraject.Rit3Id != null)
                            {
                                ritIDs.Add((int)TeTestenTraject.Rit3Id);
                            }                            
                            if (ritIDs.Contains(t.Rit1Id))
                            {
                                HuidigAantalPersonen++;
                            }
                        }
                        if(HuidigAantalPersonen + 1 <= MaxAantalPersonen)
                        {                            
                            zetels.Rit1Zetel = HuidigAantalPersonen + 1;  
                            cart.Zetel1 = HuidigAantalPersonen + 1;
                        }
                        else
                        {
                            Rit r = ritService.Get(t.Rit1Id);
                            Station s = stationService.Get(r.VertrekStationId);
                            return View("TeWeinigPlaatsen", s.StationNaam);
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
                            List<int> ritIDs = new List<int>();
                            ritIDs.Add(TeTestenTraject.Rit1Id);
                            if (TeTestenTraject.Rit2Id != null)
                            {
                                ritIDs.Add((int)TeTestenTraject.Rit2Id);
                                if (TeTestenTraject.Rit3Id != null)
                                {
                                    ritIDs.Add((int)TeTestenTraject.Rit3Id);
                                }
                                if (ritIDs.Contains((int)t.Rit2Id))
                                {
                                    HuidigAantalPersonen++;
                                }
                            }                            
                        }
                        if (HuidigAantalPersonen + 1 <= MaxAantalPersonen)
                        {
                            zetels.Rit2Zetel = HuidigAantalPersonen + 1;
                            cart.Zetel2 = HuidigAantalPersonen + 1;
                        }
                        else
                        {
                            Rit r = ritService.Get((int)t.Rit2Id);
                            Station s = stationService.Get(r.VertrekStationId);
                            return View("TeWeinigPlaatsen", s.StationNaam);
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
                            List<int> ritIDs = new List<int>();
                            ritIDs.Add(TeTestenTraject.Rit1Id);
                            if (TeTestenTraject.Rit2Id != null)
                            {
                                ritIDs.Add((int)TeTestenTraject.Rit2Id);
                            }
                            if (TeTestenTraject.Rit3Id != null)
                            {
                                ritIDs.Add((int)TeTestenTraject.Rit3Id);
                                if (ritIDs.Contains((int)t.Rit3Id))
                                {
                                    HuidigAantalPersonen++;
                                }
                            }
                            
                        }
                        if (HuidigAantalPersonen + 1 <= MaxAantalPersonen)
                        {
                            zetels.Rit3Zetel = HuidigAantalPersonen + 1;
                            cart.Zetel3 = HuidigAantalPersonen + 1;                            
                        }
                        else
                        {
                            Rit r = ritService.Get((int)t.Rit3Id);
                            Station s = stationService.Get(r.VertrekStationId);
                            return View("TeWeinigPlaatsen", s);
                        }
                    }

                    try
                    {
                        //zetels instellen en indien dit niet lukt een exception opvangen
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
                    if (cart.HotelId != null && cart.HotelId != 0)
                    {
                        boeking.HotelId = cart.HotelId;
                    } else
                    {
                        boeking.HotelId = null;
                    }                    
                    boeking.TrajectId = cart.TrajectNr;
                    boeking.LoginId = userID;
                    boeking.Klasse = cart.Klasse;
                    boeking.ZetelId = ZetelID;


                    //boeking aanmaken en indien dit niet lukt het probleem loggen
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

            //mail

            if (ModelState.IsValid)
            {
                UserService userService = new UserService();
                AspNetUsers user = userService.Get(userID);
                try
                {
                    var naam = "TGV";
                    var message = "Bedankt om te boeken bij TGV";
                    var body = "<p>Email From: " +
                                                 "{0} (info.tgveurope@gmail.com)</p><p>Message: " +
                                                 "</p><p>{1}</p>";
                    body = string.Format(body, naam, message);

                    EmailSender mail = new EmailSender();
                    await mail.SendEmailAsync(user.Email, "Bevestiging boeking", body);
            
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            //mail
            ViewBag.Ritten = ritService.GetAll().ToList();
            ViewBag.Stations = stationService.GetAll().ToList();
            ViewBag.Trajecten = trajectService.GetAll().ToList();

            return View("Payment", carts);
        }
    }
}