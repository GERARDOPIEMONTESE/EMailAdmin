DECLARE @RC int
execute @RC = [EMailAdmin].[dbo].[TableVariableText_A] 'PrepurchaseAccounts',0,25000
execute [EMailAdmin].[dbo].[TableVariableTextContent_A] @RC,1,'<table border="1"><theader><tr><td>Codigo</td><td>Sucursal</td><td>Nombre</td><td>Producto</td><td>Tarifa</td><td>Dias</td><td>Tarjetas</td></tr></theader><tbody>$body$</tbody></table>'