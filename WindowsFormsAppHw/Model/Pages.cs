using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsAppHomework
{
    public class Pages
    {
        private List<Shapes> _pages;

        // constructor 
        public Pages()
        {
            _pages = new List<Shapes>();
        }
        // insert page
        public void Insert(int index, Shapes shapes)
        {
            _pages.Insert(index, shapes);
        }

        // index property
        public Shapes this[int index]
        {
            get
            {
                return _pages[index];
            }
            set
            {
                _pages[index] = value;
            }
        }

        // remove shape
        public void Remove(Shapes shapes)
        {
            _pages.Remove(shapes);
        }

        // count shapes
        public int Count
        {
            get
            {
                return _pages.Count;
            }
        }

        //add shapes into pages
        public void Add(Shapes shapes)
        {
            _pages.Add(shapes);
        }

        // encode
        //public string Encode()
        //{
        //    string encodedPages = "";
        //    foreach (Shapes shapes in _pages)
        //    {
        //        encodedPages += shapes.Encode();
        //        encodedPages += "\n";
        //    }
        //    return encodedPages;
        //}
        
    }
}