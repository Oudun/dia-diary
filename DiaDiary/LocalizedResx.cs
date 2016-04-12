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

namespace TypeOneControl {

    public class LocalizedResx {

        private static TypeOneControl.AppResx localizedLabels = new TypeOneControl.AppResx();

        public TypeOneControl.AppResx LocalizedLabels
        {

            get { return localizedLabels; }

        }

    }

}