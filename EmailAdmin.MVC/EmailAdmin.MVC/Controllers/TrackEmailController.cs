using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Admin.Common.Controllers;
using Admin.Common.Dto;
using Admin.Common.Dto.Graficos;
using EmailAdmin.MVC.Helpers;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using FrameworkDAC.Negocio;
using Newtonsoft.Json;

namespace EmailAdmin.MVC.Controllers
{
    public class TrackEmailController : SessionController
    {

        public IList<TrackEmail> trackEmails
        {
            get
            {
                if (TempData.ContainsKey("trackEmails"))
                {
                    var d = ((IList<TrackEmail>)TempData["trackEmails"]);
                    TempData.Keep();
                    return d;
                }
                else
                    return new List<TrackEmail>();
            }
            set
            {
                if (!TempData.ContainsKey("trackEmails"))
                    TempData.Add("trackEmails", value);
                else
                    TempData["trackEmails"] = value;
            }
        }

        public DTOListableItems listItems
        {
            get
            {
                if (TempData.ContainsKey("ListItems"))
                {
                    var d = ((DTOListableItems)TempData["ListItems"]);
                    TempData.Keep();
                    return d;
                }
                else
                    return new DTOListableItems();
            }
            set
            {
                TempData["ListItems"] = value;
            }
        }

        private DTOListableItems SetListItems()
        {
            DTOListableItems listItems = new DTOListableItems();
            listItems.ListaElementos = new List<ObjetoNegocio>();
            foreach (TrackEmail trackEmail in trackEmails)
                listItems.ListaElementos.Add(trackEmail);
            return listItems;
        }    

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = @Resources.TrackEmail.TituloBuscador;
            return View("TrackEmailSearch", new TrackEmailSearch());
        }

        [HttpPost]
        public ActionResult List(TrackEmailSearch filtros)
        {
            SaveSearchData(filtros);
            trackEmails = TrackEmailHome.FindAll(filtros);

            DTOListableItems _listItems = SetListItems();
            _listItems.ListaCampos = new Dictionary<string, string>
            { 
                { "id", "Id" }, 
                { Resources.TrackEmail.Campania, "Campania" }, 
                { Resources.TrackEmail.Evento, "Evento"},
                { Resources.TrackEmail.Email, "Email"},
                {Resources.TrackEmail.Documento,"Documento"},
                { Resources.TrackEmail.Pais,"CountryCode"},
                {Resources.TrackEmail.VoucherCode,"VoucherCode"},
                { Resources.TrackEmail.FechaCreacion, "Fecha" },
                { Resources.TrackEmail.FechaUltimaApertura, "DateLastOpen" },
                { Resources.TrackEmail.Cantidad, "Count" }                
            };

            _listItems.OtherNavigationProperties = new List<NavigationProperties>();
            _listItems.OtherNavigationProperties.Add(new NavigationProperties()
            {
                EntityController = "TrackEmail",
                FkAction = "VerFechas",
                FkEntityController = "IdTrackEmail",
                IconName = "glyphicon glyphicon-th-list",
                divName = "divTrackFechas"
            });

            listItems=_listItems;

            return PartialView("GenericResults", listItems);
        }

        [HttpPost]
        public ActionResult Graficar()
        {
            List<DTOGrafico> misGraficos = new List<DTOGrafico>();

            Dictionary<string, Color> LabelsColors = SessionManager.LabelsColors;
            misGraficos.Add(GraficarEnviados(trackEmails, ref LabelsColors));
            misGraficos.Add(GraficarAbiertos(trackEmails, ref LabelsColors));
            misGraficos.Add(GraficarCampanias(trackEmails, ref LabelsColors));
            SessionManager.LabelsColors = LabelsColors;

            TempData["Graficos"] = misGraficos;
            return PartialView("GenericGrafic", misGraficos);
        }

        private void SaveSearchData(TrackEmailSearch filtros)
        {
            dynamic searchData = new System.Dynamic.ExpandoObject();
            searchData = filtros;
            TempData["TrackEmailSearch"] = searchData;
        }

        public DTOGraficoLine GraficarEnviados(IList<TrackEmail> lst, ref Dictionary<string, Color> LabelsColors)
        {
            DTOGraficoLine graf = new DTOGraficoLine();
            graf.LabelsColors = LabelsColors;
            graf.Title = Resources.TrackEmail.Campania;
            graf.SubTitle = Resources.TrackEmail.CantidadPixelEnviados;
            graf.divId = "trackemailcantidades";
            graf.DataGraf.data.labelName = "FechaCorta";
            ((DTOGrafDataLine)graf.DataGraf.data).groupName = "Campania";
            graf.DataGraf.data.valueName = "Id";
            graf.DataGraf.data.operacion = Operacion.Count;
            
            graf.Graficar<TrackEmail>(lst);

            return graf;

        }

        public DTOGraficoLine GraficarAbiertos(IList<TrackEmail> lst, ref Dictionary<string, Color> LabelsColors)
        {
            DTOGraficoLine graf = new DTOGraficoLine();
            graf.LabelsColors = LabelsColors;
            graf.Title = Resources.TrackEmail.Campania;
            graf.SubTitle = "Cantidad de Abiertos";
            graf.divId = "trackemailOpen";
            graf.DataGraf.data.labelName = "ShortDateLastOpen";
            ((DTOGrafDataLine) graf.DataGraf.data).groupName = "Campania";
            graf.DataGraf.data.valueName = "Id";
            graf.DataGraf.data.operacion = Operacion.Count;
                
            graf.Graficar<TrackEmail>(lst);
            return graf;
        }


        private DTOGraficoDona GraficarCampanias(IList<TrackEmail> lst, ref Dictionary<string, Color> LabelsColors)
        {
            DTOGraficoDona graf = new DTOGraficoDona();
            graf.LabelsColors = LabelsColors;
            graf.Title = Resources.TrackEmail.Campania;
            graf.SubTitle = "Cantidades";
            graf.divId = "trackemailCampanias";
            graf.DataGraf.data.labelName = "Campania";
            graf.DataGraf.data.valueName = "Id";
            graf.DataGraf.data.operacion = Operacion.Count;

            graf.Graficar<TrackEmail>(lst);
            return graf;
        }

        [HttpPost]
        public ActionResult VerFechas(int Id)
        {
            IList<TrackEmailEvent> fechas;
            fechas = TrackEmailHome.FindFechas(Id);

            DTOListableItems listItems = new DTOListableItems();
            listItems.ListaElementos = new List<ObjetoNegocio>();
            foreach (TrackEmailEvent trackEmailEvent in fechas)
                listItems.ListaElementos.Add(trackEmailEvent);
            listItems.ListaCampos = new Dictionary<string, string>
            { 
                { "id", "Id" }, 
                { "Fecha", "Fecha" }
            };

            DTOPopUpListableItems dtoPopUp = new DTOPopUpListableItems();
            dtoPopUp.divPopUpName = "divPopUpTrackFechas";
            dtoPopUp.Title = Resources.TrackEmail.FechasApertura;
            dtoPopUp.ListableItems = listItems;

            return PartialView("API/_PopUpGenericTable", dtoPopUp);
        }

    }
}