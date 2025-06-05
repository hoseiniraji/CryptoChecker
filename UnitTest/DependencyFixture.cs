using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class DependencyFixture
    {
        public DependencyFixture()
        {
            var app = CryptoChecker.Program.BuildApp([]);
            ServiceProvider = app.Services.CreateScope().ServiceProvider;
        }
        public IServiceProvider ServiceProvider { get; private set; }

    }
}
