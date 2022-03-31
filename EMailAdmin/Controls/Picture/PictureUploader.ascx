<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PictureUploader.ascx.cs" Inherits="EMailAdmin.Controls.Picture.PictureUploader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ajax_upload.1.0.min.js" type="text/javascript"></script>

<script type="text/javascript">
    function DeleteClick(id) {
        $('#result' + id).css('display', 'none');
        $('#loading' + id).css('display', 'none');
        $('#del_button' + id).css('display', 'none');
        $('#divShowPicture').css('display', 'none');
        $('#up_button' + id).css('display', 'block');
    }
</script>
<table width="100%">
    <tr>
        <td align="left">
            <asp:HiddenField runat="server" ID="HfIdUsuario" Value="-1" />
            <asp:Panel ID="pnlPicture" runat="server" BackColor="#FFFFFF" Width="150" Height="170">
                <table border="0">
                    <tr>
                        <td>
                            <table border="0">
                                <tr style="height: 130px;">
                                    <td style="width: 130px;" align="center">
                                        <div id="divShowPicture" style="display: block;">
                                            <table border="0">
                                                <tr style="height: 130px;">
                                                    <td style="width: 130px;" align="center">
                                                        <asp:Image runat="server" ID="imgPicture" ImageUrl="../../Image/ShowImageHandler.ashx" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ImageUrl="../../IMG/delete.gif" runat="server" ID="btnDelete" OnClientClick="return DeleteClick(0);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="up_button0" class="Text_Normal" style="cursor: pointer; display: none; text-align: center; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lblLoad" ForeColor="#898989" Font-Names="Verdana" Font-Size="11px"
                                                       Text="Cargar Foto" />
                                        </div>
                                        <span id="loading0" style="display: none; text-align: center; vertical-align: middle;">
                                            <img src="../../IMG/loading_animation_liferay.gif" alt="loading..." />
                                        </span>
                                        <div id="result0" style="display: none; text-align: center; vertical-align: middle;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div id="del_button0" class="Text_Normal" style="cursor: pointer; display: none; text-align: center; vertical-align: middle;">
                                            <img src="../../IMG/delete.gif" OnClientClick="return DeleteClick(0);" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajx:RoundedCornersExtender ID="rceFace" runat="server" TargetControlID="pnlPicture" Radius="6"
                                        Corners="All" BorderColor="#F5F5F5" Color="#FFFFFF" />
        </td>
    </tr>
</table>