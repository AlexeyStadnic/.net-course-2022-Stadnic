using Models;
using Services;

using Xunit;

namespace ServiceTests;

public class BankServicesTests
{
    [Fact]
    public void AddBonusPositiveTest()
    {
        // Arrange
        var client = new Client();
        client.Name = "Алексей";
        client.Passport = 14714;
        client.Phone = "77881886";
        client.Birthday = new DateTime(1986, 2, 9);

        // Act
        BankService bank = new BankService();
        bank.AddBonus(client);        

        //Assert        
        Assert.True(client.Bonus == 1);
    }
    
    [Fact]
    public void AddToBlackListPositiveTest()
    {
        // Arrange
        var client = new Client();
        client.Name = "Алексей";
        client.Passport = 14714;
        client.Phone = "77881886";
        client.Birthday = new DateTime(1986, 2, 9);

        // Act
        BankService bank = new BankService();
        bank.AddToBlackList(client);

        //Assert
        Assert.True(bank.BlackList.Count != 0);
    }
    
    [Fact]
    public void IsPersonInBlackListPositiveTest()
    {
        // Arrange
        var client = new Client();
        client.Name = "Алексей";
        client.Passport = 14714;
        client.Phone = "77881886";
        client.Birthday = new DateTime(1986, 2, 9);

        // Act
        BankService bank = new BankService();
        bank.AddToBlackList(client);

        //Assert        
        Assert.True(bank.IsPersonInBlackList(client));
    }
}