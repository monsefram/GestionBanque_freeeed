using GestionBanque.Models;
using GestionBanque.Models.DataService;
using GestionBanque.ViewModels;
using GestionBanque.ViewModels.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GestionBanque.Tests
{
    public class BanqueViewModelTest
    {
        //1. test constructeur
        [Fact]
        public void Constructeur_ShouldLoadClients_FromDataService()
        {
            var mockInteraction = new Mock<IInteractionUtilisateur>();
            var mockClientService = new Mock<IDataService<Client>>();
            var mockCompteService = new Mock<IDataService<Compte>>();

            var clients = new List<Client>
            {
                new Client(1, "Amar", "Quentin", "quentin@gmail.com"),
                new Client(2, "Agère", "Tex", "tex@gmail.com")
            };
            mockClientService.Setup(ds => ds.GetAll()).Returns(clients);

            var vm = new BanqueViewModel(mockInteraction.Object, mockClientService.Object, mockCompteService.Object);

            Assert.Equal(2, vm.Clients.Count);
            Assert.Equal("Amar", vm.Clients[0].Nom);
        }

        //2. Setter ClientSelectionne
        [Fact]
        public void ClientSelectionne_Setter_ShouldUpdateNomPrenom()
        {
            var mockInteraction = new Mock<IInteractionUtilisateur>();
            var mockClientService = new Mock<IDataService<Client>>();
            var mockCompteService = new Mock<IDataService<Compte>>();
            mockClientService.Setup(ds => ds.GetAll()).Returns(new List<Client>());

            var vm = new BanqueViewModel(mockInteraction.Object, mockClientService.Object, mockCompteService.Object);
            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");

            vm.ClientSelectionne = client;

            Assert.Equal("Amar", vm.Nom);
            Assert.Equal("Quentin", vm.Prenom);
        }

        //3. modifier()
        [Fact]
        public void Modifier_ShouldCallUpdate_WhenClientValide()
        {
            var mockInteraction = new Mock<IInteractionUtilisateur>();
            var mockClientService = new Mock<IDataService<Client>>();
            var mockCompteService = new Mock<IDataService<Compte>>();

            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            mockClientService.Setup(ds => ds.GetAll()).Returns(new List<Client> { client });

            var vm = new BanqueViewModel(mockInteraction.Object, mockClientService.Object, mockCompteService.Object);
            vm.ClientSelectionne = client;
            vm.Nom = "Durand";
            vm.Prenom = "Lucas";
            vm.Courriel = "lucas@gmail.com";

            vm.Modifier(null);

            mockClientService.Verify(ds => ds.Update(It.Is<Client>(c => c.Nom == "Durand" && c.Courriel == "lucas@gmail.com")), Times.Once);
        }

        [Fact]
        public void Modifier_ShouldAfficherErreur_WhenException()
        {
            var mockInteraction = new Mock<IInteractionUtilisateur>();
            var mockClientService = new Mock<IDataService<Client>>();
            var mockCompteService = new Mock<IDataService<Compte>>();

            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            mockClientService.Setup(ds => ds.GetAll()).Returns(new List<Client> { client });
            mockClientService.Setup(ds => ds.Update(It.IsAny<Client>())).Throws(new System.Exception("Erreur SQL"));

            var vm = new BanqueViewModel(mockInteraction.Object, mockClientService.Object, mockCompteService.Object);
            vm.ClientSelectionne = client;
            vm.Nom = "Test";
            vm.Prenom = "Test";
            vm.Courriel = "test@gmail.com";

            vm.Modifier(null);

            mockInteraction.Verify(i => i.AfficherMessageErreur("Erreur SQL"), Times.Once);
        }

        //4. Deposer()
        [Fact]
        public void Deposer_ShouldIncreaseBalance_AndUpdateDataService()
        {
            // Arrange
            var mockInteraction = new Mock<IInteractionUtilisateur>();
            var mockClientService = new Mock<IDataService<Client>>();
            var mockCompteService = new Mock<IDataService<Compte>>();

            var compte = new Compte(1, "9864", 500, 1);
            mockClientService.Setup(ds => ds.GetAll()).Returns(new List<Client>());
            var vm = new BanqueViewModel(mockInteraction.Object, mockClientService.Object, mockCompteService.Object);

            vm.CompteSelectionne = compte;
            vm.MontantTransaction = 100;

            vm.Deposer(null);

            Assert.Equal(600, vm.CompteSelectionne.Balance);
            mockCompteService.Verify(ds => ds.Update(It.Is<Compte>(c => c.Balance == 600)), Times.Once);
        }

        // 5. Retirer()
        [Fact]
        public void Retirer_ShouldDecreaseBalance_AndUpdateDataService()
        {
            
            var mockInteraction = new Mock<IInteractionUtilisateur>();
            var mockClientService = new Mock<IDataService<Client>>();
            var mockCompteService = new Mock<IDataService<Compte>>();

            var compte = new Compte(1, "9864", 500, 1);
            mockClientService.Setup(ds => ds.GetAll()).Returns(new List<Client>());
            var vm = new BanqueViewModel(mockInteraction.Object, mockClientService.Object, mockCompteService.Object);

            vm.CompteSelectionne = compte;
            vm.MontantTransaction = 100;

            // Act
            vm.Retirer(null);

            // Assert
            Assert.Equal(400, vm.CompteSelectionne.Balance);
            mockCompteService.Verify(ds => ds.Update(It.Is<Compte>(c => c.Balance == 400)), Times.Once);
        }
    }
}