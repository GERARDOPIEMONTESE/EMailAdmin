
use workflow

-- Afecta a 'PROD' y 'TEST'
update dynamicwssetup.wssetup
set [user]='AC_CUENTAS', [password]='cuentas2011'
where code='WANService'