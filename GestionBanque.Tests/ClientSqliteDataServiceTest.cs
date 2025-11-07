using GestionBanque.Models;
using GestionBanque.Models.DataService;

namespace GestionBanque.Tests
{
    [Collection("Dataservice")]
    public class ClientSqliteDataServiceTest
    {
        private const string CheminBd = "Resources.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void GetAll_ShouldReturnValidClients()
        {
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);

            var clients = ds.GetAll().ToList();

            Assert.Equal(3, clients.Count);

            Assert.Equal("Amar", clients[0].Nom);
            Assert.Equal("Quentin", clients[0].Prenom);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void RecupererComptes_ShouldReturnAssociatedAccounts()
        {
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");

            ds.RecupererComptes(client);

            Assert.Equal(2, client.Comptes.Count);
            Assert.Contains(client.Comptes, c => c.NoCompte == "9864");
            Assert.Contains(client.Comptes, c => c.NoCompte == "2370");
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Update_ShouldModifyClient_WhenValid()
        {
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client client = ds.Get(1)!;
            client.Nom = "Durand";
            client.Courriel = "durand@gmail.com";
            bool resultat = ds.Update(client);

            Assert.True(resultat);
            Client clientApres = ds.Get(1)!;
            Assert.Equal("Durand", clientApres.Nom);
            Assert.Equal("durand@gmail.com", clientApres.Courriel);
        }
    }
}