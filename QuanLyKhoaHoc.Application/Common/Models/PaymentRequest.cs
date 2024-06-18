namespace QuanLyKhoaHoc.Application.Common.Models
{
    public class PaymentRequest
    {
        public string OrderDesc { get; set; } = default!;
        public string ExpireDate { get; set; } = default!;
        public string BankCode { get; set; } = default!;
        public string Locale { get; set; } = default!;
        public string OrderType { get; set; } = default!;
        public string BillingMobile { get; set; } = default!;
        public string BillingEmail { get; set; } = default!;
        public string BillingFullName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingState { get; set; }
        public string InvoicePhone { get; set; }
        public string InvoiceEmail { get; set; }
        public string InvoiceCustomer { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceCompany { get; set; }
        public string InvoiceTaxcode { get; set; }
        public string InvoiceType { get; set; }
    }
}
