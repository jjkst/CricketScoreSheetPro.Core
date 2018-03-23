using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using CricketScoreSheetPro.Core.Services.Implementations;
using Firebase.Database;
using CricketScoreSheetPro.Core.ViewModels;

namespace CricketScoreSheetPro.Core.Test.RepositoryTest
{
    [TestClass]
    public class TournamentViewTest
    {
        private TournamentViewModel TournamentViewModel { get; set; }
        private AddDialogViewModel AddTournamentViewModel { get; set; }

        public TournamentViewTest()
        {
            var _client = new FirebaseClient("https://xamarinfirebase-4a90e.firebaseio.com/");
            var _tournamentService = new TournamentService(new TournamentRepository(_client, "UUID"), new TournamentDetailRepository(_client));
            TournamentViewModel = new TournamentViewModel(_tournamentService);
            AddTournamentViewModel = new AddDialogViewModel(_tournamentService);
        }

        [TestMethod]
        public void AddTournamentTest()
        {
            var t = AddTournamentViewModel.AddTournament("TEST");
        }

        [TestMethod]
        public void GetTournamentsTest()
        {
            var t = TournamentViewModel.Tournaments;
        }
    }
}
