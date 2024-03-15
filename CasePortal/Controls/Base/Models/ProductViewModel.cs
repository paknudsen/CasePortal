namespace NK.Web.CasePortal.Controls.Base.Models
{
    public class ProductViewModel
    {
        public string ProductId { get; private set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public decimal SalesPrice { get; set; }

        public string Description { get; set; }


        public ProductViewModel(string productId)
        {
            ProductId = productId;
        }

    }
}
