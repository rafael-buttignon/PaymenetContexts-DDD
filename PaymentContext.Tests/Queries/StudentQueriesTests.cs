using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Domain.Enums;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _student;

        public StudentQueriesTests()
        {
            for(var i=0;i<10;i++)
            {
                _student.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString() + "@balta.io")
                ));
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678901");
            var studn = _student.AsQueryable().Where(exp).FirstOrDefault(); 

            Assert.AreEqual(null, studn);
        }
                [TestMethod]
        public void ShouldReturnStudentWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("123456789011");
            var studn = _student.AsQueryable().Where(exp).FirstOrDefault(); 

            Assert.AreNotEqual(null, studn);
        }
    }
}