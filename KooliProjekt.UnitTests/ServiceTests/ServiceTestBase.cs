using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using KooliProjekt.Services;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ServiceTestBase
    {
        protected Mock<IHealthDataService> _healthDataServiceMock;

        public ServiceTestBase()
        {
            _healthDataServiceMock = new Mock<IHealthDataService>();
        }
    }
}
