using AdemolaTyper.ViewModels;
using AdemolaTyper.ViewModels.Factories;
using AdemolaTyperTest.BDD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.ViewModels
{
    [TestClass]
    public class WhenHomeViewModelIsInstantiated : ContextSpec<HomeWindowViewModel>
    {
        protected override HomeWindowViewModel SetupContext()
        {
            return new HomeWindowViewModelFactory().CreateViewModel() as HomeWindowViewModel;
        }

        protected override void Because()
        {
        }

        [TestMethod]
        public void ShouldLoadUpTheOptionsViewModelAsTheMainViewModel()
        {
            Sut.Workspace.ShouldBeOfType(typeof(MenuViewModel));
        }
    }

    [TestClass]
    public class WhenHomeViewModelIsAskedToCloseMenuView : ContextSpec<HomeWindowViewModel>
    {

        protected override HomeWindowViewModel SetupContext()
        {
            return new HomeWindowViewModelFactory().CreateViewModel() as HomeWindowViewModel;
        }

        protected override void Because()
        {
            Sut.LoadGameOneCommand.Execute(null);
        }

        [TestMethod]
        public void ShouldSetTheCurrentWorkspaceToGameOneViewModel()
        {
            Sut.Workspace.ShouldBeOfType(typeof(GameOneViewModel));
        }
    }
}