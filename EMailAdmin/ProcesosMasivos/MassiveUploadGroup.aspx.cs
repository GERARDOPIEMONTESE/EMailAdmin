using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMailAdmin.BackEnd.Home;
using ControlMenu;
using EMailAdmin.Utils;
using EMailAdmin.BackEnd.Domain;
using EMailAdmin.BackEnd.Service;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using CapaNegocioDatos.CapaNegocio;
using System.Globalization;
using CapaNegocioDatos.CapaHome;
using NPOI.XSSF.UserModel;
using System.Configuration;

namespace EMailAdmin.ProcesosMasivos
{
    public partial class MassiveUploadGroup : CustomPage
    {
        private int ? posibleRowError = null;
        private string NotFoundValueType = string.Empty;

        #region Constructor

        protected override void CustomPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControls();
                SessionManager.SetLoguedUser(UsuarioLogueadoDTO(), Session);
            }
        }
        
        #endregion Constructor
        
        protected void BtnAcceptOnClick(object sender, EventArgs e)
        {
            if(FormIsValid())
            {   
                var rGroup = new Group_R_Template(); 
                rGroup.Receive = chkReceive.Checked;
                rGroup.Template = TemplateHome.Get(Convert.ToInt32(ddlTemplate.SelectedItem.Value));
                rGroup.Module = ModuloHome.Obtener(ddlModule.SelectedItem.Value);

                var group = new BackEnd.Domain.Group();
                group.Name = txtName.Text;
                group.GroupType = GroupTypeHome.Get(Convert.ToInt32(ddlType.SelectedValue));
                group.IdUsuario = SessionManager.GetLoguedUser(Session).Id;
                group.Conditions = GetConditionsByFile();
                
                if (group.Conditions.Count > 0)
                {
                    rGroup.Group = group;
                    SaveAssociation(rGroup);
                    //ServiceLocator.Instance().GetGroupService().Save(group);
                    ShowOkMessage();
                    LoadGroup(group);
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            else
            {
                ShowErrorMessage();
            }
            loader.Style.Add("display", "none");
        }

        protected void BtnCancelOnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Group/GroupList.aspx");
        }

        protected override void ChequearSession()
        {
            var usuario = base.UsuarioLogueadoDTO();
            if (usuario == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["ExpireSessionRedirect"]);
            }
        }


        #region Get Group Conditions

        private GroupCondition GetSucursalCondition(Sucursal value)
        {
            return new GroupCondition
            {
                ConditionType = ConditionTypeHome.Get(BackEnd.Domain.ConditionType.Branch),
                Value = value.Id.ToString(CultureInfo.InvariantCulture),
                VisibleValue = value.NombreCombinado
            };
        }

        private GroupCondition GetProductCondition(BackEnd.Domain.Product value)
        {
            return new GroupCondition
            {
                ConditionType = ConditionTypeHome.Get(BackEnd.Domain.ConditionType.Product),
                Value = value.Id.ToString(CultureInfo.InvariantCulture),
                VisibleValue = value.Description
            };
        }

        private GroupCondition GetRatesCondition(Rate value)
        {
            return new GroupCondition
            {
                ConditionType = ConditionTypeHome.Get(BackEnd.Domain.ConditionType.Rate),
                Value = value.Id.ToString(CultureInfo.InvariantCulture),
                VisibleValue = value.Descripcion
            };
        }

        private GroupCondition GetCountryCondition(Locacion value)
        {
            return new GroupCondition
            {
                ConditionType = ConditionTypeHome.Get(BackEnd.Domain.ConditionType.Country),
                Value = value.Id.ToString(CultureInfo.InvariantCulture),
                VisibleValue = value.Nombre
            };
        }

        #endregion

        private bool FormIsValid()
        {
            if (fuExcelFile.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fuExcelFile.FileName);
                if (fileExt != ".xls" && fileExt != ".xlsx" && fileExt != ".csv")
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            if (txtName.Text == string.Empty)
            {
                return false;
            }

            return true;
        }

        private void BindControls()
        {
            ddlType.DataSource = GroupTypeHome.FindAll();
            ddlType.DataBind();

            ddlTemplate.DataSource = TemplateHome.FindAll();
            ddlTemplate.DataBind();

            ddlModule.DataSource = ModuloHome.FindAll();
            ddlModule.DataBind();
        }

        private void LoadGroup(BackEnd.Domain.Group group)
        {
            lblGroupName.Text = group.Name;

            lblBranches.Text = String.Join(", ", group.Conditions.Where(i => i.ConditionType.Id == EMailAdmin.BackEnd.Domain.ConditionType.Branch)
                                               .Select(j => j.VisibleValue));

            lblRates.Text = String.Join(", ", group.Conditions.Where(i => i.ConditionType.Id == EMailAdmin.BackEnd.Domain.ConditionType.Rate)
                                               .Select(j => j.VisibleValue));

            lblProducts.Text = String.Join(", ", group.Conditions.Where(i => i.ConditionType.Id == EMailAdmin.BackEnd.Domain.ConditionType.Product)
                                               .Select(j => j.VisibleValue));

            lblCountries.Text = String.Join(", ", group.Conditions.Where(i => i.ConditionType.Id == EMailAdmin.BackEnd.Domain.ConditionType.Country)
                                               .Select(j => j.VisibleValue));
            cntGroup.Visible = true;
        }

        private void ShowOkMessage()
        {
            ltrMessage.Text = string.Concat("<span class='ok'>", GetLocalResourceObject("okMsg").ToString(), "</span>");
        }

        private void ShowErrorMessage()
        {
            // muestro posible hint de fila en la que se puede ubicar el error
            string rowError = string.Empty;
            string errorCause = string.Empty;

            if (posibleRowError.HasValue)
            {
                rowError = string.Concat(" (", GetLocalResourceObject("errorRowMsg")  , " ", posibleRowError.ToString(), ") ");
            }

            if (NotFoundValueType != string.Empty)
            {
                errorCause = string.Concat(" - ", GetLocalResourceObject("errorMsg2").ToString(), NotFoundValueType);
            }

            ltrMessage.Text = string.Concat("<span class='error'>", GetLocalResourceObject("errorMsg").ToString(), rowError, errorCause, "</span>");
            cntGroup.Visible = false;
        }

        private IList<GroupCondition> GetConditionsByFile()
        {
            try
            {
                NPOI.SS.UserModel.IWorkbook wb;
                var gConditions = new List<GroupCondition>();
                string fileExt = System.IO.Path.GetExtension(fuExcelFile.FileName);                

                if (fileExt == ".xlsx")
                {
                    wb = new XSSFWorkbook(fuExcelFile.PostedFile.InputStream);
                }
                else if(fileExt == ".xls")
                {
                    wb = new HSSFWorkbook(fuExcelFile.PostedFile.InputStream);
                }
                else //if (fileExt == ".csv")
                {
                    wb = GetWorkBookFromCSV(fuExcelFile.PostedFile.InputStream);
                }

                ISheet sheet = wb.GetSheetAt(0);
                // dejo pasar la fila uno ya que deberia tener la descripicion de las columnas
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    posibleRowError = row + 1;
                    bool exitLoop = false;
                    if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                    {
                        switch (sheet.GetRow(row).GetCell(0).StringCellValue.ToLowerInvariant())
                        {
                            case "rte":
                                string[] rIds = sheet.GetRow(row).GetCell(4, NPOI.SS.UserModel.MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString().Split('/');
                                Product rProd = ProductHome.Get(rIds[0].Trim(), rIds[1].Trim(), Product.PRODUCT);
                                Rate rate = RateHome.GetByProductCode(rProd.Id, rIds[2].Trim());
                                
                                if (rate.Id != 0)
                                    gConditions.Add(GetRatesCondition(rate));
                                else
                                    exitLoop = true;
                                    NotFoundValueType = GetLocalResourceObject("rate").ToString();

                                break;
                            case "pro":
                                string[] pIds = sheet.GetRow(row).GetCell(2, NPOI.SS.UserModel.MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString().Split('/');
                                Product prod = ProductHome.Get(pIds[0].Trim(), pIds[1].Trim(), Product.PRODUCT);
                                
                                if (prod.Id != 0)
                                    gConditions.Add(GetProductCondition(prod));
                                else
                                    exitLoop = true;
                                    NotFoundValueType = GetLocalResourceObject("product").ToString();

                                break;
                            case "ctr":
                                string cId = sheet.GetRow(row).GetCell(1, NPOI.SS.UserModel.MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                                Pais pais = PaisHome.ObtenerPorCodigo(cId);
                                Locacion locacion = LocacionHome.Obtener(pais.IdLocacion);
                                
                                if (locacion.Id != 0)
                                    gConditions.Add(GetCountryCondition(locacion));
                                else 
                                    exitLoop = true;
                                    NotFoundValueType = GetLocalResourceObject("country").ToString();

                                break;
                            case "acc":
                                string[] sIds = sheet.GetRow(row).GetCell(3, NPOI.SS.UserModel.MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString().Split('/');
                                Sucursal suc = SucursalHome.Obtener(sIds[0].Trim(), sIds[1].Trim(), Convert.ToInt32(sIds[2].Trim()));
                                
                                if (suc.Id != 0)
                                    gConditions.Add(GetSucursalCondition(suc));
                                else
                                    exitLoop = true;
                                    NotFoundValueType = GetLocalResourceObject("branch").ToString();

                                break;
                            default:
                                exitLoop = true;
                                break;
                        }

                        if (exitLoop)
                        {
                            // hay algo mal lo mando a chequear
                            gConditions = new List<GroupCondition>();
                            break; 
                        }
                    }
                    else
                    {
                        // no seguro, el limite deberia ponerlo "LastRowNum"
                        break;
                    }
                }
                // todo salio bien devulevo las condiciones
                return gConditions;
            }
            catch
            {
                // log error                
            }
            // algo no salio bien lo informo
            return new List<GroupCondition>();
        }

        private IWorkbook GetWorkBookFromCSV(Stream stream)
        {
            List<List<string>> allRowAndColData = null;
            List<string> oneRowData = null;
            allRowAndColData = new List<List<String>>();

            var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var currentLine = reader.ReadLine();
                oneRowData = new List<String>();
                string [] oneRowArray = currentLine.Split(';');
                for (int j = 0; j < oneRowArray.Length; j++)
                {
                    oneRowData.Add(oneRowArray[j]);
                }
                allRowAndColData.Add(oneRowData);
            }

            HSSFWorkbook workBook = new HSSFWorkbook();
            try
            {
                ISheet sheet = workBook.CreateSheet("sheet1");
                for (int i = 0; i < allRowAndColData.Count(); i++)
                {
                    List<string> ardata = (List<string>)allRowAndColData[i];
                    IRow row = sheet.CreateRow((short)0 + i);
                    for (int k = 0; k < ardata.Count(); k++)
                    {
                        ICell cell = row.CreateCell((short)k);
                        cell.SetCellValue(ardata[k].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // log error
            }
            return workBook;
        }

        private void SaveAssociation(Group_R_Template association)
        {
            association.IdUsuario = UsuarioLogueadoDTO().Id;
            ServiceLocator.Instance().GetGroupRTemplateService().Save(association);
        }

    }

}