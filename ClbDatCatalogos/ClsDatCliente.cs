using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using ClbModCatalogos;
using System.Threading.Tasks;
using ClbModCatalogos.Common;

namespace ClbDatCatalogos
{
    public class ClsDatCliente
    {
        int RPP = 10;

        public async Task<IEnumerable<ClsModCliente>> Buscar(string strConexion, string strTextoBusqueda) {

            IEnumerable<ClsModCliente> lstClientes = null;
            using (var con = new SqlConnection(strConexion)){

                await con.OpenAsync();

                lstClientes = await con.QueryAsync<ClsModCliente>("SPCmotjcat005Busqueda",
                    new {
                        TextoBusqueda = strTextoBusqueda
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            
            }

            return lstClientes;
        }

        public async Task<ClsModResult> BuscarPaginacion(string strConexion, 
            string strTextoBusqueda, int intPagina)
        {
            ClsModResult objResult = new ClsModResult();
            int intTotalRegistros = 0;
            using (var con = new SqlConnection(strConexion))
            {

                await con.OpenAsync();

                objResult.Result = await con.QueryAsync<ClsModCliente, int, ClsModCliente>(
                    "SPCmotjcat005BusquedaPaginacion",
                    (objCliente, totalRegistros) => {
                        
                        intTotalRegistros = totalRegistros;

                        return objCliente;
                    },
                    new
                    {
                        TextoBusqueda = strTextoBusqueda,
                        Pagina = intPagina,
                        RPP = RPP
                    },
                    splitOn: "Registros",
                    commandType: System.Data.CommandType.StoredProcedure);
                objResult.ObjPaginacion = new ClsModPaginacion();
                objResult.ObjPaginacion.Asignar(RPP, intTotalRegistros);
                objResult.ObjPaginacion.Pagina = intPagina;

            }

            return objResult;
        }

    }
}
