using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public class CustomerFormatter : IFormatProvider, ICustomFormatter
    {
        public IFormatProvider Parent { get; private set; }

        public CustomerFormatter(IFormatProvider parent = null)
        {
            Parent = parent ?? Thread.CurrentThread.CurrentCulture;
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                return "";

            Customer customer = arg as Customer;

            if (customer == null)
                return string.Format(Parent, "{0:" + format + "}", arg);

            if (string.IsNullOrEmpty(format))
                format = "N";

            return Format(format, customer, formatProvider);
        }

        protected virtual string Format(string format, Customer customer, IFormatProvider formatProvider)
        {
            switch (format.ToUpper())
            {
                case "N":
                    return $"Customer record: {customer.Name}";
                case "P":
                    return $"Customer record: {customer.ContactPhone}";
                case "R":
                    return $"Customer record: {customer.Revenue.ToString("C", formatProvider)}";
                case "NR":
                    return $"Customer record: {customer.Name}, {customer.Revenue}";
                case "NRP":
                    return $"Customer record: {customer.Name}, {customer.ContactPhone}, {customer.Revenue.ToString("C", formatProvider)}";
                case "NP":
                    return $"Customer record: {customer.Name}, {customer.ContactPhone}";
                case "NPR":
                    return $"Customer record: {customer.Name}, {customer.ContactPhone}, {customer.Revenue.ToString("C", formatProvider)}";

                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
