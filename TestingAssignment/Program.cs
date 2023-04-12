using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using NUnit.Framework.Internal;


namespace TestingAssignment
{

    internal class Program
    {
        static void Main(string[] args)
        {

        }
        public interface DiscountService
        {
            double GetDiscount();
         
        }
        public class InsuranceService
        {
            public DiscountService discountService;



            public InsuranceService(DiscountService discountService)
            {
                this.discountService = discountService;
            }



            public double CalcPremium(int age, string location)
            {
                double premium;



                if (location == "rural")
                {
                    if (age >= 18 && age < 30)
                        premium = 5.0;
                    else
                    {
                        if (age >= 31)
                            premium = 2.50;
                        else
                            premium = 0.0;
                    }
                }
                else if (location == "urban")
                {
                    if (age >= 18 && age <= 35)
                        premium = 6.0;
                    else
                    {
                        if (age >= 36)
                            premium = 5.0;
                        else
                            premium = 0.0;
                    }
                }
                else
                    premium = 0.0;



                double discount = discountService.GetDiscount();
                if (age >= 50)
                {
                    premium = premium * discount;
                }
                return premium;
            }
        }
        [TestFixture]
        public class InsuranceServiceTests
        {
            [Test]
            public void GetPremium_Returns5_WhenAgeis25LocationRural()
            {
                //Arrange
                var mockDiscountService = new Mock<DiscountService>();
                mockDiscountService.Setup(x => x.GetDiscount()).Returns(0);
                var insuranceService = new InsuranceService(mockDiscountService.Object);

                //Act
                double premium = insuranceService.CalcPremium(25,"rural");

                //Assert
                //Assert.AreEqual(5,premium); 
                Assert.That(premium, Is.EqualTo(5)); //constraint
            }
        }

    }
}

     

