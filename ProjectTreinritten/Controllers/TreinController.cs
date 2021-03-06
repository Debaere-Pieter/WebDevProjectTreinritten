﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Extensions;
using ProjectTreinritten.Service;
using ProjectTreinritten.Services;
using ProjectTreinritten.ViewModel;
using ProjectTreinritten.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ProjectTreinritten.Controllers
{
    public class TreinController : Controller
    {
        private BoekingService boekingService;
        private HotelService hotelService;
        private RitService ritService;
        private StationService stationService;
        private TrajectService trajectService;
        private TreinTypeService treinTypeService;
        private VakantieDagenService vakantieDagenService;

        public TreinController()
        {
            boekingService = new BoekingService();
            hotelService = new HotelService();
            ritService = new RitService();
            stationService = new StationService();
            trajectService = new TrajectService();
            treinTypeService = new TreinTypeService();
            vakantieDagenService = new VakantieDagenService();
        }

        //todo's vragen voor de prijs en vragen of we moeten checken of ticketten uniek moeten zijn.
        //overstappen hardcoden of computer laten zoeken
        //namen pieter jan navragen
        //script bieridentity
        //wisselknop doen



        //startpagina met linken naar alle functionaliteiten
        public IActionResult Index()
        {
            return View();
        }

        //pagina met alle informatie over het project: in opdracht van, door wie gemaakt etc.
        public IActionResult Over()
        {
            return View();
        }

        //pagina om een boeking te doen

        public IActionResult Boeken()
        {

            ViewBag.StationLijst =
                new SelectList(stationService.GetAll(),
                 "StationId", "StationNaam");


            ViewBag.HotelLijst =
                new SelectList(hotelService.GetAll(),
                 "HotelId", "HotelNaam");

            return View();
        }

        [HttpPost]
        public IActionResult KiezenRit(BoekenVM b)
        {
            if (b == null)
            {
                return NotFound();
            }

            //System.Diagnostics.Debug.WriteLine("printen in console");


            //inputcontrole
            if (b.Eindpunt == b.Vertrekpunt)
            {
                ModelState.AddModelError(nameof(b.Eindpunt), "Eindpunt en vertrekpunt mogen niet dezelfde waarde hebben");
            }
            if (b.Eindpunt == 0)
            {
                ModelState.AddModelError(nameof(b.Eindpunt), "Gelieve een eindpunt te selecteren");
            }
            if (b.Vertrekpunt == 0)
            {
                ModelState.AddModelError(nameof(b.Eindpunt), "Gelieve een eindpunt te selecteren");
            }

            if(b.Vertrekdatum == null)
            {
                ModelState.AddModelError(nameof(b.Vertrekdatum), "Gelieve een datum op te geven in formaat yyyy - mm - dd");
            }
            else { 
                if (b.Vertrekdatum.Equals(""))
                {
                    ModelState.AddModelError(nameof(b.Vertrekdatum), "Gelieve een datum op te geven in formaat yyyy - mm - dd");
                }
            }

            for (int a = 0; a < b.AantalPersonen; a++)
            {
                if (string.IsNullOrEmpty(b.Namen[a]) || b.Namen[a].Length < 3 || b.Namen[a].Length > 30)
                {
                    ModelState.AddModelError(nameof(b.Namen), "Gelieve een geldige naam voor persoon " + (a + 1) + " op te geven");
                }
                if (string.IsNullOrEmpty(b.Voornamen[a]) || b.Voornamen[a].Length < 3 || b.Voornamen[a].Length > 30)
                {
                    ModelState.AddModelError(nameof(b.Voornamen), "Gelieve een geldige voornaam voor persoon " + (a + 1) + " op te geven");
                }
            }

            if (ModelState.IsValid)
            {
                var vertrekpunt = b.Vertrekpunt;
                var aankomst = b.Eindpunt;
                var route = new List<Traject>();
                b.VertrekpuntNaam = stationService.Get(b.Vertrekpunt).StationNaam;
                b.EindpuntNaam = stationService.Get(b.Eindpunt).StationNaam;
                //eerst gaan we de route bepalen, deze houd nog geen rekening met de tijstippen, dit doen we puur voor de route en overstappen.

                var list = ritService.GetAllByCitiesWithDate(b.Vertrekpunt, b.Eindpunt, DateTime.Parse(b.Vertrekdatum));
                //als de lijst leeg is wil dit zeggen dat  de route uit meer dan één rit bestaat
                if (list.Count() == 0)
                {
                    var listVertrekRitten = ritService.GetAllByDepartCity(vertrekpunt);
                    //datecontrole toevoegen
                    var listAankomstRitten = ritService.GetAllByArrivalCity(aankomst);
                    //datecontrole toevoegen
                    Boolean gevonden = false;

                    foreach (var item in listVertrekRitten)
                    {
                        //listVertrekRitten2.Add(item.AankomstStationId);
                        foreach (var item2 in listAankomstRitten)
                        {
                            //listAankomstRitten2.Add(item.VertrekStationId);
                            if (item.AankomstStationId == item2.VertrekStationId)
                            {
                                //trahect bestaat uit 2 ritten

                                Traject traject = new Traject
                                {
                                    Rit1Id = item.RitId,
                                    Rit2Id = item2.RitId
                                };

                                var trajectenList = new List<Traject>();
                                trajectenList.Add(traject);
                                route = trajectenList;

                                gevonden = true;

                            }
                        }
                    }
                    if (!gevonden)
                    {
                        //indien er nog stteds geen traject is gevonden zal het bestaan uit 2 tussenpunten
                        foreach (var item in listVertrekRitten)
                        {
                            foreach (var item2 in listAankomstRitten)
                            {
                                if (ritService.GetAllByCitiesWithDate(item.AankomstStationId, item2.VertrekStationId, DateTime.Parse(b.Vertrekdatum)).Count() != 0)
                                {
                                    Traject traject = new Traject
                                    {
                                        Rit1Id = item.RitId,
                                        Rit2Id = ritService.GetAllByCitiesWithDate(item.AankomstStationId, item2.VertrekStationId, DateTime.Parse(b.Vertrekdatum)).ElementAt(0).RitId,
                                        Rit3Id = item2.RitId
                                    };

                                    var trajectenList = new List<Traject>();
                                    trajectenList.Add(traject);
                                    route = trajectenList;

                                    gevonden = true;
                                }
                            }
                        }

                    }
                }
                else
                {
                    //traject bestaat uit één rit
                    Traject traject = new Traject
                    {
                        Rit1Id = list.ElementAt(0).RitId
                    };
                    var trajectenList = new List<Traject>();
                    trajectenList.Add(traject);
                    route = trajectenList;
                }


                //route is bepaald en zal nu aangemaakt worden met de juiste tijdstippen en toegevoegd worden aan de VM


                if (route[0].Rit3Id == null)
                {
                    if (route[0].Rit2Id == null)
                    {
                        //traject bestaat uit 0 overstappen
                        Rit r = ritService.Get(route[0].Rit1Id);
                        var ritten = ritService.GetAllByCitiesWithDate(r.VertrekStationId, r.AankomstStationId, DateTime.Parse(b.Vertrekdatum));
                        var trajecten = new List<Traject>();
                        foreach (var rit in ritten)
                        {
                            Traject traject = new Traject
                            {
                                Rit1Id = rit.RitId
                            };

                            if (trajectService.GetTrajecten1Rit(traject.Rit1Id).Count() != 0)
                            {

                                traject = trajectService.GetTrajecten1Rit(traject.Rit1Id).ElementAt(0);
                            }
                            else
                            {
                                trajectService.Create(traject);
                            }
                            //code gilles
                            //Rit rit1 = ritService.Get(traject.Rit1Id);
                            //TreinType treinType1 = treinTypeService.Get(rit1.TreinTypeId);
                            //int MaxAantalPersonen = 0;
                            //int HuidigAantalPersonen = 0;
                            //if (b.Klasse.Equals("Economic"))
                            //{
                            //    MaxAantalPersonen = treinType1.CapaciteitEconomic;

                            //}
                            //else if (b.Klasse.Equals("Business"))
                            //{
                            //    MaxAantalPersonen = treinType1.CapaciteitBusiness;
                            //}

                            //var boekingen = boekingService.GetAllByDate(DateTime.Parse(b.Vertrekdatum));
                            //Traject TeTestenTraject = traject;
                            //List<int> ritIDs = new List<int>();
                            //ritIDs.Add(TeTestenTraject.Rit1Id);
                            //if (TeTestenTraject.Rit2Id != null)
                            //{
                            //    ritIDs.Add((int)TeTestenTraject.Rit2Id);
                            //}
                            //if (TeTestenTraject.Rit3Id != null)
                            //{
                            //    ritIDs.Add((int)TeTestenTraject.Rit3Id);
                            //    if (ritIDs.Contains((int)t.Rit3Id))
                            //    {
                            //        HuidigAantalPersonen++;
                            //    }
                            //}
                            //code gilles


                            trajecten.Add(traject);
                            b.AlleRitten = ritService.GetAll();
                        };
                        b.Trajecten = trajecten;
                    }
                    else
                    {
                        //traject bestaat uit 1 overstap
                        Rit r = ritService.Get(route[0].Rit1Id);
                        Rit r1 = ritService.Get(route[0].Rit2Id ?? default(int));
                        var ritten = ritService.GetAllByCitiesWithDate(r.VertrekStationId, r.AankomstStationId, DateTime.Parse(b.Vertrekdatum));
                        var trajecten = new List<Traject>();


                        foreach (var rit in ritten)
                        {
                            Rit rit2 = null;
                            if (ritService.GetRitByCitiesWithDateAndTime(r1.VertrekStationId, r1.AankomstStationId, DateTime.Parse(b.Vertrekdatum), rit.AankomstUur).Count() > 0)
                            {
                                rit2 = ritService.GetRitByCitiesWithDateAndTime(r1.VertrekStationId, r1.AankomstStationId, DateTime.Parse(b.Vertrekdatum), rit.AankomstUur).ElementAt(0);
                            }
                            else
                            {
                                rit2 = ritService.GetAllByCitiesWithDate(r1.VertrekStationId, r1.AankomstStationId, DateTime.Parse(b.Vertrekdatum).AddDays(1)).ElementAt(0);
                            }

                            Traject traject = new Traject
                            {
                                Rit1Id = rit.RitId,
                                Rit2Id = rit2.RitId
                            };
                            if (trajectService.GetTrajecten2Rit(traject.Rit1Id, (int)traject.Rit2Id).Count() != 0)
                            {

                                traject = trajectService.GetTrajecten2Rit(traject.Rit1Id, (int)traject.Rit2Id).ElementAt(0);
                            }
                            else
                            {
                                trajectService.Create(traject);
                            }

                            trajecten.Add(traject);


                            //einde code gilles
                        };
                        b.Trajecten = trajecten;
                        b.AlleRitten = ritService.GetAll();

                    }
                }
                else
                {
                    //traject bestaat uit 2 overstappen
                    Rit r = ritService.Get(route[0].Rit1Id);
                    Rit r1 = ritService.Get(route[0].Rit2Id ?? default(int));
                    Rit r2 = ritService.Get(route[0].Rit3Id ?? default(int));
                    var ritten = ritService.GetAllByCitiesWithDate(r.VertrekStationId, r.AankomstStationId, DateTime.Parse(b.Vertrekdatum));
                    var trajecten = new List<Traject>();
                    foreach (var rit in ritten)
                    {
                        Rit rit2 = null;
                        if (ritService.GetRitByCitiesWithDateAndTime(r1.VertrekStationId, r1.AankomstStationId, DateTime.Parse(b.Vertrekdatum), rit.AankomstUur).Count() > 0)
                        {
                            rit2 = ritService.GetRitByCitiesWithDateAndTime(r1.VertrekStationId, r1.AankomstStationId, DateTime.Parse(b.Vertrekdatum), rit.AankomstUur).ElementAt(0);
                        }
                        else
                        {
                            rit2 = ritService.GetAllByCitiesWithDate(r1.VertrekStationId, r1.AankomstStationId, DateTime.Parse(b.Vertrekdatum).AddDays(1)).ElementAt(0);
                        }

                        Rit rit3 = null;
                        if (ritService.GetRitByCitiesWithDateAndTime(r2.VertrekStationId, r2.AankomstStationId, DateTime.Parse(b.Vertrekdatum), rit2.AankomstUur).Count() > 0)
                        {
                            rit3 = ritService.GetRitByCitiesWithDateAndTime(r2.VertrekStationId, r2.AankomstStationId, DateTime.Parse(b.Vertrekdatum), rit2.AankomstUur).ElementAt(0);
                        }
                        else
                        {
                            rit3 = ritService.GetAllByCitiesWithDate(r2.VertrekStationId, r2.AankomstStationId, DateTime.Parse(b.Vertrekdatum).AddDays(1)).ElementAt(0);
                        }

                        Traject traject = new Traject
                        {
                            Rit1Id = rit.RitId,
                            Rit2Id = rit2.RitId,
                            Rit3Id = rit3.RitId
                        };
                        if (trajectService.GetTrajecten3Rit(traject.Rit1Id, (int)traject.Rit2Id, (int)traject.Rit3Id).Count() != 0)
                        {

                            traject = trajectService.GetTrajecten3Rit(traject.Rit1Id, (int)traject.Rit2Id, (int)traject.Rit3Id).ElementAt(0);
                        }
                        else
                        {
                            trajectService.Create(traject);
                        }
                        trajecten.Add(traject);
                    };
                    b.Trajecten = trajecten;
                    b.AlleRitten = ritService.GetAll();
                }

                return View(b);
            }
            else
            {

                ViewBag.StationLijst =
                    new SelectList(stationService.GetAll(),
                     "StationId", "StationNaam");


                ViewBag.HotelLijst =
                    new SelectList(hotelService.GetAll(),
                     "HotelId", "HotelNaam");

                return View("Boeken", b);
            }
        }

        //pagina die bevestiging van boeking toont
        public IActionResult Bevestiging(BoekingVM b)
        {
            if (b == null)
            {
                return NotFound();
            }

            return View(b);
        }

        [Authorize]
        public IActionResult Historiek()
        {
            ViewBag.Ritten = ritService.GetAll().ToList();
            ViewBag.Stations = stationService.GetAll().ToList();
            ViewBag.Trajecten = trajectService.GetAll().ToList();
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var list = boekingService.GetAllByUser(userID);
            ViewBag.alleHotels = hotelService.GetAll();
            return View(list);
        }

        

        //pagina om winkelkar te bekijken
        public IActionResult Winkelkar()
        {
            return RedirectToAction("Index", "ShoppingCart");
        }

        //pagina om in te loggen
        public IActionResult Inloggen()
        {
            return View();
        }

        //pagina voor account aan te maken
        public IActionResult Registreer()
        {
            return View();
        }

        //404 pagina, deze pagina word getoond indien de pagina waarnaar men surft leeg is
        public IActionResult Leeg()
        {
            return View();
        }

        //pagina waar men wachtwoord kan opvragen via email indien men deze vergeten is
        public IActionResult Wachtwoord()
        {
            return View();
        }

        public IActionResult Select(BoekenVM b)
        {
            if (b == null)
            {
                return NotFound();
            }
            Traject t = trajectService.Get(b.GekozenTrajectId);

            ShoppingCartVM shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            for (var i = 0; i < b.Namen.Count(); i++)
            {
                string eindPunt = null; //kleine letter om verwarring te voorkomen
                string vertrekPunt = null;
                string hotel = ""; // kleine letter om verwarring te vermijden
                if (b.Namen[i] != null)
                {
                    var prijs = 0;
                    if (t.Rit1Id != 0)
                    {
                        var type = ritService.GetTreinTypeRit(t.Rit1Id);
                        if (b.Klasse.Equals("Economic"))
                        {
                            prijs += type.PrijsEconomic;
                        }
                        else if (b.Klasse.Equals("Business"))
                        {
                            prijs += type.PrijsBusiness;
                        }
                    }

                    if (t.Rit2Id != 0 && t.Rit2Id != null)
                    {
                        var type = ritService.GetTreinTypeRit((int)t.Rit2Id);
                        if (b.Klasse.Equals("Economic"))
                        {
                            prijs += type.PrijsEconomic;
                        }
                        else if (b.Klasse.Equals("Business"))
                        {
                            prijs += type.PrijsBusiness;
                        }
                    }
                    else
                    {
                        var rit1 = ritService.Get(t.Rit1Id);
                        var station = stationService.Get(rit1.AankomstStationId);
                        eindPunt = station.StationNaam;
                    }

                    if (t.Rit3Id != 0 && t.Rit3Id != null)
                    {
                        var rit3 = ritService.Get((int)t.Rit3Id);
                        var station = stationService.Get(rit3.AankomstStationId);
                        eindPunt = station.StationNaam;

                        var type = ritService.GetTreinTypeRit((int)t.Rit3Id);
                        if (b.Klasse.Equals("Economic"))
                        {
                            prijs += type.PrijsEconomic;
                        }
                        else if (b.Klasse.Equals("Business"))
                        {
                            prijs += type.PrijsBusiness;
                        }
                    }
                    else if(t.Rit2Id != 0 && t.Rit2Id != null)
                    {
                        var rit2 = ritService.Get((int)t.Rit2Id);
                        var station = stationService.Get(rit2.AankomstStationId);
                        eindPunt = station.StationNaam;
                    }

                    var ritVertrek = ritService.Get((int)t.Rit1Id);
                    var stationVertrek = stationService.Get(ritVertrek.VertrekStationId);
                    vertrekPunt = stationVertrek.StationNaam;
                    
                    if (b.HotelId != null && b.HotelId != 0)
                    {
                        var GekozenHotel = hotelService.Get((int)b.HotelId);
                        hotel = GekozenHotel.HotelNaam;
                    }                   

                    CartVM item = new CartVM
                    {
                        TrajectNr = t.TrajectId,
                        Naam = b.Namen.ElementAt(i),
                        Voornaam = b.Voornamen.ElementAt(i),
                        Prijs = prijs,
                        Aantal = 1,
                        HotelId = b.HotelId,
                        Klasse = b.Klasse,
                        Vertrekdatum = b.Vertrekdatum,
                        DateCreated = DateTime.Now,
                        VertrekPunt = vertrekPunt,
                        EindPunt = eindPunt,
                        Hotel = hotel
                    };

                    shopping.Cart.Add(item);

                    HttpContext.Session.SetObject("ShoppingCart", shopping);
                }
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactVM model)
        {
            if (model == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p> Email From: " + "{0} ({1}) </p> <p>Message:" +
                        "</p><p>{2}</p>";
                    body = string.Format(body, model.JouwNaam, model.JouwEmail, model.Message);

                    EmailSender mail = new EmailSender();
                    await mail.SendEmailAsync("info.tgveurope@gmail.com", "contact", body);
                    return RedirectToAction("Sent");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public IActionResult Sent()
        {
            return View();
        }

        [Route("/CustomErrorPages/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}