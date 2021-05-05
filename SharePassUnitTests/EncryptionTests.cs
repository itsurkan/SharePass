using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePass.Encryptors;
using SharePass.Helpers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SharePassUnitTests
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void TestEncDec()
        {
            // Arrange

            var pass = "test data";
            var salt = new SaltGenerator().Generate();
            var salted = $"{pass}{salt}";

            // Act
            var enc = new AESEncryptor().Encrypt(salted);
            var dec = new AESEncryptor().Decrypt(enc);

            var decUnSalt = dec.Remove(dec.Length - salt.Length, salt.Length);

            // Assert
            Assert.AreEqual(pass, decUnSalt, "Wrong encryption/decryption");
        }

        [TestMethod]
        public void TestEncDecMultiple()
        {
            var startDate = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                TestEncDec();
            }
            var span = (DateTime.Now - startDate).Seconds;
            Assert.IsTrue(2 > span, "Too long enc/dec proccess");
        }

        [TestMethod]
        public void LinkTokenGenerationTest()
        {
            // Arrange
            var linkGenerator = new SharePass.Helpers.LinkGenerator();

            // Act
            var link = linkGenerator.Generate();

            // Assert
            Assert.AreEqual(24, link.Length, "Link token is too short");

            var secondLink = linkGenerator.Generate();
            Assert.AreNotEqual(link, secondLink, "The link tokens should not be the same");
        }
    }
}
