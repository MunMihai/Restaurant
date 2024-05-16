namespace Restaurant.Dtos
{
    public class PreorderedDishDto
    {
        public int TableOrderId { get; set; }
        public List<int> VeganDishIds { get; set; }
        public List<int> GlutenFreeDishIds { get; set; }
        public List<int> SpicyDishIds { get; set; }
    }
}
