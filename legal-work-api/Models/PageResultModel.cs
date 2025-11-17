namespace legal_work_api.Models
{
    public class PageResultModel<T>
    {
        public T Data { get; set; }

        public int Deslocamento { get; set; }
        public int RegistrosRetornados { get; set; }
        public int TotalRegistros { get; set; }
    }
}
