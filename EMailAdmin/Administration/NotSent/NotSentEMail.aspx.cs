using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlMenu;
using CapaNegocioDatos.CapaNegocio;
using CapaNegocioDatos.CapaHome;
using System.Globalization;
using EMailAdmin.BackEnd.Service;

namespace EMailAdmin.Administration.NotSent
{
    public partial class NotSentEMail : CustomPage
    {
        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFilters();
            }
        }

        protected void BtnProcessOnClick(object sender, EventArgs e)
        {
            if (cvPeriod.IsValid)
            {
                int countryCode = 0;
                Int32.TryParse(ddlCountry.SelectedValue, out countryCode);
                ServiceLocator.Instance().GetSendMailService().ProcessNotSentEMails(
                    countryCode, GetFromDate(), GetToDate(), "ACNET");
            }
        }

        protected void ValidatePeriod(object sender, ServerValidateEventArgs validation)
        {
            DateTime fromDate = GetFromDate();
            DateTime toDate = GetToDate();

            validation.IsValid = toDate.Ticks > fromDate.Ticks &&
                toDate.Subtract(fromDate).TotalHours <= 12;
        }

        #region Private Methods

        private void LoadFilters()
        {
            ddlCountry.Items.Clear();
            var countries = new List<Pais> { new Pais { Id = -1, Nombre = "" } };
            countries.AddRange(PaisHome.BuscarLazy());
            ddlCountry.DataSource = countries;
            ddlCountry.DataBind();
        }

        private DateTime GetDate(DateTime original, int hour, int minute)
        {
            DateTime date = new DateTime(original.Year, original.Month, original.Day,
                hour, minute, 0);

            return date;
        }

        private DateTime GetFromDate()
        {
            DateTime fromDate = Convert.ToDateTime(txtFromDate.Text, CultureInfo.CurrentCulture);
            int hour = 0;
            int minute = 0;

            Int32.TryParse(txtFromHour.Text, out hour);
            Int32.TryParse(txtFromMinute.Text, out minute);

            return GetDate(fromDate, hour, minute);
        }

        private DateTime GetToDate()
        {
            DateTime toDate = Convert.ToDateTime(txtToDate.Text, CultureInfo.CurrentCulture);
            int hour = 0;
            int minute = 0;

            Int32.TryParse(txtToHour.Text, out hour);
            Int32.TryParse(txtToMinute.Text, out minute);

            return GetDate(toDate, hour, minute);
        }

        #endregion
    }
}