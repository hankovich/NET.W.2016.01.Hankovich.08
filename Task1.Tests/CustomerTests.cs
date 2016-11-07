using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        [TestCase("", null, ExpectedResult = "Customer record: Jeffrey Richter")]
        [TestCase("R", null, ExpectedResult = "Customer record: 1000000")]
        [TestCase("N", null, ExpectedResult = "Customer record: Jeffrey Richter")]
        [TestCase("NP", null, ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100")]
        [TestCase("NPR", null, ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100, 1,000,000.00")]
        [TestCase("NPR", "en-GB", ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100, 1,000,000.00")]
        public string ToString_Format(string format, string cultureName)
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            if (cultureName != null)
                formatProvider = new CultureInfo(cultureName);

            Customer customer = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            
            return customer.ToString(format, formatProvider);
        }

        [Test]
        [TestCase("{0:N}", null, ExpectedResult = "Customer record: Jeffrey Richter")]
        [TestCase("{0:NP}", null, ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100")]
        [TestCase("{0:P}", "en-GB", ExpectedResult = "Customer record: +1 (425) 555-0100")]
        public string StringFormat(string format, string cultureName)
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            if (cultureName != null)
                formatProvider = CultureInfo.CreateSpecificCulture(cultureName);;

            Customer customer = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
            string result = string.Format(new CustomerFormatter(formatProvider), format, customer);
            return result;
        }
    }
}
