using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA
{
    class NameList
    {
        public List<string> m_lstFemaleNames;
        public List<string> m_lstMaleNames;

        public NameList()
        {
            m_lstFemaleNames = File.ReadAllLines("D:\\Development\\Supernatural\\FemaleNames.txt").ToList();
            m_lstMaleNames = File.ReadAllLines("D:\\Development\\Supernatural\\MaleNames.txt").ToList();
        }
    }
}
