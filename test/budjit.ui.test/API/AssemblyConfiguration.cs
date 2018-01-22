using AutoMapper;
using budjit.ui.API.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace budjit.ui.test.API
{
    [TestClass]
    public class AssemblyConfiguration
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Mapper.Initialize(m => m.AddProfile<AutoMapperProfile>());
        }
    }
}