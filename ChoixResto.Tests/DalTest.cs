using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChoixResto.Models;
using System.Collections.Generic;
using System.Data.Entity;

[TestClass]
public class DalTests
{
    private IDal dal;

    [TestInitialize]
    public void Init_AvantChaqueTest()
    {
        IDatabaseInitializer<DbContext> init = new DropCreateDatabaseAlways<DbContext>();
        Database.SetInitializer(init);
        init.InitializeDatabase(new MyDbContext());

        dal = new Dal();
    }

    [TestCleanup]
    public void ApresChaqueTest()
    {
        dal.Dispose();
    }

    [TestMethod]
    public void CreateRestaurant_AvecUnNouveauRestaurant_ObtientTousLesRestaurantsRenvoitBienLeRestaurant()
    {
        dal.CreateRestaurant("La bonne fourchette", "0102030405");
        List<Restaurant> restos = dal.GetAllRestaurants();

        Assert.IsNotNull(restos);
        Assert.AreEqual(1, restos.Count);
        Assert.AreEqual("La bonne fourchette", restos[0].Name);
        Assert.AreEqual("0102030405", restos[0].PhoneNumber);
    }

    [TestMethod]
    public void ModifierRestaurant_CreationDUnNouveauRestaurantEtChangementNameEtPhoneNumber_LaModificationEstCorrecteApresRechargement()
    {
        dal.CreateRestaurant("La bonne fourchette", "0102030405");
        dal.ModifyRestaurant(1, "La bonne cuillère", null);

        List<Restaurant> restos = dal.GetAllRestaurants();
        Assert.IsNotNull(restos);
        Assert.AreEqual(1, restos.Count);
        Assert.AreEqual("La bonne cuillère", restos[0].Name);
        Assert.IsNull(restos[0].PhoneNumber);
    }

    [TestMethod]
    public void RestaurantExiste_AvecCreationDunRestauraunt_RenvoiQuilExiste()
    {
        dal.CreateRestaurant("La bonne fourchette", "0102030405");

        bool existe = dal.RestaurantExist("La bonne fourchette");

        Assert.IsTrue(existe);
    }

    [TestMethod]
    public void RestaurantExiste_AvecRestaurauntInexistant_RenvoiQuilExiste()
    {
        bool existe = dal.RestaurantExist("La bonne fourchette");

        Assert.IsFalse(existe);
    }

    [TestMethod]
    public void ObtenirUser_UserInexistant_ReturnNull()
    {
        User user = dal.GetUser(1);
        Assert.IsNull(user);
    }

    [TestMethod]
    public void ObtenirUser_IdNonNumerique_ReturnNull()
    {
        User user = dal.GetUser("abc");
        Assert.IsNull(user);
    }

    [TestMethod]
    public void AddUser_NewUserEtRecuperation_LuserEstBienRecupere()
    {
        dal.AddUser("New user", "12345");

        User user = dal.GetUser(1);

        Assert.IsNotNull(user);
        Assert.AreEqual("New user", user.Name);

        user = dal.GetUser("1");

        Assert.IsNotNull(user);
        Assert.AreEqual("New user", user.Name);
    }

    [TestMethod]
    public void Authentifier_LoginMdpOk_AuthentificationOK()
    {
        dal.AddUser("New user", "12345");

        User user = dal.Authentificate("New user", "12345");

        Assert.IsNotNull(user);
        Assert.AreEqual("New user", user.Name);
    }

    [TestMethod]
    public void Authentifier_LoginOkMdpKo_AuthentificationKO()
    {
        dal.AddUser("New user", "12345");
        User user = dal.Authentificate("New user", "0");

        Assert.IsNull(user);
    }

    [TestMethod]
    public void Authentifier_LoginKoMdpOk_AuthentificationKO()
    {
        dal.AddUser("New user", "12345");
        User user = dal.Authentificate("New", "12345");

        Assert.IsNull(user);
    }

    [TestMethod]
    public void Authentifier_LoginMdpKo_AuthentificationKO()
    {
        User user = dal.Authentificate("New user", "12345");

        Assert.IsNull(user);
    }

    [TestMethod]
    public void HasAlreadyVoted_AvecIdNonNumerique_ReturnFalse()
    {
        bool pasVote = dal.HasAlreadyVoted(1, "abc");

        Assert.IsFalse(pasVote);
    }

    [TestMethod]
    public void HasAlreadyVoted_UserNAPasVote_ReturnFalse()
    {
        int idSurvey = dal.CreateSurvey();
        int idUser = dal.AddUser("New user", "12345");

        bool pasVote = dal.HasAlreadyVoted(idSurvey, idUser.ToString());

        Assert.IsFalse(pasVote);
    }

