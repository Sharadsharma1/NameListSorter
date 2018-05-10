
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace name_sorterTest
{
    [TestClass]
    public class NameSorterTest
    {
        [TestMethod]
        public void CheckIfArgumentIsSupplied()
        {
            string[] arg = { };

            Assert.ThrowsException<Exception>(() => name_sorter.NameSorter.GetInputFileName(arg));  
        }

        [TestMethod]
        public void GetInputFileNameFromArgument()
        {
            string[] arg = {"Test"};
            Assert.AreEqual(arg[0], name_sorter.NameSorter.GetInputFileName(arg));
            
        }

        [TestMethod]
        public void ReadInputFile()
        {
            string inputFileName = @"../../../../unsorted-names-list.txt";
            ArrayList unSortedNameList = name_sorter.NameSorter.ReadInputFile(inputFileName);
            Assert.AreNotEqual(null, unSortedNameList);
        }

        [TestMethod]
        public void ReadBlankInputFile()
        {
            string inputFileName = @"../../../../blank-names-list.txt";
            Assert.ThrowsException<Exception>(() => name_sorter.NameSorter.ReadInputFile(inputFileName));
        }

        [TestMethod]
        public void CheckForSortedNameList()
        {
            ArrayList alPeople = new ArrayList();
            ArrayList unSortedNameList = new ArrayList();
            unSortedNameList.Add("Janet Parsons");
            unSortedNameList.Add("Vaughn Lewis");
            unSortedNameList.Add("Adonis Julius Archer");

            List<string> expectedSortedNameList = new List<string>();
            expectedSortedNameList.Add("Adonis Julius" + " " + "Archer");
            expectedSortedNameList.Add("Vaughn" + " " + "Lewis");
            expectedSortedNameList.Add("Janet" + " " + "Parsons");

            foreach (string fullName in unSortedNameList)            {
                string lastName = fullName.Split(' ').LastOrDefault();
                string firstName = fullName.Substring(0, fullName.Length - lastName.Length);
                alPeople.Add(new name_sorter.NameSorter.Person(firstName, lastName));
            }
            
            ArrayList actualSortedNameList = name_sorter.NameSorter.SortNameList(alPeople);
            List<string> sortedList = new List<string>();
            
            foreach (name_sorter.NameSorter.Person p in actualSortedNameList)
            {
                sortedList.Add(p.FirstName + "" + p.lastName);
            }

            
            Assert.IsTrue(sortedList[0] == expectedSortedNameList[0]);
            Assert.IsTrue(sortedList[1] == expectedSortedNameList[1]);
            Assert.IsTrue(sortedList[2] == expectedSortedNameList[2]);
        }
    }
}
