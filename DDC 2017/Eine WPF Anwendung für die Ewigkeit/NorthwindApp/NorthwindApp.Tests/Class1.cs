using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NorthwindApp.WpfApp.DAL;
using NorthwindApp.WpfApp.ViewModels;
using NUnit.Framework;

namespace NorthwindApp.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        [Test]
        public void Ensure_Initial_State_CustomerList_Is_Empty()
        {
            var dal = new Mock<INorthwindDAL>();
            var vm = new MainWindowViewModel(dal.Object);
            Assert.That(vm.Customers.Count, Is.EqualTo(0));
        }
        [Test]
        public void Ensure_AfterClick_CustomerList_Is_Not_Empty()
        {
            var dal = new Mock<INorthwindDAL>();
            //dal.Setup(x => x.GetAllCustomers()).Returns(new[]
            //{
            //    new CustomerList(),
            //});
            //var vm = new MainWindowViewModel(dal.Object);
            //vm.RefreshDataCommand.Execute(null);
            //Assert.That(vm.Customers.Count, Is.EqualTo(1));
        }
    }

   
}
