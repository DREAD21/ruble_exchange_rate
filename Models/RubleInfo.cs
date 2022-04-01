namespace ruble_exchange_rate.Models
{
    
    public class RubleInfo
    {
        public Info quotes { get; set; }
    }

    public class Info
    {
        public decimal USDRUB { get; set; }
        public decimal USDEUR { get; set; }
    }
}