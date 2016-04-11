using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhoneAppDB {

    public class LocalizedResx {        

        private static PhoneAppDB.AppResx mylocalizedresx = new PhoneAppDB.AppResx();

        public PhoneAppDB.AppResx MyLocalizedResx {

            get{ return mylocalizedresx;}

        }

    }

}