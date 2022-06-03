namespace Heliconia.Application.PurchasesServices.GetCustomer
{
    public class GetCustomerDTO
    {
        public string Id { get; set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string IdentificationDocument { get; private set; }

    }
}
