using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Pinger
{
    class KR
    {
        public KR(string name, string address, int interval, string vsc_name, string zabbix_address)
        {
            this.address = address;
            this.name = name;
            this.interval = interval;
            this.vsc_name = vsc_name;
            this.zabbix_address = zabbix_address;
        }

        public string name = "";
        public string address = "";
        public int interval = 0;
        public string vsc_name = "";
        public string zabbix_address = "";

    }
}
