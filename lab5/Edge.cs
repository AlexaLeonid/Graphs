using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Edge
    {
        private int weight;
        private bool isIncluded = false;
        private bool isPrinted = false;
        public Edge(int weight)
        {
            this.weight = weight;
        }

        public void setIncluded(bool a)
        {
            this.isIncluded = a;
        }
        public bool getIncluded()
        {
            return isIncluded;
        }
        public int getWeight()
        {
            return this.weight;
        }
        public void setWeight(int weight)
        {
            this.weight = weight;
        }
        public void setPrinted(bool a)
        {
            this.isPrinted = a;
        }
        public bool getPrinted()
        {
            return isPrinted;
        }
    }
}
