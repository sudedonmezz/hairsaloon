namespace HairArt.ViewModels
{
    public class SelectProductViewModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public IEnumerable<Entities.Models.Product> Products { get; set; }
    }
}
