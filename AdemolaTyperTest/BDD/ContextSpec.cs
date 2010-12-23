using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AdemolaTyperTest.BDD
{
    public abstract class ContextSpec<T>
    {
        protected abstract T SetupContext();
        protected abstract void Because();
        protected T Sut { get; set; }


        [TestInitialize]
        public void TestFixtureSetup()
        {
            Sut = SetupContext();
            Because();
        }
    }

}