using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_A_Ski.Models
{
    public class Invoice
    {
        public int id { get; set; }

        public string InvoiceNumber { get; set; }

        public bool PaymentCompleted { get; set; }

        private DateTime invoiceissued;

        public DateTime InvoiceIssued
        {
            set { invoiceissued = value; }
        }

        public string InvoiceIssuedOn
        {
            get { return invoiceissued.ToString("yyyy-MM-dd HH:mm"); }
        }

        private DateTime? paymentdate;

        public DateTime? PaymentDate
        {
            set { paymentdate = value; }
        }

        public string? InvoicePaidOn
        {
            get { return paymentdate?.ToString("yyyy-MM-dd HH:mm"); }
        }

        public static ObservableCollection<Invoice> ListOfInvoices { get; set; }

        public static void RefreshListOfInvoices() 
        {
            ListOfInvoices = new SQLController().GetInvoices();
        }
    }
}
