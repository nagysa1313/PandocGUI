using PandocGUI.Model;
using SharpRepository.XmlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.DAL
{
    public class PandocTaskRepository : XmlRepository<PandocTask>
    {
        public PandocTaskRepository()
            : base("data")
        {

        }
    }
}