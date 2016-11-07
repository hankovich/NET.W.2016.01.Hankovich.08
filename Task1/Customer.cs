using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Customer : IFormattable
    {
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public decimal Revenue { get; set; }

        public Customer(string name, string contactPhone, decimal revenue)
        {
            Name = name;
            ContactPhone = contactPhone;
            Revenue = revenue;
        }

        public string ToString(string format = "", IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
                format = "N";
            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            /*
            if (!(formatProvider is CustomerFormatter))
                formatProvider = new CustomerFormatter(formatProvider);

            return string.Format(formatProvider, "{0:" + format + "}", this);
            /**/
            switch (format.ToUpper())
            {
                case "N":
                    return $"Customer record: {Name}";
                case "P":
                    return $"Customer record: {ContactPhone}";
                case "R":
                    return $"Customer record: {Revenue.ToString("", formatProvider)}";
                case "NR":
                    return $"Customer record: {Name}, {Revenue.ToString("###,###.00", formatProvider)}";
                case "NRP":
                    return $"Customer record: {Name}, {Revenue.ToString("###,###.00", formatProvider)}, {ContactPhone}";
                case "NP":
                    return $"Customer record: {Name}, {ContactPhone}";
                case "NPR":
                    return $"Customer record: {Name}, {ContactPhone}, {Revenue.ToString("###,###.00", formatProvider)}";
                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
    /* Для объектов класса Customer, у которого есть строковые свойства Name, ContactPhone и свойство Revenue типа decimal, 
     * реализовать возможность строкового представления различного вида.
Например, для объекта со значениями Name = "Jeffrey Richter", Revenue = 1000000, ContactPhone = "+1 (425) 555-0100", могут быть следующие варианты:
·          Customer record: Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100
·          Customer record: +1 (425) 555-0100
·          Customer record: Jeffrey Richter, 1,000,000.00
·          Customer record: Jeffrey Richter
·          Customer record: 1000000 и т.д.
Не изменяя класс Customer, добавить для объектов данного класса дополнительную возможность форматирования, не предусмотренную классом. Разработать модульные тесты.
*/