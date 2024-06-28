using CollegeLibraryDesktopApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestCollegeLibraryDesktopDesktopApp
{
    [TestClass]
    public class StatisticsUnitTest
    {
        [TestMethod]
        public void TestAllIssuedStatistics_Return()
        {
            int[] statistics = new Statistics().GetAllIssuedStatistics(10, 7);
            Assert.AreEqual(3, statistics[2]);
        }

        [TestMethod]
        public void TestAllRemainderStatistics_Return()
        {
            int[] statistics = new Statistics().GetAllRemainderStatistics(15, 7);
            Assert.AreEqual(8, statistics[1]);
        }
        [TestMethod]
        public void TestIssuedRemainderStatistics_Return()
        {
            int[] statistics = new Statistics().GetIssuedRemainderStatistics(10, 7);
            Assert.AreEqual(17, statistics[0]);
        }
        [TestMethod]
        public void TestStatistics_Return()
        {
            string statistics = new Statistics().GetAllStatistics(10, 7);
            Assert.AreEqual(statistics, "Выдано книг - 7, всего книг - 10, остаток - 3");
        }
        [TestMethod]
        public void TestCheckAllIssuedRemainderStatistics_Return()
        {
            bool result = new Statistics().CheckAllIssuedRemainderStatistics(10, 7, 3);
            Assert.AreEqual(true, result);
        }
    }
}
