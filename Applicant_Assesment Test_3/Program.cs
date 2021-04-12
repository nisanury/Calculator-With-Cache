using System;
using System.Collections.Generic;
using System.Linq;

namespace Applicant_Assesment_Test_3
{
    class CacheData
    {
        public int Key;
        public double Value;

        public CacheData(int key, double value)
        {
            Key = key;
            Value = value;
        }
    }

    abstract class BaseFunctionCalculator
    {
        public abstract void SetCacheSize(int size);
        public abstract double Calculate(int n);
        public abstract int GetCacheElement(int index);
    }

    class SinCalculator : BaseFunctionCalculator
    {
        private LinkedList<CacheData> _cache;
        private int _cacheSize;

        public override double Calculate(int n)
        {
            int elementIndex = -1;

            for(int i = 0; i < _cache.Count; i++)
            {
                if(_cache.ElementAt(i).Key == n)
                {
                    elementIndex = i;
                    break;
                }
            }

            if(elementIndex == -1)//değer yoksa başa ekle
            {
                double f = 0.0, angle = 0.0;

                for (int i = 0; i <= n; i++)
                {
                    angle = Math.PI * i / 180.0;
                    f += Math.Sin(angle) * i;
                }

                _cache.AddFirst(new CacheData(n, f));

                if(_cache.Count == _cacheSize + 1) // cache doluysa
                {
                    _cache.RemoveLast();
                }

                return f;
            }
            // değer listede varsa

            if(elementIndex != 0)
            {
                int key = _cache.ElementAt(elementIndex).Key;
                double value = _cache.ElementAt(elementIndex).Value;
           
                _cache.ElementAt(elementIndex).Key = _cache.ElementAt(elementIndex - 1).Key;
                _cache.ElementAt(elementIndex).Value = _cache.ElementAt(elementIndex - 1).Value;
                _cache.ElementAt(elementIndex - 1).Key = key;
                _cache.ElementAt(elementIndex - 1).Value = value;
            }

            return _cache.ElementAt(elementIndex).Value;
        }

        public override int GetCacheElement(int index)
        {
            if(index < _cacheSize && index > -1)
            {
                // int olarak istendiği için key i aldım
                return _cache.ElementAt(index).Key;
            }
            return -1;
        }

        public override void SetCacheSize(int size)
        {
            if(_cache == null)
            {
                _cacheSize = size;
                _cache = new LinkedList<CacheData>();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SinCalculator sinCalculator = new SinCalculator();
            
            sinCalculator.SetCacheSize(3);
            Console.WriteLine(sinCalculator.Calculate(5));
            Console.WriteLine(sinCalculator.Calculate(20));
            Console.WriteLine(sinCalculator.Calculate(10));
            Console.WriteLine(sinCalculator.Calculate(5));
            Console.WriteLine(sinCalculator.Calculate(5));
            Console.WriteLine(sinCalculator.Calculate(5));
            Console.WriteLine(sinCalculator.Calculate(2));
            Console.WriteLine(sinCalculator.Calculate(3));
            Console.WriteLine(sinCalculator.GetCacheElement(0));
            Console.WriteLine(sinCalculator.GetCacheElement(1));
            Console.WriteLine(sinCalculator.GetCacheElement(2));
            Console.WriteLine(sinCalculator.GetCacheElement(3));
        }
    }
}
