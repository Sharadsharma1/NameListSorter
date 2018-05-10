using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace name_sorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Get Input file name
            string inputFileName = NameSorter.GetInputFileName(args);

            //Read Input txt file
            ArrayList unSortedList = NameSorter.ReadInputFile(inputFileName);

            //Sort the names
            ArrayList getSortedList =  NameSorter.SortNameList(unSortedList);

            //Writing sorted list in txt file 
            NameSorter.WritingResults(getSortedList);
        }
    }

    /// <summary>
    /// Class for creating Person structure and method to sort names
    /// </summary>
    public class NameSorter
    {
        /// <summary>
        /// This is Sturcture for Person 
        /// </summary>
        public struct Person : IComparer
        {
            public string FirstName;
            public string lastName;

            public Person(string first, string last)
            {
                FirstName = first;
                lastName = last;
            }

            /// <summary>
            /// Function to Compare objects 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public int Compare(object a, object b)
            {
                var lNameCmp = 0;
                var fNameCmp = 0;

                var pa = (Person)a;
                var pb = (Person)b;

                lNameCmp = String.Compare(pa.lastName, pb.lastName);
                if (lNameCmp != 0) return lNameCmp;

                fNameCmp = String.Compare(pa.FirstName, pb.FirstName);

                return fNameCmp;
            }
        }

        /// <summary>
        /// Get the input arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetInputFileName(string[] args)
        {
            string inputFileName = string.Empty;

            //Check the argurments given
            if (args.Length > 0)
            {
                inputFileName = @args[0];
                return inputFileName;
            }
            else
            {
                Console.WriteLine("Input file name not provided.");
                throw new Exception("Input file name not provided.");
            }
        }

        /// <summary>
        /// Read input Name list from text file
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <returns></returns>
        public static ArrayList ReadInputFile(string inputFileName)
        {
            // Open the file to read from.
            string[] readText = File.ReadAllLines(inputFileName);
            ArrayList alPeople = new ArrayList();
            foreach (string fullName in readText)
            {
                if(!string.IsNullOrWhiteSpace(fullName))
                {
                    // Check in full name is having more than 3 given names
                    if (fullName.Split(' ').Length > 3)
                    {
                        Console.WriteLine("Name should have up to three given names");
                        throw new Exception("Name should have up to three given names");
                    }

                    string lastName = fullName.Split(' ').LastOrDefault();
                    string firstName = fullName.Substring(0, fullName.Length - lastName.Length);
                    alPeople.Add(new Person(firstName, lastName));
                }
            }

            if (alPeople.Count == 0)
            {
                throw new Exception("Input file is empty or does not content any data.");
            }

            return alPeople;
        }

        /// <summary>
        /// Method to Sort Names 
        /// </summary>
        /// <param name="args"></param>
        public static ArrayList SortNameList(ArrayList NameList)
        {
            Console.WriteLine("\n Name List before Sort");
            foreach (Person p in NameList)
            {
                Console.WriteLine("   {0} {1}", p.FirstName, p.lastName);
            }

            // sort arraylist using custom IComparer
            NameList.Sort(new Person());

            // output sorted array
            Console.WriteLine("\n Name List after Sort");

            return NameList;

            
        }

        /// <summary>
        /// Method to write sorting results in the file
        /// </summary>
        /// <param name="alPeople"></param>
        public static void WritingResults(ArrayList alPeople)
        {

            string outputFilePath = @"../sorted-names-list.txt";

            // check if the out file exists
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            // Looping aroung the people array list
            foreach (Person p in alPeople)
            {
                Console.WriteLine("   {0} {1}", p.FirstName, p.lastName);
                using (FileStream fs = new FileStream(outputFilePath, FileMode.Append))
                {
                    using (TextWriter tw = new StreamWriter(fs))
                    {
                        tw.WriteLine("   {0} {1}", p.FirstName, p.lastName);
                    }
                }
            }
        }
    }

    
}