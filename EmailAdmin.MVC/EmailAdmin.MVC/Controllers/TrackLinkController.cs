using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin.Common.Controllers;
using Admin.Common.Dto;
using Admin.Common.Dto.Graficos;
using EmailAdmin.MVC.Helpers;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Home;
using FrameworkDAC.Negocio;

namespace EmailAdmin.MVC.Controllers
{
    public class TrackLinkController : SessionController
    {
        public IList<TrackLink> trackLinks
        {
            get
            {
                if (TempData.ContainsKey("trackLinks"))
                {
                    var d = ((IList<TrackLink>)TempData["trackLinks"]);
                    TempData.Keep();
                    return d;
                }
                else
                    return new List<TrackLink>();
            }
            set
            {
                if (!TempData.ContainsKey("trackLinks"))
                    TempData.Add("trackLinks", value);
                else
                    TempData["trackLinks"] = value;
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
            foreach (TrackLink trackLink in trackLinks)
                listItems.ListaElementos.Add(trackLink);
            return listItems;
        }    

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = @Resources.TrackLink.TituloBuscador;
            return View("TrackLinkSearch", new TrackLinkSearch());
        }

        [HttpPost]
        public ActionResult List(TrackLinkSearch filtros)
        {
            SaveSearchData(filtros);
            trackLinks = TrackLinkHome.FindAll(filtros);

            DTOListableItems _listItems = SetListItems();
            _listItems.ListaCampos = new Dictionary<string, string>
            { 
                { "id", "Id" }, 
                { "IdEmailLog", "IdEmailLog" }, 
                { Resources.TrackLink.Nombre, "Link_Name" }, 
                { Resources.TrackLink.Template, "TemplateName" },
                { Resources.TrackLink.FechaCreacion, "Fecha" },
                { Resources.TrackLink.FechaUltimaApertura, "DateLastOpen" },
                { Resources.TrackLink.Pais,"CountryCode"},
                { Resources.TrackLink.VoucherCode,"VoucherCode"},
                { Resources.TrackLink.CantidadClicks, "Count" }
            };

            _listItems.OtherNavigationProperties = new List<NavigationProperties>();
            _listItems.OtherNavigationProperties.Add(new NavigationProperties()
            {
                EntityController = "TrackLink",
                FkAction = "VerFechas",
                FkEntityController = "IdTrackEmailLink",
                IconName = "glyphicon glyphicon-th-list",
                divName = "divTrackFechas"
            });

            listItems = _listItems;

            return PartialView("GenericResults", _listItems);
        }

        [HttpPost]
        public ActionResult Graficar()
        {
            List<DTOGrafico> misGraficos = new List<DTOGrafico>();

            Dictionary<string, Color> LabelsColors = SessionManager.LabelsColors;
            misGraficos.Add(GraficarLinks(trackLinks, ref LabelsColors));
            misGraficos.Add(GraficarAbiertos(trackLinks, ref LabelsColors));
            misGraficos.Add(GraficarTemplates(trackLinks, ref LabelsColors));
            SessionManager.LabelsColors = LabelsColors;

            TempData["Graficos"] = misGraficos;
            return PartialView("GenericGrafic", misGraficos);
        }


        private DTOGrafico GraficarAbiertos(IList<TrackLink> lst, ref Dictionary<string, Color> LabelsColors)
        {
            DTOGraficoLine graf = new DTOGraficoLine();
            graf.LabelsColors = LabelsColors;
            graf.Title = "Links";
            graf.SubTitle = "Cantidad de Abiertos";
            graf.divId = "trackemailLinksOpen";
            graf.DataGraf.data.labelName = "DateLastOpen";
            ((DTOGrafDataLine)graf.DataGraf.data).groupName = "Link_Name";
            graf.DataGraf.data.valueName = "Id";
            graf.DataGraf.data.operacion = Operacion.Count;

            graf.Graficar<TrackLink>(lst);
            return graf;
        }

        private DTOGrafico GraficarTemplates(IList<TrackLink> lst, ref Dictionary<string, Color> LabelsColors)
        {
            DTOGraficoDona graf = new DTOGraficoDona();
            graf.LabelsColors = LabelsColors;
            graf.Title = Resources.TrackLink.Template;
            graf.SubTitle = "Cantidades";
            graf.divId = "tracklinkTemplates";
            graf.DataGraf.data.labelName = "TemplateName";
            graf.DataGraf.data.valueName = "IdTemplate";
            graf.DataGraf.data.operacion = Operacion.Count;

            graf.Graficar<TrackLink>(lst);
            return graf;
        }

        private void SaveSearchData(TrackLinkSearch filtros)
        {
            dynamic searchData = new System.Dynamic.ExpandoObject();
            searchData = filtros;
            TempData["TrackLinkSearch"] = searchData;
        }

        private DTOGraficoDona GraficarLinks(IList<TrackLink> lst, ref Dictionary<string, Color> LabelsColors)
        {
            DTOGraficoDona graf = new DTOGraficoDona();
            graf.LabelsColors = LabelsColors;
            graf.Title = "Links";
            graf.SubTitle = "Cantidades";
            graf.divId = "trackemailLinks";
            graf.DataGraf.data.labelName = "Link_Name";
            graf.DataGraf.data.valueName = "Id";
            graf.DataGraf.data.operacion = Operacion.Count;

            graf.Graficar<TrackLink>(lst);
            return graf;
        }

        [HttpPost]
        public ActionResult VerFechas(int Id)
        {
            IList<TrackLinkEvent> fechas;
            fechas = TrackLinkHome.FindFechas(Id);

            DTOListableItems listItems = new DTOListableItems();
            listItems.ListaElementos = new List<ObjetoNegocio>();
            foreach (TrackLinkEvent trackLinkEvent in fechas)
                listItems.ListaElementos.Add(trackLinkEvent);
            listItems.ListaCampos = new Dictionary<string, string>
            { 
                { "id", "Id" }, 
                { "Fecha", "Fecha" }
            };

            DTOPopUpListableItems dtoPopUp = new DTOPopUpListableItems();
            dtoPopUp.divPopUpName = "divPopUpTrackFechas";
            dtoPopUp.Title = "Fechas de apertura";
            dtoPopUp.ListableItems = listItems;

            return PartialView("API/_PopUpGenericTable", dtoPopUp);
        }
	}
}