using GestionBanque.Models;
using GestionBanque.Models.DataService;

namespace GestionBanque.Tests
{
    [Collection("Dataservice")]
    public class CompteSqliteDataServiceTest
    {
        private const string CheminBd = "Resources.bd"; 

        [Fact]
        [AvantApresDataService("Resources.bd")]
        public void Update_ShouldModifyBalance_WhenCorrect()
        {
            CompteSqliteDataService ds = new CompteSqliteDataService("Resources.bd");
            Compte compteAvant = ds.Get(1)!;
            double ancienneBalance = compteAvant.Balance;
            double nouvelleBalance = ancienneBalance + 100;

            compteAvant.Balance = nouvelleBalance;

            bool resultat = ds.Update(compteAvant);

            Assert.True(resultat);

            Compte compteApres = ds.Get(1)!;

            Assert.Equal(nouvelleBalance, compteApres.Balance);
        }
    }
}