    [TestMethod]
    public void HasAlreadyVoted_UserAVote_ReturnTrue()
    {
        int idSurvey = dal.CreateSurvey();
        int idUser = dal.AddUser("New user", "12345");
        dal.CreateRestaurant("La bonne fourchette", "0102030405");
        dal.AddVote(idSurvey, 1, idUser);

        bool aVote = dal.HasAlreadyVoted(idSurvey, idUser.ToString());

        Assert.IsTrue(aVote);
    }

    [TestMethod]
    public void ObtenirLesResults_AvecQuelquesChoix_ReturnBienLesResults()
    {
        int idSurvey = dal.CreateSurvey();
        int idUser1 = dal.AddUser("User1", "12345");
        int idUser2 = dal.AddUser("User2", "12345");
        int idUser3 = dal.AddUser("User3", "12345");

        dal.CreateRestaurant("Resto pinière", "0102030405");
        dal.CreateRestaurant("Resto pinambour", "0102030405");
        dal.CreateRestaurant("Resto mate", "0102030405");
        dal.CreateRestaurant("Resto ride", "0102030405");

        dal.AddVote(idSurvey, 1, idUser1);
        dal.AddVote(idSurvey, 3, idUser1);
        dal.AddVote(idSurvey, 4, idUser1);
        dal.AddVote(idSurvey, 1, idUser2);
        dal.AddVote(idSurvey, 1, idUser3);
        dal.AddVote(idSurvey, 3, idUser3);

        List<Result> resultats = dal.GetResults(idSurvey);

        Assert.AreEqual(3, resultats[0].VoteAmount);
        Assert.AreEqual("Resto pinière", resultats[0].Name);
        Assert.AreEqual("0102030405", resultats[0].PhoneNumber);
        Assert.AreEqual(2, resultats[1].VoteAmount);
        Assert.AreEqual("Resto mate", resultats[1].Name);
        Assert.AreEqual("0102030405", resultats[1].PhoneNumber);
        Assert.AreEqual(1, resultats[2].VoteAmount);
        Assert.AreEqual("Resto ride", resultats[2].Name);
        Assert.AreEqual("0102030405", resultats[2].PhoneNumber);
    }

    [TestMethod]
    public void ObtenirLesResults_AvecDeuxSondages_ReturnBienLesBonsResults()
    {
        int idSurvey1 = dal.CreateSurvey();
        int idUser1 = dal.AddUser("User1", "12345");
        int idUser2 = dal.AddUser("User2", "12345");
        int idUser3 = dal.AddUser("User3", "12345");
        dal.CreateRestaurant("Resto pinière", "0102030405");
        dal.CreateRestaurant("Resto pinambour", "0102030405");
        dal.CreateRestaurant("Resto mate", "0102030405");
        dal.CreateRestaurant("Resto ride", "0102030405");
        dal.AddVote(idSurvey1, 1, idUser1);
        dal.AddVote(idSurvey1, 3, idUser1);
        dal.AddVote(idSurvey1, 4, idUser1);
        dal.AddVote(idSurvey1, 1, idUser2);
        dal.AddVote(idSurvey1, 1, idUser3);
        dal.AddVote(idSurvey1, 3, idUser3);

        int idSurvey2 = dal.CreateSurvey();
        dal.AddVote(idSurvey2, 2, idUser1);
        dal.AddVote(idSurvey2, 3, idUser1);
        dal.AddVote(idSurvey2, 1, idUser2);
        dal.AddVote(idSurvey2, 4, idUser3);
        dal.AddVote(idSurvey2, 3, idUser3);

        List<Result> resultats1 = dal.GetResults(idSurvey1);
        List<Result> resultats2 = dal.GetResults(idSurvey2);

        Assert.AreEqual(3, resultats1[0].VoteAmount);
        Assert.AreEqual("Resto pinière", resultats1[0].Name);
        Assert.AreEqual("0102030405", resultats1[0].PhoneNumber);
        Assert.AreEqual(2, resultats1[1].VoteAmount);
        Assert.AreEqual("Resto mate", resultats1[1].Name);
        Assert.AreEqual("0102030405", resultats1[1].PhoneNumber);
        Assert.AreEqual(1, resultats1[2].VoteAmount);
        Assert.AreEqual("Resto ride", resultats1[2].Name);
        Assert.AreEqual("0102030405", resultats1[2].PhoneNumber);

        Assert.AreEqual(1, resultats2[0].VoteAmount);
        Assert.AreEqual("Resto pinambour", resultats2[0].Name);
        Assert.AreEqual("0102030405", resultats2[0].PhoneNumber);
        Assert.AreEqual(2, resultats2[1].VoteAmount);
        Assert.AreEqual("Resto mate", resultats2[1].Name);
        Assert.AreEqual("0102030405", resultats2[1].PhoneNumber);
        Assert.AreEqual(1, resultats2[2].VoteAmount);
        Assert.AreEqual("Resto pinière", resultats2[2].Name);
        Assert.AreEqual("0102030405", resultats2[2].PhoneNumber);
    }
}