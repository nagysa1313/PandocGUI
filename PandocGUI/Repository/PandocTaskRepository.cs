using PandocGUI.Model;
using SharpRepository.Repository;
using SharpRepository.XmlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Repository
{
    public class PandocTaskRepository : XmlRepository<PandocTask>
    {
        static PandocTaskRepository()
        {
            
        }
    }
}
