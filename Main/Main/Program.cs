using System;
using System.Collections.Generic;
using System.Linq;
using Core.LinqTool;
using Core.Models;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = Activator.CreateInstance<Student>();

            var list = new StudentCollection()
            {
                new Student("UK","Mike", 12),
                new Student("UK","Tom", 10),
                new Student("USA","Green", 15),
                new Student("CHN","Jack", 8)
            };

            var tool = new DLinqTool();
            Console.WriteLine("/*************Group Sample*********/");

            #region Group sample

            var lambda = tool.GetGroupByLambda(obj, "Country");
            var group = list.GroupBy(lambda.Compile());

            foreach (var item in group)
            {
                foreach (var student in item)
                {
                    Console.WriteLine("Country: {0} Name: {1} Age: {2}", student.Country, student.Name, student.Age);
                }

                Console.WriteLine("==================================");
            }

            #endregion

            Console.WriteLine("/*************Order Sample*********/");

            #region Order Sample

            var orderLambda = tool.GetOrderByLambda(obj, "Age");
            var orderList = list.OrderBy(orderLambda.Compile());

            foreach (var student in orderList)
            {
                Console.WriteLine("Country: {0} Name: {1} Age: {2}", student.Country, student.Name, student.Age);

            }


            #endregion

            Console.WriteLine("/*************Select Sample*********/");

            #region Select Sample

            //Create filter
            var filter = new Filter();
            filter.data = "UK";
            filter.field = "Country";
            filter.op = "eq";

            IList<Filter> filters = new[] { filter };

            var search = new SearchFilter();
            search.groupOp = "And";
            search.rules = filters;

            //Create filter list
            var searchList = new List<SearchFilter>();
            searchList.Add(search);

            var stdLambda = tool.GetLambda(obj, searchList);
            var result = list.Where(stdLambda.Compile());

            foreach (var student in result)
            {
                Console.WriteLine("Country: {0} Name: {1} Age: {2}", student.Country, student.Name, student.Age);

            }

            #endregion

            Console.Read();

        }
    }

    #region Test model
    class Student
    {
        private string _name;
        private int _age;
        private string _country;

        public Student() { }
        public Student(string country, string name, int age)
        {
            _name = name;
            _age = age;
            _country = country;
        }
        public string Name
        {
            get { return _name; }
        }

        public int Age
        {
            get { return _age; }
        }

        public string Country
        {
            get { return _country; }
        }
    }

    class StudentCollection : List<Student>
    {

    }
    #endregion
}
