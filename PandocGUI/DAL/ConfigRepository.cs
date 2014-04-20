using PandocGUI.Model;
using SharpRepository.XmlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.DAL
{
    public class ConfigRepository : XmlRepository<Config>
    {
        public ConfigRepository()
            : base("data")
        {

        }
    }
}
