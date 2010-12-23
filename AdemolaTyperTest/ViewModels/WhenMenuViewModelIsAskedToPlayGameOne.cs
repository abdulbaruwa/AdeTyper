using System;
using AdemolaTyper.ViewModels;
using AdemolaTyperTest.BDD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.ViewModels
{
    [TestClass]
    public class WhenMenuViewModelIsAskedToPlayGameOne : ContextSpec<MenuViewModel>
    {
        private bool methodFired;
        private bool closeRequestFired;

        protected override MenuViewModel SetupContext()
        {
            return new MenuViewModel();
        }

        protected override void Because()
        {
            Sut.RequestStartGameOne += new EventHandler(Sut_RequestStartGameOne);
            Sut.RequestClose += new EventHandler(Sut_RequestClose);
            Sut.PlayGameOneCommand.Execute(null);
        }

        private void Sut_RequestClose(object sender, EventArgs e)
        {
            closeRequestFired = true;
        }

        private void Sut_RequestStartGameOne(object sender, EventArgs e)
        {
            methodFired = true;
        }

        [TestMethod]
        public void ShouldFireGameOneEvent()
        {
            methodFired.ShouldBeTrue();    
        }

        [TestMethod]
        public void ShouldFireMenuViewCloseEvent()
        {
            closeRequestFired.ShouldBeTrue();
        }
    }
}