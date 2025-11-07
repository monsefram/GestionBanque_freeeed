using Xunit;
using GestionBanque.Models; 
namespace GestionBanque.Tests
{
    public class CompteTest
    {
        [Fact]
        public void Deposer_AjouteMontantAuSolde()
        {

            double balance = 1000;
            var compte = new Compte(1, "Test", balance, 1); 

            compte.Deposer(50.0);

            Assert.Equal(1050.0, compte.Balance);
        }

        [Fact]
        public void Retirer_RetireMontantDuSolde()
        {
            var compte = new Compte(1, "Test", 200.0, 1);

            compte.Retirer(40.0);

            Assert.Equal(160.0, compte.Balance);
        }

        [Fact]
        public void Deposer_MontantNegatif_DoitLever()
        {
            var compte = new Compte(1, "Test", 100.0, 1);

            Assert.Throws<ArgumentException>(() => compte.Deposer(-1.0));
        }

        [Fact]
        public void Retirer_SoldeInsuffisant_DoitLever()
        {
            var compte = new Compte(1, "Test", 10.0, 1);

            Assert.Throws<InvalidOperationException>(() => compte.Retirer(50.0));
        }
    }
}