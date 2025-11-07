using Xunit;
using GestionBanque.Models;
using System;

namespace GestionBanque.Tests
{
    public class ClientTest
    {
        [Fact]
        public void SetterNom_AssigneCorrectementLaValeur()
        {

            var client = new Client(1, "AncienNom", "Jean", "jean@test.com");


            client.Nom = "Dupont";


            Assert.Equal("Dupont", client.Nom);
        }

        [Fact]
        public void SetterNom_Vide_DoitLeverException()
        {
            var client = new Client(1, "Dupont", "Jean", "jean@test.com");


            Assert.Throws<ArgumentException>(() => client.Nom = "");
        }

        [Fact]
        public void SetterPrenom_AssigneCorrectementLaValeur()
        {

            var client = new Client(1, "Dupont", "AncienPrenom", "jean@test.com");

            client.Prenom = "Paul";


            Assert.Equal("Paul", client.Prenom);
        }

        [Fact]
        public void SetterPrenom_Vide_DoitLeverException()
        {
            var client = new Client(1, "Dupont", "Jean", "jean@test.com");


            Assert.Throws<ArgumentException>(() => client.Prenom = "");
        }

        [Fact]
        public void SetterCourriel_AssigneCorrectementLaValeur()
        {

            var client = new Client(1, "Dupont", "Jean", "ancien@test.com");

            client.Courriel = "nouveautest.com";


            Assert.Equal("nouveautest.com", client.Courriel);
        }
        [Fact]
        public void SetterCourriel_Invalide_DoitLeverException()
        {
            var client = new Client(1, "Dupont", "Jean", "jean@test.com");


            Assert.Throws<ArgumentException>(() => client.Courriel = "invalide");
        }

        [Fact]
        public void SetterCourriel_Null_DoitLeverException()
        {
            var client = new Client(1, "Dupont", "Jean", "jean@test.com");


            Assert.Throws<ArgumentException>(() => client.Courriel = null);
        }
    }
}