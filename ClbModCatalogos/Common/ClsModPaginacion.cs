namespace ClbModCatalogos.Common
{
    public class ClsModPaginacion
    {

        public int Paginas { get; set; }
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }


        public void Asignar(int intRegistrosPorPagina, decimal decTotalRegistros)
        {
            decimal paginas = decTotalRegistros / intRegistrosPorPagina;
            if ((int)paginas < paginas)
            {
                Paginas = (int)paginas + 1;
            }
            else
            {
                Paginas = (int)paginas;
            }
            TotalRegistros = int.Parse(decTotalRegistros.ToString());
        }
    }
}
