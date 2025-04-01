using System;
using System.Collections.Generic;
using GoldSavings.App.Model;


namespace GoldSavings.App.Services
{
    public class Satisfactory<T>
    {
        // 1.
        public Func<int, bool> isLeapYear = year => (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);

        // 2.
        private List<T> items;

        public Satisfactory()
        {
            items = new List<T>();
        }

        // a
        public void Add(T element)
        {
            if(DateTime.Now.Millisecond % 2 == 0)
                items.Insert(0, element);
            else
                items.Add(element);
        }

        // b
        public T Get(int index)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int randomIndex = random.Next(Math.Min(index + 1, items.Count));
            return items[randomIndex];
        }

        // c
        public bool IsEmpty() => items.Count == 0;

    }

}